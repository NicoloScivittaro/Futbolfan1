import { Component } from '@angular/core';
import { PlayerService } from '../services/PlayerService';
import { Player } from '../model/player';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.scss']
})
export class PlayerListComponent {
  players: Player[] = [];
  playerSearch: string = '';
  maxAge: number | null = null;
  maxSalary: number | null = null;
  maxCost: number | null = null;

  constructor(private playersService: PlayerService) { }

  ngOnInit(): void {
    this.playersService.getAllPlayers().subscribe(
      (data: Player[]) => {
        console.log(data);  // Log the players data
        this.players = data;
      },
      (error) => {
        console.error('There was an error!', error);
      }
    );
  }

  getPlayerImage(player: Player): string {
    // Return a placeholder or construct a URL based on player ID or name
    return 'assets/images/player-placeholder.png';
  }

  calculateOverall(player: Player): number {
    const stats = [player.speed, player.shooting, player.passing, player.dribbling, player.defense, player.physical];
    const sum = stats.reduce((acc, stat) => acc + stat, 0);
    return sum / stats.length;  // Average of all stats
  }

  // Funzione di filtraggio dei giocatori
  filteredPlayers(): Player[] {
    return this.players.filter(player => {
      const matchesSearch = player.name.toLowerCase().includes(this.playerSearch.toLowerCase());
      const matchesAge = this.maxAge ? player.age <= this.maxAge : true;
      const matchesSalary = this.maxSalary ? player.salary <= this.maxSalary : true;
      const matchesCost = this.maxCost ? player.cost <= this.maxCost : true;
      return matchesSearch && matchesAge && matchesSalary && matchesCost;
    });
  }
}
