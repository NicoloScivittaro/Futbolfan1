import { Component, OnInit } from '@angular/core';
import { TeamService } from '../services/team.service';
import { Player } from '../model/player';  // Assumi che questa sia l'interfaccia per i giocatori

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  selectedTeam: any;
  players: Player[] = [];

  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    // Inizializza qualcosa se necessario
  }

  onSelectTeam(team: any): void {
    this.selectedTeam = team;
    this.teamService.getPlayers(team.id).subscribe((players: Player[]) => {
      this.players = players;
    });
  }
}
