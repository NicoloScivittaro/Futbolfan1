import { Component, OnInit } from '@angular/core';
import { ChampionshipService } from '../services/ChampionshipService';
import { Championship } from '../model/Championship';

@Component({
  selector: 'app-championship-list',
  templateUrl: './championship-list.component.html',
  styleUrls: ['./championship-list.component.scss']
})
export class ChampionshipListComponent implements OnInit {
  championships: Championship[] = [];

  constructor(private championshipService: ChampionshipService) { }

  ngOnInit(): void {
    this.loadChampionships();
  }

  loadChampionships(): void {
    this.championshipService.getChampionships().subscribe(data => {
      this.championships = data;
    });
  }

  deleteChampionship(id: number): void {
    this.championshipService.deleteChampionship(id).subscribe(() => {
      this.loadChampionships();
    });
  }
}
