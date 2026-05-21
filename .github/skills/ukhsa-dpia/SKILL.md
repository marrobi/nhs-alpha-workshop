---
name: ukhsa-dpia
description: 'Use when drafting a Data Protection Impact Assessment (DPIA) for a UKHSA service that processes personal or health data under UK GDPR.'
---

# UKHSA DPIA — Data Protection Impact Assessment

This skill drafts DPIAs for UKHSA digital services following [ICO guidance](https://ico.org.uk/for-organisations/uk-gdpr-guidance-and-resources/accountability-and-governance/data-protection-impact-assessments-dpias/) and the [NHS Data Security and Protection Toolkit](https://www.dsptoolkit.nhs.uk/) where in scope.

## When to Use

- Starting a new UKHSA service that processes personal or health data
- Adding a new data processing activity to an existing service
- Integrating with external systems (PDS, GP Connect, MESH, lab feeds, partner data shares)
- Before an Alpha or Beta assessment — assessors will ask about data protection

## ICO 8-Step DPIA Process

1. **Identify the need** — Why is a DPIA required? (Art. 35 triggers: health data, public-task processing at scale)
2. **Describe the processing** — What data, from whom, how collected, how stored, how shared
3. **Consultation** — Caldicott Guardian, SIRO, Safety Officer, clinical/policy lead, IG team, user representatives
4. **Necessity and proportionality** — Lawful basis (Art. 6), special category condition (Art. 9), data minimisation
5. **Identify risks** — Risks to individuals' rights and freedoms
6. **Identify measures** — Technical and organisational measures to mitigate each risk
7. **Sign off** — Caldicott Guardian + SIRO + Safety Officer + DPO/IG lead approval
8. **Integrate outcomes** — Feed measures into the backlog, hazard log, and ADRs

## UK GDPR — Key Articles for UKHSA

- **Art. 6**: Lawful basis — typically `6(1)(e)` public task for UKHSA
- **Art. 9**: Special category condition — typically `9(2)(i)` public health, or `9(2)(h)` health/social care
- **Art. 35**: DPIA required for high-risk processing (health data always qualifies)
- **Art. 25**: Data protection by design and default

## UKHSA-Specific Considerations

- Caldicott Principles — justify each item of patient data
- NHS Data Security Standards (DSP Toolkit) where in scope
- Data flows to/from NHS Spine, PDS, GP Connect, MESH, partner labs
- Data residency — Azure UK South primary, UK West DR only
- Retention periods — NHS Records Management Code of Practice / UKHSA records schedule
- MHRA GxP / Annex 11 / ALCOA+ where the service falls under regulated scope

## Technical Controls Verification

For each DPIA the following controls must be evidenced in the service:

| Control | Evidence |
|---|---|
| User-Assigned Managed Identity | Terraform `azurerm_user_assigned_identity` |
| Key Vault references for secrets | `@Microsoft.KeyVault(SecretUri=...)` in App Service settings |
| HSTS + TLS 1.2 minimum | `app.UseHsts()` and `minimum_tls_version = "1.2"` |
| Private Endpoints for SQL / KV / Storage | `azurerm_private_endpoint` resources |
| Entra-only SQL authentication | `azuread_authentication_only = true` |
| Audit logging to Log Analytics | `azurerm_monitor_diagnostic_setting` on each resource |

## Output

The agent must **create or update** `docs/dpia/dpia.md` using the template in `templates/dpia-template.md`. **Do not edit this skill file** — it is a reference.

## Rules

- A DPIA is mandatory before live operation. No exceptions for health data.
- Material change → DPIA review. Not "we'll get to it" — same sprint.
- Residual risk above Medium needs documented acceptance by SIRO + Caldicott Guardian.
- DPIA links to: hazard log, ADRs, threat model, user research artefacts.
- Version-controlled in the same repo as the service.

## References

- [ICO DPIA guidance](https://ico.org.uk/for-organisations/uk-gdpr-guidance-and-resources/accountability-and-governance/data-protection-impact-assessments-dpias/)
- [NHS DSP Toolkit](https://www.dsptoolkit.nhs.uk/)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
- [NCSC Cloud Security Principles](https://www.ncsc.gov.uk/collection/cloud)
