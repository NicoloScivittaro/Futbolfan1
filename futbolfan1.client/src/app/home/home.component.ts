import { Component, OnInit } from '@angular/core';
import { TeamService } from '../services/team.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title = 'Futbolfan';
  teams: any[] = [];
  players: any[] = [];
  selectedTeam: any = null;

  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    this.loadTeams();
  }

  loadTeams(): void {
    this.teamService.getTeams().subscribe((teams: any[]) => {
      this.teams = teams;
    });
  }

  onSelectTeam(team: any): void {
    this.selectedTeam = team;
    this.teamService.getPlayers(team.id).subscribe((players: any[]) => {
      this.players = players;
    });
  }

  clearSelection(): void {
    this.selectedTeam = null;
    this.players = [];
  }
}
