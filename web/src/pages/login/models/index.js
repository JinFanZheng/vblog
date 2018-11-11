import { query } from '../services';
export default {
  namespace: 'login',
  state: {
    success: true,
  },

  subscriptions: {
    setup({ dispatch, history }) {
      return history.listen(({ pathname, query, search }) => {});
    },
  },
  effects: {
    *fetch({ payload }, { call, put }) {
      const result = yield call(query, { userName: 'admin', password: 'admin' });
      console.log(result);
      yield put({ type: 'save', payload });
    },
  },
  reducers: {
    save(state, { payload }) {
      return { ...state, ...payload };
    },
  },
};
