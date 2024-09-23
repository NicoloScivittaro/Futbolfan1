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
  saveName: string = '';  // Aggiungi una variabile per il nome del salvataggio

  constructor(private route: ActivatedRoute, private teamService: TeamService) { }

  ngOnInit(): void {
    this.teamId = +this.route.snapshot.paramMap.get('id')!;
    this.loadTeamDetails();
  }

  loadTeamDetails(): void {
    this.teamService.getTeam(this.teamId).subscribe(
      (team: Team) => this.team = team,
      (error: any) => this.errorMessage = 'Error loading team details'
    );

    this.teamService.getAvailablePlayers(this.teamId).subscribe(
      (response: { selectedTeam: Team; availablePlayers: Player[] }) => {
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
        this.loadTeamDetails(); // Ricarica i dettagli per aggiornare il budget
      },
      (error: any) => this.errorMessage = 'Error buying player'
    );
  }

  sellPlayer(playerId: number): void {
    this.teamService.sellPlayer(this.teamId, playerId).subscribe(
      () => {
        this.successMessage = 'Player sold successfully!';
        this.loadTeamDetails(); // Ricarica i dettagli per aggiornare il budget
      },
      (error: any) => this.errorMessage = 'Error selling player'
    );
  }
  // Modifica la funzione per utilizzare il nuovo metodo di salvataggio
  saveTeam(): void {
    if (this.saveName) {
      const saveRequest = {
        teamId: this.teamId,
        saveName: this.saveName
      };

      this.teamService.saveTeamState(saveRequest).subscribe(
        (response: any) => {
          this.successMessage = 'Team saved successfully!';
        },
        (error: any) => {
          this.errorMessage = 'Error saving team';
        }
      );
    } else {
      this.errorMessage = 'Please provide a name for the save';
    }
  }
}
