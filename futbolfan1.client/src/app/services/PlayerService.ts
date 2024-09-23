import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Player } from '../model/player';
import { Team } from '../model/team';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private apiUrl = 'https://localhost:7293/api/teams';   // URL dell'API per i giocatori

  constructor(private http: HttpClient) { }

  createPlayer(player: Player): Observable<any> {
    return this.http.post(this.apiUrl, player);
  }
  getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}/players`); // Modifica l'endpoint in base alla tua API
  }
  getPlayers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/players`);

    // Ottiene i giocatori di una specifica squadra
  }
  getPlayersByTeam(teamId: number): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}/Teams/${teamId}/players`);
  }

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.apiUrl);
  }


}
