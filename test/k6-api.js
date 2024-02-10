import { check } from 'k6';
import http from "k6/http";

export const options = {
  iterations: 1,
};

export default function () {
  const response = http.get("https://test-api.k6.io/public/crocodiles/");
  check(response, {
    'is status 200': (r) => r.status === 200,
  });
}
