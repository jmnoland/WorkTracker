interface Action {
  type: string
}

const logger = (store: any) => (next: (action: Action) => unknown) => (action: Action) => {
  console.group(action.type)
  console.info('dispatching', action)
  const result = next(action)
  console.log('next state', store.getState())
  console.groupEnd()
  return result
}

export default logger
