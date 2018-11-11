export default {
  namespace: 'global',
  state: {},

  subscriptions: {
    setup({ dispatch, history }) {
      return history.listen(({ pathname, query, search }) => {});
    },
  },
  effects: {
    *fetch({ payload }, { call, put }) {
      yield put({ type: 'save', payload });
    },
  },
  reducers: {
    save(state, { payload }) {
      return { ...state, ...payload };
    },
  },
};
