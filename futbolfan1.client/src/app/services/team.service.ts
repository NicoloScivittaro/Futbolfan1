import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private apiUrl = 'https://localhost:5001/api/teams';  // URL dell'API per le squadre
  private playerApiUrl = 'https://localhost:5001/api/players';  // URL per i giocatori

  constructor(private http: HttpClient) { }

  getTeams(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Aggiungi questo metodo per ottenere i giocatori per un team specifico
  getPlayers(teamId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/${teamId}/players`);  // Assicurati che l'endpoint esista
  }
  buyPlayer(teamId: number, playerId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/BuyPlayer`, { teamId, playerId });
  }

}
