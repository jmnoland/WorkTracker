interface Size {
    default: string,
    small: string,
    medium: string,
    large: string,
};

export interface Theme {
    id: number,
    name: string,
    colors: {
        background: string,
        lighter: string,
        light: string,
        dark: string,
        orange: string,
        orange_dark: string,
        white: string,
        danger: string,
        success: string,
    },
    border: {
        shadow: string,
        radius: {
            default: string,
            button: string,
        },
    },
    padding: Size,
    margin: Size,
    font: {
        size: Size
    },
};

export const theme: Theme = {
  id: 0,
  name: "default",
  colors: {
    background: "#1d2228",
    lighter: "#888888",
    light: "#555555",
    dark: "#101417",
    orange: "#fb8122",
    orange_dark: "#E1741E",
    white: "#e1e2e2",
    danger: "#ff0000",
    success: "#228B22",
  },
  border: {
    shadow: "0px 0px 20px 5px #101417",
    radius: {
      default: "1px",
      button: "1px",
    },
  },
  padding: {
    default: "5px",
    small: "2.5px",
    medium: "10px",
    large: "20px",
  },
  margin: {
    default: "10px",
    small: "5px",
    medium: "20px",
    large: "40px",
  },
  font: {
    size: {
      default: "14px",
      small: "11px",
      medium: "18px",
      large: "22px",
    },
  },
};
