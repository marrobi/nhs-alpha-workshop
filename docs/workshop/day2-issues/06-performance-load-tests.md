## Performance Load Tests

### Goal
Create k6 load test suite with NHS-appropriate thresholds.

### Acceptance Criteria
- [ ] `tests/performance/smoke.k6.js` — 1 VU, 1 minute smoke test
- [ ] `tests/performance/load.k6.js` — ramp to 100 VU over 5 minutes
- [ ] `tests/performance/stress.k6.js` — ramp to 500 VU to find breaking point
- [ ] Shared thresholds: p95 < 200ms, p99 < 1000ms, error rate < 0.1%
- [ ] Tests check response status, body content, and timing
- [ ] Smoke test passes against local server
- [ ] Core Web Vitals check via Playwright (TTFB < 200ms, CLS < 0.1)
