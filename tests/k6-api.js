import { check } from 'k6';
import { Httpx } from 'https://jslib.k6.io/httpx/0.1.0/index.js';

export const options = {
   thresholds: {
        checks: ['rate>0.95'],
    },
    scenarios: {
        docs: {
          executor: 'constant-vus',
          exec: 'docs',
          vus: 1,
          duration: '5s',
          gracefulStop: '5s',
        },
    },
};

const session = new Httpx({
    baseURL:  __ENV.BASE_URL || "http://api:8080", // docker compose service name
    timeout: 1000, // 1s
})

export function docs() {
    const res = session.get("/index.html");
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
}
