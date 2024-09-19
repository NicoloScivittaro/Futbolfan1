import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'; // Assicurati di importare il Router
import { TeamService } from '../services/team.service'; // Importa il TeamService

@Component({
  selector: 'app-team-selection',
  templateUrl: './team-selection.component.html',
  styleUrls: ['./team-selection.component.scss']
})
export class TeamSelectionComponent implements OnInit {
  teams: any[] = [];

  // Inietta il Router qui
  constructor(private teamService: TeamService, private router: Router) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(): void {
    this.teamService.getTeams().subscribe(teams => {
      this.teams = teams;
    });
  }

  goToPlayers(): void {
    // Utilizza il router per navigare alla pagina dei giocatori
    this.router.navigate(['/players']);
  }

  selectTeam(team: any): void {
    console.log('Team selected:', team);
  }

  buyPlayer(teamId: number, playerId: number): void {
    this.teamService.buyPlayer(teamId, playerId).subscribe(response => {
      console.log('Player bought successfully:', response);
      // Aggiorna lo stato del team o dei giocatori se necessario
    }, error => {
      console.error('Failed to buy player:', error);
    });
  }
}


