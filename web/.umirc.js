// ref: https://umijs.org/config/
import { resolve } from 'path';
export default {
  history: 'hash',
  base: '/www/',
  publicPath: '/www/',
  outputPath: '../VBlog/wwwroot/www',
  plugins: [
    // ref: https://umijs.org/plugin/umi-plugin-react.html
    [
      'umi-plugin-react',
      {
        antd: true,
        dva: true,
        dynamicImport: true,
        title: 'VanZ博客管理端',
        dll: true,
        routes: {
          exclude: [],
        },
        // hardSource: true,
      },
    ],
  ],
  alias: {
    components: resolve(__dirname, 'src/components'),
    utils: resolve(__dirname, 'src/utils'),
    services: resolve(__dirname, 'src/services'),
    models: resolve(__dirname, 'src/models'),
    themes: resolve(__dirname, 'src/themes'),
    images: resolve(__dirname, 'src/assets'),
    mock: resolve(__dirname, 'mock'),
  },
};
