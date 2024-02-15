import { check } from 'k6';
import { Httpx } from 'https://jslib.k6.io/httpx/0.1.0/index.js';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

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
        collections: {
            executor: 'constant-vus',
            exec: 'collections',
            vus: 5,
            duration: '20s',
            gracefulStop: '20s',
        },
    },
};

const session = new Httpx({
    baseURL: __ENV.BASE_URL || "http://api:8080", // docker compose service name
    timeout: 1000, // 1s
})

export function docs() {
    const res = session.get("/index.html");
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
}

export function collections() {
    createCollection()
    getCollections()
}

export function createCollection() {
    const url = "/api/collections"
    const payload = JSON.stringify({
        Title: randomString(8),
        Description: randomString(64),
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = session.post(url, payload, params);
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
}

export function getCollections() {
    const url = "/api/collections"
    const res = session.get(url);
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
}
