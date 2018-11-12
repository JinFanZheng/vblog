import { query, getTags, insertTag, getCategories,insertCategory } from '../services';
import action from 'utils/action';
import { message } from 'antd';
export default {
  namespace: 'article',
  state: {
    pages: {
      list: [],
      count: 0
    },
    tags: [],
    categories: [],
    insertTagSuccess: false,
    insertCategorySuccess: false
  },

  subscriptions: {
    setup({ dispatch, history }) {
      //{ pathname, query, search }
      return history.listen(({ pathname, query, search }) => {
        if (pathname === '/article/' || pathname === '/article') {
          dispatch(action('fetch', { pageIndex: 1, pageSize: 20 }))
        }
        if (pathname === '/article/edit/' || pathname === '/article/edit') {
          dispatch(action('getTags'));
          dispatch(action('getCategoies'));
        }
      });
    },
  },
  effects: {
    * fetch({ payload }, { call, put }) {
      console.log(payload)
      const result = yield call(query, payload);
      console.log(result);
      yield put(action('save', result));
    },
    * getTags({ payload }, { call, put }) {
      const result = yield call(getTags, payload);
      if (result.success && result.statusCode === 200) {
        yield put(action('setTags', result.data));
      }
    },
    * insertTag({ payload }, { call, put }) {
      const result = yield call(insertTag, payload);
      if (result.success && result.statusCode === 200) {
        message.success(result.message);
        yield put(action('setInsertTagSuccess', true));
      } else {
        message.error(result.message);
      }
    },
    * getCategoies({ payload }, { call, put }) {
      const result = yield call(getCategories, payload);
      if (result.success && result.statusCode === 200) {
        yield put(action('setCategoies', result.data));
      }
    },
    * insertCategory({ payload }, { call, put }) {
      const result = yield call(insertCategory, payload);
      if (result.success && result.statusCode === 200) {
        message.success(result.message);
        yield put(action('setInsertCategorySuccess', true));
      } else {
        message.error(result.message);
      }
    },
  },
  reducers: {
    save(state, { payload }) {
      return {...state, ...payload };
    },
    setTags(state, { payload }) {
      return {...state, tags: payload };
    },
    setInsertTagSuccess(state, { payload }) {
      return {...state, insertTagSuccess: payload };
    },
    setCategoies(state, { payload }) {
      return {...state, categories: payload };
    },
    setInsertCategorySuccess(state, { payload }) {
      return {...state, insertCategorySuccess: payload };
    },
  },
};