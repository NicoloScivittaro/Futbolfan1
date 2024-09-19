// proxy.conf.js
const PROXY_CONFIG = [
  {
    context: ['/api'],
    target: 'http://localhost:7293',
    secure: false,
    changeOrigin: true,
    logLevel: 'debug'
  }
];

module.exports = PROXY_CONFIG;
