> CuzForum前端部分，使用react, redux, react-router, axios 开发

接口地址修改位置：`src/js/axios.js`

```js
const Axios = axios.create({
  baseURL: process.env.NODE_ENV === 'production' ? 'https://api.yiiu.co' : 'http://localhost:8080'
});
```

## 开始

运行

```sh
yarn install
yarn start # yarn start 启动的 NODE_ENV 是 development
如果无法启动，可以尝试： set NODE_OPTIONS=--openssl-legacy-provider && npm run start
```



构建

```sh
yarn install
yarn build # yarn build 后，部署运行后的 NODE_ENV 是 production
```

## 部署

nginx部署

```sh
# 假如构建好的build文件夹放在 /home/build
server {
  listen 5000;
  server_name localhost;
  location / {
    root /home/build;
    index index.html;
  }
}
```

serve命令运行

```sh
npm -g install serve
serve build
# 后台运行
nohup serve build &
```
