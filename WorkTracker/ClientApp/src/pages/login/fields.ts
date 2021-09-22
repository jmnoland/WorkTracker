export const loginFields = {
  email: {
    name: "email",
    required: "Please enter an email",
    rules: [],
  },
  password: {
    name: "password",
    required: "Please enter a password",
    rules: [],
  }
};

export const registerFields = {
  email: {
    name: "email",
    required: "Please enter an email",
    rules: [],
  },
  name: {
    name: "name",
  },
  password: {
    name: "password",
    required: "Please enter a password",
    rules: [],
  },
  confirmPassword: {
    name: "confirmPassword",
    required: "Please retype your password",
    rules: [
      {
        validate: (value: string, data: { password: { value: string } }): boolean => {
            return value === data.password.value;
        },
        message: "Passwords must match",
      },
    ],
  },
};
