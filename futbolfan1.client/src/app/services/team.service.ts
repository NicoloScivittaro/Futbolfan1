import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Team } from '../model/team'; // Crea i modelli corrispondenti
import { Player } from '../model/player'; // Crea i modelli corrispondenti

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private apiUrl = 'https://localhost:5001/api/teams';  // URL del backend

  constructor(private http: HttpClient) { }

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.apiUrl);
  }
  getPlayers(teamId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/teams/${teamId}/players`);
  }
  buyPlayer(teamId: number, playerId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/buyPlayer`, { teamId, playerId });
  }

  sellPlayer(playerId: number, teamId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/sellPlayer`, { playerId, teamId });
  }
}
