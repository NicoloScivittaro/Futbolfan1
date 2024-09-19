import { Component, OnInit } from '@angular/core';
import { Team } from '../model/team';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.scss']
})
export class TeamsComponent implements OnInit {
  teams: Team[] = [];

  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    this.loadTeams();  // Load teams when the component initializes
  }

  loadTeams(): void {
    this.teamService.getTeams().subscribe((teams: Team[]) => {
      this.teams = teams;
    });
  }

  deleteTeam(teamId: number): void {
    this.teamService.deleteTeam(teamId).subscribe(() => {
      this.loadTeams();  // Refresh the team list after deletion
    });
  }
}
