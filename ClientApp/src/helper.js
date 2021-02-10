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
        if (
          currentField &&
          currentField.validation &&
          currentField.validation.rules
        ) {
          currentField.validation.errors = [];
          const errors = fields[fieldKey].validation.rules.reduce(
            (total, rule) => {
              const result = rule.validate(fields[fieldKey].value, fields);
              if (!result) {
                isValid = false;
                total.push({ message: rule.message, id: rule.id });
                currentField.validation.errors.push({
                  message: rule.message,
                  id: rule.id,
                });
              }
              return total;
            },
            []
          );
          if (errors.length === 0) return null;
          return { [fieldKey]: { errors } };
        }
        return { [fieldKey]: { errors: null } };
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
