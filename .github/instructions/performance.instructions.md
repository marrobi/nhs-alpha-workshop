---
applyTo: "**/performance/**,**/*.k6.js"
---

# Performance Testing Standards — k6

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
├── smoke.k6.js       # 1 VU, 1 minute — quick validation
├── load.k6.js        # 100 VU, 5 minutes — realistic load
├── stress.k6.js      # Ramp to 500 VU — find breaking point
└── helpers/
    └── config.js     # Shared thresholds, scenarios
```

## Thresholds

Always define `thresholds` in k6 options:

```javascript
thresholds: {
  http_req_duration: ['p(95)<200', 'p(99)<1000'],
  http_req_failed: ['rate<0.001'],
}
```

## Running

```bash
# BASE_URL must always be set explicitly — scripts will throw if it is missing
k6 run -e BASE_URL=http://localhost:3000 tests/performance/smoke.k6.js
k6 run -e BASE_URL=$BASE_URL tests/performance/load.k6.js
```
