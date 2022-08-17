import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { User } from '../_models/user';
import { getUser, removeUser, setUser } from '../_helpers/storage.helper';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../_models/apiResponse';

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

  login(username: string, password: string) {
    return this.http.post(`${environment.apiUrl}/security/login`, {username, password})
                    .pipe(map((response: ApiResponse) => {

                      return response;
                      // if (user && user.token) {
                      //   setUser(JSON.stringify(user));
                      //   this.currentUserSubject.next(user);
                      // }
                      // return user
                    }));
  }

  logout() {
    removeUser();
    this.currentUserSubject.next(null);
  }
}
