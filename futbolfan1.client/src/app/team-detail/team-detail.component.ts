import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team';
import { Player } from '../model/player';

@Component({
  selector: 'app-team-detail',
  templateUrl: './team-detail.component.html',
  styleUrls: ['./team-detail.component.scss']
})
export class TeamDetailComponent implements OnInit {
  teamId!: number;
  team: Team | null = null;
  availablePlayers: Player[] = [];
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private route: ActivatedRoute, private teamService: TeamService) { }

  ngOnInit(): void {
    this.teamId = +this.route.snapshot.paramMap.get('id')!;
    this.loadTeamDetails();
  }

  loadTeamDetails(): void {
    // Fetch team details
    this.teamService.getTeam(this.teamId).subscribe(
      (team: Team) => this.team = team,
      (error: any) => this.errorMessage = 'Error loading team details'
    );

    // Fetch available players
    this.teamService.getAvailablePlayers(this.teamId).subscribe(
      (response: { selectedTeam: Team; availablePlayers: Player[] }) => {
        // Assuming API returns both the team and available players in a single response
        this.team = response.selectedTeam;
        this.availablePlayers = response.availablePlayers;
      },
      (error: any) => this.errorMessage = 'Error loading available players'
    );
  }


  buyPlayer(playerId: number): void {
    this.teamService.buyPlayer(this.teamId, playerId).subscribe(
      () => {
        this.successMessage = 'Player bought successfully!';
        this.loadTeamDetails();
      },
      (error: any) => this.errorMessage = 'Error buying player'
    );
  }

  sellPlayer(playerId: number): void {
    this.teamService.sellPlayer(this.teamId, playerId).subscribe(
      () => {
        this.successMessage = 'Player sold successfully!';
        this.loadTeamDetails();
      },
      (error: any) => {
        this.errorMessage = 'Error selling player: ' + (error.error?.message || 'Unknown error');
      }
    );
  }


}
