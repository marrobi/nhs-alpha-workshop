---
name: nhs-dpia
description: 'Use when drafting a Data Protection Impact Assessment (DPIA) for an NHS service that processes health data under UK GDPR.'
---

# NHS DPIA — Data Protection Impact Assessment

This skill drafts DPIAs for NHS digital health services following [ICO guidance](https://ico.org.uk/for-organisations/uk-gdpr-guidance-and-resources/accountability-and-governance/data-protection-impact-assessments-dpias/) and the [NHS Data Security and Protection Toolkit](https://www.dsptoolkit.nhs.uk/).

## When to Use

- Starting a new NHS service that processes personal or health data
- Adding a new data processing activity to an existing service
- Integrating with external NHS systems (PDS, GP Connect, etc.)
- Before any Alpha assessment — assessors will ask about data protection

## ICO 8-Step DPIA Process

1. **Identify the need** — Why is a DPIA required? (Art. 35 triggers: health data = special category)
2. **Describe the processing** — What data, from whom, how collected, how stored, how shared
3. **Consultation** — Who was consulted (clinicians, patients, IG team, Caldicott Guardian)
4. **Necessity and proportionality** — Lawful basis (Art. 6), special category condition (Art. 9), data minimisation
5. **Identify risks** — Risks to individuals' rights and freedoms
6. **Identify measures** — Technical and organisational measures to mitigate each risk
7. **Sign off** — DPO/IG lead approval
8. **Integrate outcomes** — Feed measures into development backlog

## UK GDPR — Key Articles for NHS

- **Art. 6**: Lawful basis — typically `6(1)(e)` public task for NHS
- **Art. 9**: Special category condition — typically `9(2)(h)` health/social care purposes
- **Art. 35**: DPIA required for high-risk processing (health data always qualifies)
- **Art. 25**: Data protection by design and default

## NHS-Specific Considerations

- Caldicott Principles — justify each item of patient data
- NHS Data Security Standards (DSP Toolkit)
- Data flows to/from NHS Spine, PDS, GP Connect
- Data residency — Azure UK South/UK West only
- Retention periods — NHS Records Management Code of Practice

## Output

Generate the DPIA as `docs/dpia/dpia.md` using the template in `templates/dpia-template.md`.
