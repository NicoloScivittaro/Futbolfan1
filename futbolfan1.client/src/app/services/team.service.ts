import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Team } from '../model/team';
import { Player } from '../model/player';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private apiUrl = 'https://localhost:7293/api/teams';  // Assicurati che l'URL sia corretto

  constructor(private http: HttpClient) { }

  // Ottieni la lista di team
  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.apiUrl);
  }

  // Ottieni i dettagli di un team
  getTeam(teamId: number): Observable<Team> {
    return this.http.get<Team>(`${this.apiUrl}/${teamId}`);
  }
  // In team.service.ts
  getPlayers(teamId: number): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}/${teamId}/players`);
  }

  // Ottieni i giocatori disponibili per l'acquisto
  getAvailablePlayers(teamId: number): Observable<{ selectedTeam: Team, availablePlayers: Player[] }> {
    return this.http.get<{ selectedTeam: Team, availablePlayers: Player[] }>(`${this.apiUrl}/BuyPlayers/${teamId}`);
  }

  // Acquista un giocatore
  buyPlayer(teamId: number, playerId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/BuyPlayer`, { teamId, playerId });
  }

  // Vendi un giocatore
sellPlayer(sellingTeamId: number, playerId: number, buyingTeamId?: number): Observable<void> {
  const requestBody = { sellingTeamId, playerId, buyingTeamId };
  return this.http.post<void>(`${this.apiUrl}/SellPlayer`, requestBody);
}


  // Crea un nuovo team
  createTeam(team: Team): Observable<Team> {
    return this.http.post<Team>(this.apiUrl, team);
  }

  // Modifica un team
  updateTeam(team: Team): Observable<Team> {
    return this.http.put<Team>(`${this.apiUrl}/${team.id}`, team);
  }

  // Cancella un team
  deleteTeam(teamId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${teamId}`);
  }
  // Method to get both team and available players
  getTeamAndAvailablePlayers(teamId: number): Observable<{ selectedTeam: Team; availablePlayers: Player[] }> {
    return this.http.get<{ selectedTeam: Team; availablePlayers: Player[] }>(`${this.apiUrl}/TeamAndAvailablePlayers/${teamId}`);
  }

}
