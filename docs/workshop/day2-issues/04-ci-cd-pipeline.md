## CI/CD Pipeline

### Goal
Create a GitHub Actions CI pipeline that runs linting, tests, and builds on every PR.

### Acceptance Criteria
- [ ] `.github/workflows/ci.yml` runs on pull requests to `main`
- [ ] Pipeline steps: checkout → setup Python 3.12 → install deps → ruff lint → pytest with coverage
- [ ] Frontend steps: setup Node 20 → npm ci → npm run lint → npm run build
- [ ] Pipeline fails if coverage < 80%
- [ ] Pipeline fails if `ruff check` or `ruff format --check` finds issues
- [ ] Terraform validation: `terraform init -backend=false && terraform validate`
- [ ] Pipeline completes in under 5 minutes
