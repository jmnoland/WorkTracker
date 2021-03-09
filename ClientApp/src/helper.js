import { useState, useRef, useEffect } from "react";
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
  if (!token) return;
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

export function parseDateTime(value) {
  let date = value;
  if (typeof date === "string") date = new Date(value);
  return `${date.getDate()}-${date.getMonth()}-${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}`;
}

export function getUserMapping(users) {
  return users.reduce((total, user) => {
    total[user.userId] = user.name;
    return total;
  }, {});
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

export function useObject(initialFields, initialValues) {
  // if error fields are missing add them
  function getInitialValues() {
    return Object.keys(initialFields).reduce((total, field) => {
      const currentField = {
        ...initialFields[field],
        value: initialValues[field],
      };
      if (currentField.validation) {
        currentField.validation.errors = [];
      } else currentField.validation = { errors: [] };
      total[field] = currentField;
      return total;
    }, {});
  }
  function getInitialFields() {
    return Object.keys(initialFields).reduce((total, field) => {
      const currentField = { ...initialFields[field] };
      if (currentField.validation) {
        currentField.validation.errors = [];
      } else currentField.validation = { errors: [] };
      total[field] = currentField;
      return total;
    }, {});
  }

  const allInitialValuesRef = useRef(getInitialValues());

  useEffect(() => {
    allInitialValuesRef.current = getInitialValues();
  }, [initialValues]);

  const allInitialValues = allInitialValuesRef.current;

  const [values, setValues] = useState(allInitialValues);

  const [fields, setFields] = useState(getInitialFields());

  const object = {
    data: Object.keys(fields).reduce((total, field) => {
      const currentField = fields[field];
      currentField.value = values[field].value;
      total[field] = {
        ...currentField,
        onChange: (value) => {
          if (currentField.onChange) {
            currentField.onChange(value, object.data);
          }
          currentField.validation.errors = [];
          return setValues((oldValues) => {
            return {
              ...oldValues,
              [field]: {
                ...oldValues[field],
                value: value,
              },
            };
          });
        },
      };
      return total;
    }, {}),
    validate() {
      let isValid = true;
      Object.keys(fields).forEach((fieldKey) => {
        const currentField = fields[fieldKey];
        if (currentField.validation && currentField.validation.rules) {
          currentField.validation.errors = currentField.validation.rules.reduce(
            (errors, rule, index) => {
              if (!rule.validate(currentField.value, fields)) {
                isValid = false;
                errors.push({
                  id: `${fieldKey}-validation-fail-${index}`,
                  message: rule.message,
                });
              }
              return errors;
            },
            []
          );
        }
      });
      if (!isValid) setValues(fields);
      return isValid;
    },
    reset() {
      setValues(() => getInitialValues());
      setFields(initialFields);
    },
  };
  return object;
}
