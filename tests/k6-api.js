import http from 'k6/http';
import { check } from 'k6';
import { Httpx } from 'https://jslib.k6.io/httpx/0.1.0/index.js';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';
import { sleep } from 'k6';

export const options = {
    thresholds: {
        checks: ['rate>0.95'],
        'http_req_duration{scenario:docs}': ['p(99)<100'],
        'http_req_duration{scenario:collections}': ['p(99)<300'],
        'http_req_duration{scenario:content}': ['p(99)<300'],
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
        content: {
            executor: 'constant-vus',
            exec: 'contents',
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

const binFile = open(`./assets/test-record.mp3`)

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

export function contents() {
    createContents()
    getContentsWithPagination()
}

function createCollection() {
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

function getCollections() {
    const url = "/api/collections"
    const res = session.get(url);
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
}

function createContents() {
    const title = randomString(12)
    const url = `/api/contents?Title=${title}`
    const data = {
        InputFile: http.file(binFile, `test.mp3`),
    }

    const params = {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    };

    const res = session.post(url, data, params);
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
    sleep(1) // ENHANCE_YOUR_CALM otherwise
}

function getContentsWithPagination() {
    const url = "/api/contents"
    const payload = JSON.stringify({
        PageNumber: 2,
        PageSize: 5,
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = session.get(url, payload, params);
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
}
