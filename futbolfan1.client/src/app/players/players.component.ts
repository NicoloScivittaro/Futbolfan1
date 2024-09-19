import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Player } from '../model/player';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss'],
})
export class PlayersComponent implements OnInit {
  players: Player[] = [];
  teamId: number = 1; // TeamId fisso per esempio

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadPlayers();
  }

  loadPlayers(): void {
    this.apiService.getPlayersByTeam(this.teamId).subscribe((data) => {
      this.players = data;
    });
  }
}
