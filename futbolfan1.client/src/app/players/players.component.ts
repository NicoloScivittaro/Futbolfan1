import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlayerService } from '../services/PlayerService';
import { Player } from '../model/player';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss']
})
export class PlayersComponent implements OnInit {
  playerId!: number;
  player!: Player;

  constructor(
    private route: ActivatedRoute,
    private playerService: PlayerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.playerId = +this.route.snapshot.paramMap.get('id')!; // Ensure playerId is never null
    this.loadPlayer();
  }

  // Load player details from the service
  loadPlayer(): void {
    this.playerService.getPlayerById(this.playerId).subscribe(
      (data: Player) => {
        this.player = data;
      },
      (error) => {
        console.error('Failed to load player', error);
      }
    );
  }

  // Navigate back to the player list
  goBack(): void {
    this.router.navigate(['/players']);
  }
  // In players.component.ts
  calculateOverall(player: Player): number {
    if (!player) return 0;

    const { speed, shooting, passing, dribbling, defense, physical } = player;
    const overall = (speed + shooting + passing + dribbling + defense + physical) / 6;
    return Math.round(overall); // Return rounded overall rating
  }
}
