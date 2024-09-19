import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthResponse } from '../model/AuthResponse';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7293/user';

  constructor(private http: HttpClient) { }

  register(email: string, userName: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/register`, { email, userName, password, confirmPassword: password });
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/api/login`, { email, password })
      .pipe(
        tap((response: AuthResponse) => {
          localStorage.setItem('token', response.token);  // Salva il token
        })
      );
  }

  getProtectedData(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/protected`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.getToken()}`
      })
    });
  }

  getProfile(): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http.get<any>(`${this.apiUrl}/profile`, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
