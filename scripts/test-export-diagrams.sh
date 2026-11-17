#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
fixture_dir="$(mktemp -d)"
trap 'rm -f -- "${fixture_dir}/diagram with spaces.mmd" "${fixture_dir}/output"; rmdir -- "${fixture_dir}"' EXIT
printf 'graph TD; A-->B;\n' > "${fixture_dir}/diagram with spaces.mmd"

assert_discovery_failure() {
  if bash "${repo_root}/scripts/export-diagrams.sh" "$@" > "${fixture_dir}/output" 2>&1; then
    echo "Expected diagram discovery to fail: $*" >&2
    exit 1
  fi
  if ! grep -q 'Could not discover all diagram inputs; no diagrams were exported.' "${fixture_dir}/output"; then
    cat "${fixture_dir}/output" >&2
    exit 1
  fi
  if grep -q '^Exported ' "${fixture_dir}/output"; then
    echo "Export started before all inputs were discovered." >&2
    exit 1
  fi
}

assert_discovery_failure "${fixture_dir}/missing"
assert_discovery_failure "${fixture_dir}/diagram with spaces.mmd" "${fixture_dir}/missing"
assert_discovery_failure "${fixture_dir}/missing" "${fixture_dir}/diagram with spaces.mmd"
echo "Diagram discovery regression checks passed."
