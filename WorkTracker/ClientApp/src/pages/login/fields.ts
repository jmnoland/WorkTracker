export default {
  email: {
    name: "email",
    validation: {
      rules: [
        {
          validate: (value: string) => {
            return value !== "" && value;
          },
          message: "Please enter an email",
        },
      ],
    },
  },
  password: {
    name: "password",
    validation: {
      rules: [
        {
          validate: (value: string) => {
            return value !== "" && value;
          },
          message: "Please enter a password",
        },
      ],
    },
  }
};
