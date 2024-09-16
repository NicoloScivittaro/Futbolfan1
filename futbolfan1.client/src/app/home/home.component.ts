import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service'; // Assicurati che questo servizio sia configurato correttamente
import { TeamService } from '../services/team.service'; // Assicurati che questo servizio sia configurato correttamente

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title = 'FutbolFan';
  teams: any[] = [];
  players: any[] = [];
  selectedTeam: any = null;

  constructor(private apiService: ApiService, private teamService: TeamService) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(): void {
    this.apiService.getTeams().subscribe({
      next: (teams: any[]) => {
        console.log('Squadre ricevute:', teams);
        this.teams = teams;
      },
      error: (err: any) => {
        console.error('Errore nel recuperare le squadre:', err);
      }
    });
  }

  onSelectTeam(team: any): void {
    this.selectedTeam = team;
    this.teamService.getPlayers(team.id).subscribe(players => {
      this.players = players;
    });
  }

  clearSelection(): void {
    this.selectedTeam = null;
    this.getPlayers(); // Ricarica tutti i giocatori se necessario
  }

  getPlayers(): void {
    this.apiService.getPlayers().subscribe((data: any[]) => {
      this.players = data;
    });
  }
}
