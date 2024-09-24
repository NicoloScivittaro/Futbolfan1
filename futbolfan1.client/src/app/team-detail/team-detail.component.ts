import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team';
import { Player } from '../model/player';
import { SaveRequest } from '../model/SaveRequest';

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
  saveName: string = '';
  teamPlayerSearch: string = '';
  availablePlayerSearch: string = '';
  maxAge: number | null = null;
  maxSalary: number | null = null;
  maxCost: number | null = null;
  selectedFormation: Player[] = []; // Array per i giocatori selezionati per la formazione

  constructor(private route: ActivatedRoute, private teamService: TeamService) { }

  ngOnInit(): void {
    this.teamId = +this.route.snapshot.paramMap.get('id')!;
    this.loadTeamDetails();
  }

  loadTeamDetails(): void {
    this.teamService.getTeam(this.teamId).subscribe(
      (team: Team) => this.team = team,
      (error: Error) => this.errorMessage = 'Error loading team details'
    );

    this.teamService.getAvailablePlayers(this.teamId).subscribe(
      (response: { selectedTeam: Team; availablePlayers: Player[] }) => {
        this.team = response.selectedTeam;
        this.availablePlayers = response.availablePlayers;
      },
      (error: Error) => this.errorMessage = 'Error loading available players'
    );
  }

  buyPlayer(playerId: number): void {
    this.teamService.buyPlayer(this.teamId, playerId).subscribe(
      () => {
        this.successMessage = 'Player bought successfully!';
        this.loadTeamDetails();
      },
      (error: Error) => this.errorMessage = 'Error buying player'
    );
  }

  sellPlayer(playerId: number): void {
    this.teamService.sellPlayer(this.teamId, playerId).subscribe(
      () => {
        this.successMessage = 'Player sold successfully!';
        this.loadTeamDetails();
      },
      (error: Error) => this.errorMessage = 'Error selling player'
    );
  }

  saveTeam(): void {
    if (this.saveName) {
      const saveRequest: SaveRequest = {
        teamId: this.teamId,
        saveName: this.saveName
      };

      this.teamService.saveTeamState(saveRequest).subscribe(
        () => {
          this.successMessage = 'Team saved successfully!';
        },
        (error: Error) => {
          this.errorMessage = 'Error saving team';
        }
      );
    } else {
      this.errorMessage = 'Please provide a name for the save';
    }
  }

  // Funzione di filtraggio per i giocatori della squadra
  filteredTeamPlayers(): Player[] {
    return this.team?.players.filter(player => {
      const matchesSearch = player.name.toLowerCase().includes(this.teamPlayerSearch.toLowerCase());
      const matchesAge = this.maxAge ? player.age <= this.maxAge : true;
      const matchesSalary = this.maxSalary ? player.salary <= this.maxSalary : true;
      const matchesCost = this.maxCost ? player.cost <= this.maxCost : true;
      return matchesSearch && matchesAge && matchesSalary && matchesCost;
    }) || [];
  }

  // Funzione di filtraggio per i giocatori disponibili
  filteredAvailablePlayers(): Player[] {
    return this.availablePlayers.filter(player => {
      const matchesSearch = player.name.toLowerCase().includes(this.availablePlayerSearch.toLowerCase());
      const matchesAge = this.maxAge ? player.age <= this.maxAge : true;
      const matchesSalary = this.maxSalary ? player.salary <= this.maxSalary : true;
      const matchesCost = this.maxCost ? player.cost <= this.maxCost : true;
      return matchesSearch && matchesAge && matchesSalary && matchesCost;
    });
  }

  // Metodo per aggiungere/rimuovere giocatori dalla formazione
  toggleFormation(player: Player): void {
    const index = this.selectedFormation.indexOf(player);
    if (index === -1) {
      this.selectedFormation.push(player);
    } else {
      this.selectedFormation.splice(index, 1);
    }
  }

  // Metodo per rimuovere un giocatore dalla formazione
  removeFromFormation(player: Player): void {
    const index = this.selectedFormation.indexOf(player);
    if (index !== -1) {
      this.selectedFormation.splice(index, 1);
    }
  }
}
