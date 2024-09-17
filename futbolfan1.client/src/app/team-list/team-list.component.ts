import { Component } from '@angular/core';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent {
  teams: any[] = [];  // Assumi che i team siano caricati qui
  selectedTeam: any = null;

  constructor(private teamService: TeamService) { }

  buyPlayer(teamId: number, playerId: number): void {
    this.teamService.buyPlayer(teamId, playerId).subscribe(response => {
      console.log('Player bought successfully:', response);
      // Aggiorna lo stato del team o dei giocatori se necessario
    }, error => {
      console.error('Failed to buy player:', error);
    });
  }
}
