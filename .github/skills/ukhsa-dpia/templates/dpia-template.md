# Data Protection Impact Assessment — [Service Name]

**Version**: 0.1
**Date**: YYYY-MM-DD
**Author**: [Name / Role]
**DPO / IG Lead**: [Name]
**Status**: Draft / Under Review / Approved

---

## 1. Identify the Need for a DPIA

**Why is this DPIA required?**

This UKHSA service processes [describe data] which includes [personal data / health data / both]. Where health data is in scope it is special-category under UK GDPR Art. 9, and large-scale or sensitive public-task processing triggers the mandatory DPIA requirement under Art. 35.

---

## 2. Describe the Processing

| Item | Detail |
|---|---|
| **Purpose** | |
| **Data subjects** | Members of the public / Clinicians / UKHSA staff / Partner organisations |
| **Categories of data** | |
| **Data sources** | |
| **Recipients** | |
| **Retention period** | |
| **Storage location** | Azure UK South (DR: UK West) |
| **Technical measures** | Encryption at rest (AES-256), TLS 1.2+ in transit, Private Endpoints, Managed Identity |

### Data Flow Diagram

[Insert or describe the data flow]

---

## 3. Consultation

| Consultee | Role | Date | Input |
|---|---|---|---|
| | Caldicott Guardian | | |
| | SIRO | | |
| | Safety Officer | | |
| | DPO / IG Lead | | |
| | Clinical / Policy Lead | | |
| | User / Public Representative | | |

---

## 4. Necessity and Proportionality

| Question | Answer |
|---|---|
| **Lawful basis (Art. 6)** | 6(1)(e) — public task |
| **Special category condition (Art. 9)** | 9(2)(i) public health / 9(2)(h) health & social care |
| **Is data minimised?** | |
| **Can purpose be achieved with less data?** | |
| **How is accuracy ensured?** | |
| **Caldicott Principles applied?** | |

---

## 5. Risk Assessment

| # | Risk | Likelihood | Severity | Overall Risk |
|---|---|---|---|---|
| 1 | | Low / Medium / High | Low / Medium / High | |
| 2 | | | | |

---

## 6. Measures to Mitigate Risks

| Risk # | Measure | Effect on Risk | Residual Risk | Owner |
|---|---|---|---|---|
| 1 | | Reduced / Accepted | | |
| 2 | | | | |

---

## 7. Technical Controls Verification

| Control | Implemented? | Evidence |
|---|---|---|
| User-Assigned Managed Identity | | Terraform `azurerm_user_assigned_identity` |
| Key Vault references for secrets | | `@Microsoft.KeyVault(SecretUri=...)` |
| HSTS enforced | | `app.UseHsts()` |
| TLS 1.2 minimum on all data-plane endpoints | | App Service / SQL / Storage configs |
| Private Endpoints for SQL / Key Vault / Storage | | `azurerm_private_endpoint` |
| Entra-only SQL authentication | | `azuread_authentication_only = true` |
| Diagnostic settings to Log Analytics | | `azurerm_monitor_diagnostic_setting` |

---

## 8. Sign Off

| Role | Name | Signature | Date |
|---|---|---|---|
| Caldicott Guardian | | | |
| SIRO | | | |
| Safety Officer | | | |
| DPO / IG Lead | | | |

---

## 9. Review Schedule

This DPIA will be reviewed:
- [ ] Before Go Live
- [ ] After significant changes to data processing or external integrations
- [ ] After any data incident
- [ ] At least annually

---

## Appendices

- A: Data Flow Diagram
- B: Caldicott Principles Mapping
- C: NHS DSP Toolkit Evidence References (where in scope)
- D: NCSC CAF Mapping
