import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/AuthService';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;

      // Log per verificare i dati inviati
      console.log('Email:', email);
      console.log('Password:', password);

      this.authService.login(email, password).subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);  // Memorizza il token nel localStorage
          console.log('Login successful!');

          // Reindirizza l'utente dopo il login (modifica il percorso se necessario)
          this.router.navigate(['/home']);
        },
        error: (error: HttpErrorResponse) => {
          console.error('Login error:', error);

          // Controlla se è un errore di credenziali non valide
          if (error.status === 401) {
            console.log('Unauthorized access - invalid credentials');
            // Puoi anche aggiungere un messaggio d'errore visibile nell'interfaccia
          } else {
            console.log('Unexpected error occurred.');
          }
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
