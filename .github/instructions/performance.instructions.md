---
applyTo: "**/performance/**,**/*.k6.js"
---

# Performance Testing Standards — k6
Performance and frontend speed targets for UKHSA services

## Targets

| Metric | Target |
|---|---|
| p95 response time | < 200ms |
| p99 response time | < 1000ms |
| Error rate under load | < 0.1% |
| LCP (Largest Contentful Paint) | < 2.5s |
| CLS (Cumulative Layout Shift) | < 0.1 |
| TTFB (Time to First Byte) | < 200ms |

## File Structure

```
tests/performance/
├── smoke.k6.js       # 1 VU, 1 minute — quick validation after deploy
├── load.k6.js        # Realistic concurrent users for 5 minutes
├── stress.k6.js      # Ramp until error rate or latency target breaches
├── soak.k6.js        # Hold steady load for 1+ hour (memory leaks, connection pool issues)
└── helpers/
    └── config.js     # Shared thresholds, scenarios, base URLs, auth tokens
```

`load.k6.js` virtual-user counts and ramp profiles MUST be sized to the service's expected production traffic, not a generic 100 VU figure — confirm with the product owner.

## Thresholds

Every k6 script MUST define `thresholds` in its options so the run fails the build when a target is breached:

```javascript
export const options = {
  thresholds: {
    http_req_duration: ['p(95)<200', 'p(99)<1000'],
    http_req_failed: ['rate<0.001'],
    checks: ['rate>0.99'],
  },
};
```

Service-specific thresholds (e.g. a critical-path endpoint with a tighter SLO) SHOULD be added per-endpoint using tagged metrics.

## Running

```bash
# BASE_URL MUST always be set explicitly — scripts MUST throw if it is missing
k6 run -e BASE_URL=https://localhost:5001 tests/performance/smoke.k6.js
k6 run -e BASE_URL=$BASE_URL tests/performance/load.k6.js
```

Performance runs MUST be reproducible. Test data MUST be seeded deterministically before each run; results MUST be archived to the CI build artefacts.

## When to Run

- **smoke**: on every deploy to non-production and production (as a post-deploy gate)
- **load**: on every PR that touches application code or infrastructure
- **stress**: at least monthly, and before any major release
- **soak**: at least before a major release, and after any change to connection pooling, caching, or background workers

## Frontend Performance

- Measure Core Web Vitals (LCP, CLS, INP, TTFB) in production via Application Insights or a real-user monitoring tool
- Lighthouse runs MUST be wired into CI for representative pages; score regressions MUST be investigated
- Static assets MUST be served with long-lived cache headers and a content-hash filename
- Page weight target: < 500 KB for the critical render path on mobile

## Reporting

- Test runs MUST emit a JSON summary that CI uploads as an artefact
- Trend data SHOULD be tracked over time so regressions show against a baseline, not against an arbitrary threshold
- Breached targets MUST be raised as bugs, not silently ignored
