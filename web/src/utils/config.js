const isDevelopment = process.env.NODE_ENV === 'development';
const apiDomain = isDevelopment ? 'http://localhost:52174' : '';

export default {
  isDevelopment,
  apiAddress: apiDomain,
};
