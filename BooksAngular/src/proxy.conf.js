const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
    ? env.ASPNETCORE_URLS.split(';')[0]
    : 'https://localhost:7219';

const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:7219",
    secure: false,
    changeOrigin: true,
    logLevel: 'debug'
  }
]

module.exports = PROXY_CONFIG;
