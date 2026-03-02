## Runbook and Deployment Documentation

### Goal
Create operational documentation for deploying, monitoring, and operating the service in Azure.

### Acceptance Criteria
- [ ] `docs/runbook.md` created with deployment, monitoring, and incident response sections
- [ ] Deployment section covers: Terraform apply steps, React build, FastAPI deployment to App Service, environment variable configuration
- [ ] Naming convention documented: `{resource-type}-{app_name}-{environment}` pattern for all Azure resources
- [ ] Monitoring section covers: Application Insights dashboards, key metrics (p95 latency, error rate, availability), alert rules
- [ ] Incident response section covers: severity levels, escalation paths, rollback procedure
- [ ] Key Vault secret rotation process documented
- [ ] Health check endpoint (`/api/health`) described with expected response
- [ ] Smoke test checklist for post-deployment verification

### Labels
`documentation`, `operations`

### Suggested Agent
`@nhs-service-builder`
