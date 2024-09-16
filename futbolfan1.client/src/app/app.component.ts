import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';
import { Team } from './model/team';
import { Player } from './model/player';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'FutbolFan';
  teams: Team[] = [];
  players: Player[] = [];
  selectedTeam: Team | null = null;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(): void {
    this.apiService.getTeams().subscribe({
      next: (teams: Team[]) => {
        this.teams = teams;
      },
      error: (err) => {
        console.error('Errore nel recuperare le squadre:', err);
      },
    });
  }

  getPlayersByTeam(teamId: number): void {
    this.apiService.getPlayersByTeam(teamId).subscribe({
      next: (players: Player[]) => {
        if (players.length === 0) {
          console.log('Nessun giocatore trovato per la squadra con ID:', teamId);
        } else {
          console.log('Giocatori trovati:', players);
        }
        this.players = players;
      },
      error: (err) => {
        console.error('Errore nel recuperare i giocatori:', err);
      },
    });
  }



  onSelectTeam(team: Team): void {
    this.selectedTeam = team;
    this.getPlayersByTeam(team.id); // Carica i giocatori della squadra selezionata
  }

  clearSelection(): void {
    this.selectedTeam = null;
    this.players = []; // Svuota l'elenco dei giocatori
  }
}
