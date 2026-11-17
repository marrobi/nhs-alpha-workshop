#!/usr/bin/env bash
# Installs the pinned diagram export tools: the draw.io desktop CLI (run headless
# with xvfb) and mermaid-cli. Runs from the dev container postCreateCommand.
set -euo pipefail

DRAWIO_VERSION="32.6.10"
declare -A DRAWIO_SHA256=(
  [amd64]="fc96faafe68cb4e06c0b7116779891592b20fce2cde7907adec504f23c5af83c"
  [arm64]="3cd0b69b31f9113f09df43f47f149e78f1a109d628282ae713e13d0e5d320800"
)

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
arch="$(dpkg --print-architecture)"

if [[ -z "${DRAWIO_SHA256[$arch]:-}" ]]; then
  echo "No pinned draw.io package for architecture '${arch}'." >&2
  exit 1
fi

if [[ "$(dpkg-query --showformat='${Version}' --show draw.io 2>/dev/null)" != "${DRAWIO_VERSION}" ]]; then
  deb="$(mktemp --suffix=.deb)"
  trap 'rm -f "${deb}"' EXIT
  curl -fsSL -o "${deb}" \
    "https://github.com/jgraph/drawio-desktop/releases/download/v${DRAWIO_VERSION}/drawio-${arch}-${DRAWIO_VERSION}.deb"
  echo "${DRAWIO_SHA256[$arch]}  ${deb}" | sha256sum --check --status
  sudo apt-get update
  # The draw.io package does not declare the sound and GPU buffer libraries that Electron loads.
  # Together with the draw.io dependencies, they also let the Puppeteer browser for mermaid-cli start.
  asound_pkg="libasound2t64"
  apt-cache show "${asound_pkg}" >/dev/null 2>&1 || asound_pkg="libasound2"
  sudo DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
    xvfb xauth "${asound_pkg}" libgbm1 "${deb}"
fi

if ! xvfb-run --auto-servernum drawio --no-sandbox --disable-gpu --version >/dev/null 2>&1; then
  echo "The draw.io CLI is installed but does not start." >&2
  exit 1
fi

npm ci --prefix "${repo_root}/scripts/diagrams" --no-audit --no-fund
