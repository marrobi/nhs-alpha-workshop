#!/usr/bin/env bash
# Exports each draw.io (.drawio) and Mermaid (.mmd) diagram to a PNG file next
# to its source. Usage: scripts/export-diagrams.sh [directory-or-file ...]
# The default directory is docs/.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
mmdc="${repo_root}/scripts/diagrams/node_modules/.bin/mmdc"
puppeteer_config="${repo_root}/scripts/diagrams/puppeteer-config.json"
install_hint="Rebuild the dev container, or run .devcontainer/install-diagram-tools.sh."

if [[ $# -eq 0 ]]; then
  set -- "${repo_root}/docs"
fi

file_list="$(mktemp)"
trap 'rm -f -- "${file_list}"' EXIT
if ! find "$@" -type f \( -name '*.drawio' -o -name '*.mmd' \) -print0 > "${file_list}"; then
  echo "Could not discover all diagram inputs; no diagrams were exported." >&2
  exit 1
fi

drawio_files=()
mermaid_files=()
while IFS= read -r -d '' source; do
  case "${source}" in
    *.drawio) drawio_files+=("${source}") ;;
    *.mmd) mermaid_files+=("${source}") ;;
  esac
done < "${file_list}"

if [[ ${#drawio_files[@]} -eq 0 && ${#mermaid_files[@]} -eq 0 ]]; then
  echo "No .drawio or .mmd files found in: $*" >&2
  exit 1
fi

if [[ ${#drawio_files[@]} -gt 0 ]]; then
  if ! command -v drawio >/dev/null || ! command -v xvfb-run >/dev/null; then
    echo "The draw.io CLI or xvfb-run is not installed. ${install_hint}" >&2
    exit 1
  fi
  for source in "${drawio_files[@]}"; do
    target="${source%.drawio}.png"
    # Electron writes harmless D-Bus errors in a container, so show the output only on failure.
    if ! output="$(xvfb-run --auto-servernum drawio --no-sandbox --disable-gpu \
      --export --format png --scale 2 --border 10 --output "${target}" "${source}" 2>&1)"; then
      echo "${output}" >&2
      echo "Could not export ${source}" >&2
      exit 1
    fi
    echo "Exported ${target}"
  done
fi

if [[ ${#mermaid_files[@]} -gt 0 ]]; then
  if [[ ! -x "${mmdc}" ]]; then
    echo "mermaid-cli is not installed. ${install_hint}" >&2
    exit 1
  fi
  for source in "${mermaid_files[@]}"; do
    target="${source%.mmd}.png"
    "${mmdc}" --quiet --puppeteerConfigFile "${puppeteer_config}" \
      --backgroundColor white --scale 2 --input "${source}" --output "${target}"
    echo "Exported ${target}"
  done
fi
