 import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44339/',
  redirectUri: baseUrl,
  clientId: 'AssetManagementApp_App',
  responseType: 'code',
  scope: 'offline_access AssetManagementApp',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'AssetManagementApp',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44339',
      rootNamespace: 'AssetManagementApp',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
