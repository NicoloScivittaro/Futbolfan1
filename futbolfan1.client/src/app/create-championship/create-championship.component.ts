import { Component } from '@angular/core';
import { ChampionshipService } from '../services/ChampionshipService';
import { Router } from '@angular/router';
import { Championship } from '../model/Championship';

@Component({
  selector: 'app-create-championship',
  templateUrl: './create-championship.component.html',
  styleUrls: ['./create-championship.component.scss']
})
export class CreateChampionshipComponent {
  championship: Championship = {
    id: 0,
    name: '',
    startDate: new Date(),
    endDate: new Date()
  };

  constructor(private championshipService: ChampionshipService, private router: Router) { }

  createChampionship(): void {
    this.championshipService.createChampionship(this.championship).subscribe(() => {
      this.router.navigate(['/championships']);
    });
  }
}
