import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

export interface IUser {
  email: string;
  username: string;
  avatarUrl?: string;
}

const defaultPath = '/home';

export interface AuthResponse {
  token: string;
  email: string;
  userName: string;
}

@Injectable()
export class AuthService {
  private _user: IUser | null = null;

  get loggedIn(): boolean {
    const token = localStorage.getItem('accessToken');
    return !!token
  }

  private _lastAuthenticatedPath: string = defaultPath;
  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private http: HttpClient, private router: Router) {
  }

  logIn(username: string, password: string) {
    return this.http.post<AuthResponse>('/api/Account/login', {username, password}).pipe(tap({
      next: async (res) => {
        localStorage.setItem('accessToken', res.token)
        this._user = {
          email: res.email,
          username: res.userName,
        }
        this.router.navigate([this._lastAuthenticatedPath]);
      },
    }))
  }

  getUser() {
    return this.http.get<AuthResponse>(`api/Account/me`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('accessToken')}`,
      },
    }).pipe(tap({
      next: (res) => {
        this._user = {
          email: res.email,
          username: res.userName,
        }
      },
    }))
  }

  createAccount(username: string, email: string, password: string) {
    return this.http.post<AuthResponse>('/api/Account/register', {
      Username: username,
      Email: email,
      Password: password,
    })
      .pipe(tap({
        next: (res) => {
          localStorage.setItem('accessToken', res.token)
          this._user = {
            email: res.email,
            username: res.userName,
          }
          this.router.navigate([this._lastAuthenticatedPath]);
        },
      }))
  }

  //TODO: Reset and Change Password

  async logOut() {
    this._user = null;
    localStorage.removeItem('accessToken')
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
      'reset-password',
      'create-account',
      'change-password/:recoveryCode',
    ].includes(route.routeConfig?.path || defaultPath);

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = defaultPath;
      this.router.navigate([defaultPath]);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/login-form']);
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
    }

    return isLoggedIn || isAuthForm;
  }
}
