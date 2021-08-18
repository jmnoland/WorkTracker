export const loginFields = {
  email: {
    name: "email",
    required: "Please enter an email",
    validation: {
      rules: [],
    },
  },
  password: {
    name: "password",
    required: "Please enter a password",
    validation: {
      rules: [],
    },
  }
};

export const registerFields = {
  email: {
    name: "email",
    required: "Please enter an email",
    validation: {
      rules: [],
    },
  },
  name: {
    name: "name",
  },
  password: {
    name: "password",
    required: "Please enter a password",
    validation: {
      rules: [],
    },
  },
  confirmPassword: {
    name: "confirmPassword",
    required: "Please retype your password",
    validation: {
      rules: [
        {
          validate: (value: string, data: { password: { value: string } }) => {
            return value === data.password.value;
          },
          message: "Passwords must match",
        },
      ],
    },
  },
};
