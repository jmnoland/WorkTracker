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
