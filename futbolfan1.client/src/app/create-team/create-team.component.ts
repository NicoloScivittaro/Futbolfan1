import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrls: ['./create-team.component.scss']
})
export class CreateTeamComponent {
  newTeam: Team = {
    id: 0,
    name: '',
    coach: '',
    transferBudget: 0,
    salaryBudget: 0,
    overall: 0,  // This can be calcolato successivamente
    players: []  // Inizializzato con lista giocatori vuota
  };

  constructor(private teamService: TeamService, private router: Router) { }

  createTeam(): void {
    this.teamService.createTeam(this.newTeam).subscribe({
      next: () => {
        this.router.navigate(['/teams']);  // Reindirizza alla lista dei team dopo la creazione
      },
      error: (err) => {
        console.error('Errore nella creazione del team:', err);  // Log degli errori
      }
    });
  }
}
