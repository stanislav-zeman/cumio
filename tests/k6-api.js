import { check } from 'k6';
import { Httpx } from 'https://jslib.k6.io/httpx/0.1.0/index.js';

export const options = {
    iterations: 1,
};

const session = new Httpx({
    baseURL:  __ENV.BASE_URL || "http://api:8080"
})

export default function testSuite() {
    pingSwaggerDocs()
}

function pingSwaggerDocs() {
    const response = session.get("/index.html");
    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}
