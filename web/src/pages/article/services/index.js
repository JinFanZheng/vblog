import { post, get } from 'utils/request';
import config from 'utils/config';

const { apiAddress } = config;
export async function query(params) {
  return post(`${apiAddress}/api/Article/getpages`, { params });
}
export async function detail(params) {
  return get(`${apiAddress}/api/Article/detail`, { params });
}
//
export async function save(params) {
  return post(`${apiAddress}/api/Article/save`, { params });
}
export async function getTags(params) {
  return get(`${apiAddress}/api/Tag/getall`, { params });
}
export async function insertTag(params) {
  return post(`${apiAddress}/api/Tag/insert`, { params });
}
export async function getCategories(params) {
  return get(`${apiAddress}/api/Category/getall`, { params });
}
export async function insertCategory(params) {
  return post(`${apiAddress}/api/Category/insert`, { params });
}
