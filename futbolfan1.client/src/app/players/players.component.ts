import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../services/PlayerService';
import { Player } from '../model/player';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss'],
})
export class PlayersComponent implements OnInit {
  players: Player[] = [];
  teamId: number | null = null; // Imposta a null inizialmente

  constructor(private playerService: PlayerService) { }

  ngOnInit(): void {
    this.loadPlayers();
  }

  loadPlayers(): void {
    if (this.teamId) {
      this.playerService.getPlayersByTeam(this.teamId).subscribe((data) => {
        this.players = data;
      });
    } else {
      this.playerService.getAllPlayers().subscribe((data) => {
        this.players = data; // Carica tutti i giocatori se teamId è null
      });
    }
  }
}
