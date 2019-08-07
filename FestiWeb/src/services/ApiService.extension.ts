import type = Mocha.utils.type;

export class AuthClient {
    private url: any;
    constructor (defaultUrl: string) {
      this.url = defaultUrl;
    }

    getBaseUrl (defaultUrl: string) {
      return this.url
    }

    transformHttpRequestOptions (options: RequestInit): Promise<RequestInit> {
      if (options.headers) {
        (<any>options.headers)['Authorization'] = 'Bearer ' + localStorage.getItem('token')
      }

      options.mode = 'cors'
      return Promise.resolve(options)
    }
}

export class ApiClientBase {
  constructor (private authClient: AuthClient) {

  }
    protected _loginAttempts = 0;
    getBaseUrl (defaultUrl: string) {
      return this.authClient ? this.authClient.getBaseUrl(defaultUrl) : defaultUrl
    }

    transformOptions (options: RequestInit): Promise<RequestInit> {
      return this.authClient ? this.authClient.transformHttpRequestOptions(options) : Promise.resolve(options)
    }

    protected transformResult (url: string, response: Response, processor: (response: Response) => any) {
      if (response.status == 401) {
        this._loginAttempts += 1
        if (this._loginAttempts >= 3) {
          localStorage.removeItem('token')
          localStorage.removeItem('vuex')
          this._loginAttempts = 0
        }
      }
      return processor(response)
    }
}
