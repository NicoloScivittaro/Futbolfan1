import { Component, OnInit } from '@angular/core';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  teams: Team[] = [];

  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    this.loadTeams();
  }

  loadTeams(): void {
    this.teamService.getTeams().subscribe({
      next: (teams: Team[]) => {
        this.teams = teams;
      },
      error: (err) => {
        console.error('Errore durante il caricamento delle squadre:', err);
      }
    });
  }

  buyPlayer(teamId: number, playerId: number): void {
    this.teamService.buyPlayer(teamId, playerId).subscribe({
      next: () => {
        console.log('Giocatore comprato con successo');
      },
      error: (err) => {
        console.error('Errore durante l\'acquisto del giocatore:', err);
      }
    });
  }
}
