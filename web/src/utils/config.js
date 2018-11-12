const isDevelopment = process.env.NODE_ENV === 'development';
const apiDomain = isDevelopment ? 'https://localhost:5001' : '';

export default {
  isDevelopment,
  apiAddress: apiDomain,
};
