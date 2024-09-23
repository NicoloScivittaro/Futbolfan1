import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team';

@Component({
  selector: 'app-edit-team',
  templateUrl: './edit-team.component.html',
  styleUrls: ['./edit-team.component.scss']
})
export class EditTeamComponent implements OnInit {
  team: Team = {
    id: 0,
    name: '',
    players: [],
    transferBudget: 0,
    salaryBudget: 0,
    coach: '',  // Add default value for coach
    overall: 0  // Add default value for overall
  };

  constructor(
    private route: ActivatedRoute,
    private teamService: TeamService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const teamId = +this.route.snapshot.paramMap.get('id')!;
    this.loadTeam(teamId);
  }

  loadTeam(teamId: number): void {
    this.teamService.getTeams().subscribe(teams => {
      this.team = teams.find(t => t.id === teamId) || this.team;
    });
  }

  saveTeam(): void {
    this.teamService.updateTeam(this.team.id, this.team).subscribe({
      next: () => {
        this.router.navigate(['/teams']);  // Navigate back to the teams list after saving
      },
      error: (err) => {
        console.error('Error updating team:', err);  // Log error if update fails
      }
    });
  }


  cancel(): void {
    this.router.navigate(['/teams']);
  }

}
