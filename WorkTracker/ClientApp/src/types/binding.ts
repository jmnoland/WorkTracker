export interface Binding<T> {
  value: T,
  onChange: (val: T) => void,
}
