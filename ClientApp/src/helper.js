import { useState } from "react";
import jwt_decode from "jwt-decode";

export function verifyTokenExpiry(decodedToken) {
  // Adding miliseconds to timestamp
  const unix = Number(decodedToken.exp + "000");
  const expiryDate = new Date(unix);
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

export function useField(initial, initialVal) {
  const initialField = { ...initial, value: initialVal };
  const [field, _setField] = useState(initialField);

  const setField = (value, props) => {
    const temp = field;
    temp.value = value;
    if (temp.onChange && typeof temp.onChange === "function") {
      temp.onChange(value, props);
    }
    _setField(temp);
  };

  return { ...field, setField };
}
