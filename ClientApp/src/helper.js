import jwt_decode from "jwt-decode";

export function verifyTokenExpiry(decodedToken) {
  const expiryDate = new Date(decodedToken.exp);
  if (expiryDate > Date.now()) {
    return true;
  }
  return false;
}

export function decodeJwtToken(token) {
  const decodedToken = jwt_decode(token);
  if (verifyTokenExpiry(decodedToken)) {
    return decodedToken;
  }
  return null;
}

export function getToken() {
  const cookies = document.cookie.split("=");
  const index = cookies.reduce((total, val, i) => {
    if (val === "X-User-Token") total = i + 1;
    return total;
  }, 0);
  return cookies[index];
}
