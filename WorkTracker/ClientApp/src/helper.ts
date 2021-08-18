import { useState } from "react";
import jwtDecode from "jwt-decode";
import { DecodedToken, Dictionary, User, Error, FormField, ValidationRule, Form } from "./types";

export function verifyTokenExpiry(decodedToken : DecodedToken): boolean {
    // Adding miliseconds to timestamp
    const unix = decodedToken.exp * 1000;
    if (unix > Date.now()) {
        return true;
    }
    return false;
}

export function dateToUTCUnix(date: Date): number {
    return Date.UTC(
        date.getUTCFullYear(),
        date.getUTCMonth(),
        date.getUTCDate(),
        date.getUTCHours(),
        date.getUTCMinutes(),
        date.getUTCSeconds(),
        date.getUTCMilliseconds()
    );
}

export function decodeJwtToken(token : string) : DecodedToken | null {
    if (!token) return null;
    const decodedToken: DecodedToken = jwtDecode(token);
    if (verifyTokenExpiry(decodedToken)) {
        return decodedToken;
    }
    return null;
}

export function parseDateTime(value : string) : string | null {
    if (typeof value !== "string") return null;
    const date : Date = new Date(value);
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    const hour = date.getHours();
    const minute = date.getMinutes();
    return `${day < 10 ? `0${day}` : day}-${
        month < 10 ? `0${month}` : month
    }-${year} ${hour < 10 ? `0${hour}` : hour}:${
        minute < 10 ? `0${minute}` : minute
    }`;
}

export function getUserMapping(users?: User[]) : Dictionary<string> {
    const userMap : Dictionary<string> = {};
    if (!users) return userMap;
    users.reduce((total, user) => {
        total[user.userId] = user.name;
        return total;
    }, userMap);
    return userMap;
}

export function useForm(initialFields: Dictionary<any>, initialValues: Dictionary<any>): Form {
    const objectKeys = Object.keys(initialValues);
    const [values, setValues] = useState(initialValues);
    const [modified, setModified] = useState([] as string[]);
    const [errors, setErrors] = useState<Dictionary<Error[]>>(objectKeys.reduce(
        (total: Dictionary<Error[]>, val: string) => {
            total[val] = [];
            return total;
        },
    {}));

    function onChange(value: any, name: string) {
        setValues({ ...values, [name]: value });
        if (!modified.includes(name)) setModified([ ...modified, name ]);
    }

    function checkRules(rules: ValidationRule<any>[], name: string): boolean {
        let allValid = true;
        const temp: Error[] = [];
        if (initialFields[name].required) {
            if (values[name] === undefined || values[name] === null || values[name] === "") {
                temp.push({ id: "R", message: initialFields[name].required });
                allValid = false;
            }
        }
        let count = 0;
        rules.forEach(rule => {
            if (!rule.validate(values[name])) {
                temp.push({ id: `V${count}`, message: rule.message });
                allValid = false;
            };
            count ++;
        });
        if (!allValid) setErrors({ ...errors, [name]: temp });
        return allValid;
    }

    function validate(): boolean {
        let valid = true;
        modified.forEach((name) => {
            const rules = initialFields[name].rules;
            if (rules) {
                if (!checkRules(rules, name)) valid = false;
            }
        });
        return valid;
    }

    function reset() {
        setValues(initialValues);
        setModified([] as string[]);
        setErrors(initialValues.map((val: any) => ({ [val]: [] })));
    }

    return {
        form: objectKeys.reduce((total: Dictionary<any>, name: string) => {
            total[name] = {
                ...initialFields[name],
                errors: errors[name],
                value: values[name],
                onChange: (val: any) => onChange(val, name),
            }
            return total;
        }, {} as Dictionary<FormField<any>>),
        modified: modified,
        validate: validate,
        reset: reset,
    };
}
