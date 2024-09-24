import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Player } from '../model/player';
import { Team } from '../model/team';  // Importa il modello Team

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  private apiUrl = 'https://localhost:7293/api/Players/players'; // Correct URL?


  constructor(private http: HttpClient) { }

  // Ottenere tutte le squadre
  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`${this.apiUrl}/teams`);
  }

   getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}`);
  }

  // Ottenere i giocatori di una squadra specifica
  getPlayersByTeam(teamId: number): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}/teams/${teamId}/players`);
  }

  getPlayerById(id: number): Observable<Player> {
    return this.http.get<Player>(`${this.apiUrl}/${id}`);
  }

  updatePlayer(playerId: number, player: Player): Observable<Player> {
    return this.http.put<Player>(`${this.apiUrl}/players/${playerId}`, player);
  }

  // Creare un nuovo giocatore
  createPlayer(player: Player): Observable<Player> {
    return this.http.post<Player>(`${this.apiUrl}/players/create`, player); // Assicurati che l'endpoint sia corretto
  }
}
