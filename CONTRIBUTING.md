# Contributing

Thank you for your interest in contributing to this project. We welcome contributions from the community.

## How to contribute

1. Fork the repository
2. Create a feature branch from `main`
3. Make your changes
4. Ensure all tests pass and linting is clean
5. Submit a pull request

## Development setup

```bash
# Backend
pip install -r requirements.txt
uvicorn app.main:app --reload --port 8000

# Frontend
cd frontend && npm ci && npm run dev

# Tests
pytest
cd frontend && npm run lint
```

## Pull request process

- At least one approving review is required before merging
- All CI checks (lint, test, coverage, security scan) must pass
- Keep PRs focused — one change per PR where possible
- Write meaningful commit messages

## Code standards

- Follow existing patterns in the codebase
- All function signatures must have type annotations
- Aim for 80%+ test coverage
- Follow [PEP 8](https://peps.python.org/pep-0008/) (enforced by `ruff`)
- Use `nhsuk-react-components` for all user-facing UI

## Reporting issues

Open a GitHub Issue with a clear description, steps to reproduce, and expected behaviour.

## Code of conduct

This project follows the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you agree to uphold this code.

## Licence

By contributing, you agree that your contributions will be licensed under the [MIT License](LICENSE).
