import { Injectable } from '@angular/core';
import { TOKEN_KEY, USER_KEY } from '../_constants/session-key.constant';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  public saveToken(token: string): void {
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  public saveUser(user: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }
}
