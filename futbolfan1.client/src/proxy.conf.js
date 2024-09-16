const PROXY_CONFIG = [
  {
    context: ['/teams'], // Percorsi da proxy
    target: 'http://localhost:7293', // URL del tuo API
    secure: false,
    changeOrigin: true,
    logLevel: 'debug'
  }
];

module.exports = PROXY_CONFIG;
