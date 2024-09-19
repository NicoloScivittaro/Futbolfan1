import { Component } from '@angular/core';
import { AuthService } from '../services/AuthService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(private authService: AuthService, private router: Router) { }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); // Verifica se il token è presente
  }

  logout(): void {
    localStorage.removeItem('token'); // Rimuove il token dal localStorage
    this.router.navigate(['/login']); // Reindirizza alla pagina di login
  }
}
