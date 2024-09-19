import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PlayerService } from '../services/PlayerService';  // Ensure the path is correct
import { Player } from '../model/player';

@Component({
  selector: 'app-create-player',
  templateUrl: './create-player.component.html',
  styleUrls: ['./create-player.component.scss']
})
export class CreatePlayerComponent {
  newPlayer: Player = {
    id: 0,
    name: '',
    position: '',
    age: 0,
    teamId: 0,
    cost: 0,
    salary: 0,
    isStarting: false,
    speed: 0,
    shooting: 0,
    passing: 0,
    dribbling: 0,
    defense: 0,
    physical: 0,
    role: ''  // Aggiungi la proprietà 'role'
  };

  constructor(private playerService: PlayerService, private router: Router) { }

  createPlayer(): void {
    this.playerService.createPlayer(this.newPlayer).subscribe(() => {
      this.router.navigate(['/players']);  // Redirect to player list or another page after creation
    });
  }

  // Metodo per calcolare la valutazione complessiva del giocatore
  calculateOverall(): number {
    const stats = [
      this.newPlayer.speed,
      this.newPlayer.shooting,
      this.newPlayer.passing,
      this.newPlayer.dribbling,
      this.newPlayer.defense,
      this.newPlayer.physical
    ];
    const total = stats.reduce((acc, stat) => acc + stat, 0);
    return total / stats.length;
  }
}
