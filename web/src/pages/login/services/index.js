import request, { post } from 'utils/request';
import config from 'utils/config';

const { apiAddress } = config;
export async function query(params) {
  return post(`${apiAddress}/api/Account/login`, { params });
}
