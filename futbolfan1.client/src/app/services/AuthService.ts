import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthResponse } from '../model/AuthResponse';

interface RegisterResponse {
  success: boolean;
  message: string;
}

interface ProfileResponse {
  id: number;
  email: string;
  userName: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7293/user';

  constructor(private http: HttpClient) { }

  register(email: string, userName: string, password: string): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.apiUrl}/api/register`, { email, userName, password, confirmPassword: password });
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/api/login`, { email, password })
      .pipe(
        tap((response: AuthResponse) => {
          localStorage.setItem('token', response.token);  // Salva il token
        })
      );
  }

  getProtectedData(): Observable<any> {  // Qui puoi definire un'interfaccia se conosci la struttura
    return this.http.get<any>(`${this.apiUrl}/protected`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.getToken()}`
      })
    });
  }

  getProfile(): Observable<ProfileResponse> {
    const token = localStorage.getItem('token');
    return this.http.get<ProfileResponse>(`${this.apiUrl}/profile`, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
