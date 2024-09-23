import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Team } from '../model/team';
import { Player } from '../model/player';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private apiUrl = 'https://localhost:7293/api/teams';

  constructor(private http: HttpClient) { }

  // Ottieni la lista di team
  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.apiUrl);
  }

  // Ottieni i dettagli di un team
  getTeam(teamId: number): Observable<Team> {
    return this.http.get<Team>(`${this.apiUrl}/${teamId}`);
  }
  getPlayers(teamId: number): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}/${teamId}/players`);
  }
  // In team.service.ts
  getPlayer(playerId: number): Observable<Player> {
    return this.http.get<Player>(`https://localhost:7293/api/players/${playerId}`);
  }
  // Ottieni i giocatori disponibili per l'acquisto
  getAvailablePlayers(teamId: number): Observable<{ selectedTeam: Team; availablePlayers: Player[] }> {
    return this.http.get<{ selectedTeam: Team; availablePlayers: Player[] }>(`${this.apiUrl}/BuyPlayers/${teamId}`);
  }

  // Acquista un giocatore
  buyPlayer(teamId: number, playerId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/BuyPlayer`, { teamId, playerId })
      .pipe(
        tap(() => {
          // Dopo l'acquisto, non serve gestire il budget qui
          console.log(`Player ${playerId} bought for team ${teamId}`);
        })
      );
  }

  // Vendi un giocatore
  sellPlayer(sellingTeamId: number, playerId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/SellPlayer`, { sellingTeamId, playerId })
      .pipe(
        tap(() => {
          // Dopo la vendita, non serve gestire il budget qui
          console.log(`Player ${playerId} sold from team ${sellingTeamId}`);
        })
      );
  }

  // Crea un nuovo team
  createTeam(team: Team): Observable<Team> {
    return this.http.post<Team>(this.apiUrl, team);
  }

  // Modifica un team
  updateTeam(teamId: number, team: Team): Observable<Team> {
    return this.http.put<Team>(`${this.apiUrl}/${teamId}`, team);
  }


  // Cancella un team
  deleteTeam(teamId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${teamId}`);
  }

  // Ottieni sia il team che i giocatori disponibili
  getTeamAndAvailablePlayers(teamId: number): Observable<{ selectedTeam: Team; availablePlayers: Player[] }> {
    return this.http.get<{ selectedTeam: Team; availablePlayers: Player[] }>(`${this.apiUrl}/TeamAndAvailablePlayers/${teamId}`);
  }

  // Salva lo stato del team
  saveTeamState(saveRequest: { teamId: number; saveName: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/SaveTeam`, saveRequest);
  }

  // Salva un giocatore
  savePlayer(player: Player): Observable<Player> {
    if (player.id) {
      return this.http.put<Player>(`https://localhost:7293/api/players/${player.id}`, player);
    } else {
      return this.http.post<Player>('https://localhost:7293/api/players', player);
    }
  }

  // Ottieni i salvataggi precedenti
  getSaves(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetSaves`);
  }

  // Aggiungi anche il metodo per rinominare un salvataggio se non esiste
  renameSave(saveId: number, newName: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/RenameSave/${saveId}`, { newName });
  }

  // Metodo per caricare un salvataggio
  loadTeamSave(saveId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/LoadTeamSave/${saveId}`);
  }
  loadSave(saveId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/LoadTeamSave/${saveId}`);
  }

}
