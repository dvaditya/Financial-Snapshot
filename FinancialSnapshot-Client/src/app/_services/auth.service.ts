import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { User } from '../_models/user';
import { getUser, removeUser, setUser } from '../_helpers/storage.helper';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../_models/apiResponse.interface';
import { Login } from '../_models/security/login.interface';
import { Register } from '../_models/security/register.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User | null>;
  public currentUser: Observable<User | null>;

  constructor(private http: HttpClient) { 
    this.currentUserSubject = new BehaviorSubject<User | null>(getUser());
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  login(loginForm: Login) {
    return this.http.post(`${environment.apiUrl}/security/login`,{ data: loginForm })
                    .pipe(map((response: ApiResponse) => {
                      if(response.IsSuccess && response.Data){
                        setUser(JSON.stringify(response.Data));
                        this.currentUserSubject.next(response.Data);
                      }
                      return response;
                    }));
  }

  logout() {
    removeUser();
    this.currentUserSubject.next(null);
  }

  register(registerForm: Register) {
    return this.http.post(`${environment.apiUrl}/security/register`, { data: registerForm })
                    .pipe(map((response: ApiResponse) => {
                      return response;
                    }));
  }
}
