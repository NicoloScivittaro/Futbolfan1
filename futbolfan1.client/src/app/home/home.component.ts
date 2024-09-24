import { Component, OnInit } from '@angular/core';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team';
import { Player } from '../model/player';  // Importa il modello Player

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  title = 'Futbolfan';
  teams: Team[] = [];  // Usa il tipo Team
  players: Player[] = [];  // Usa il tipo Player
  selectedTeam: Team | null = null;  // Usa il tipo Team o null

  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    this.loadTeams();
  }

  // Carica le squadre e usa il tipo Team per i dati ricevuti
  loadTeams(): void {
    this.teamService.getTeams().subscribe((teams: Team[]) => {
      this.teams = teams;
    });
  }

  // Usa il tipo Team per il parametro team e Player[] per i giocatori
  onSelectTeam(team: Team): void {
    this.selectedTeam = team;
    this.teamService.getPlayers(team.id).subscribe((players: Player[]) => {
      this.players = players;
    });
  }

  // Funzione per resettare la selezione della squadra
  clearSelection(): void {
    this.selectedTeam = null;
    this.players = [];
  }
}
