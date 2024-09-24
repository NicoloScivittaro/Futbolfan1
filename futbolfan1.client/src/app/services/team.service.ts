import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Team } from '../model/team';
import { Player } from '../model/player';
import { Save } from '../model/Save'; // Assumiamo che tu abbia un'interfaccia Save

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

  // Ottieni i giocatori di un team
  getPlayers(teamId: number): Observable<Player[]> {
    return this.http.get<Player[]>(`${this.apiUrl}/${teamId}/players`);
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
          console.log(`Player ${playerId} bought for team ${teamId}`);
        })
      );
  }

  // Vendi un giocatore
  sellPlayer(sellingTeamId: number, playerId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/SellPlayer`, { sellingTeamId, playerId })
      .pipe(
        tap(() => {
          console.log(`Player ${playerId} sold from team ${sellingTeamId}`);
        })
      );
  }

  // Crea un nuovo team
  createTeam(team: Team): Observable<Team> {
    return this.http.post<Team>('https://localhost:7293/api/teams', team);
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
  saveTeamState(saveRequest: { teamId: number; saveName: string }): Observable<Save> {
    return this.http.post<Save>(`${this.apiUrl}/SaveTeam`, saveRequest);
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
  getSaves(): Observable<Save[]> {
    return this.http.get<Save[]>(`${this.apiUrl}/GetSaves`);
  }

  // Rinominare un salvataggio
  renameSave(saveId: number, newName: string): Observable<Save> {
    return this.http.put<Save>(`${this.apiUrl}/RenameSave/${saveId}`, { newName });
  }

  // Metodo per caricare un salvataggio
  loadTeamSave(saveId: number): Observable<Save> {
    return this.http.get<Save>(`${this.apiUrl}/LoadTeamSave/${saveId}`);
  }
  loadSave(saveId: number): Observable<Team> {
    return this.http.get<Team>(`${this.apiUrl}/LoadTeamSave/${saveId}`);
  }
  // Elimina un salvataggio
  deleteSave(saveId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteSave/${saveId}`);
  }

}
