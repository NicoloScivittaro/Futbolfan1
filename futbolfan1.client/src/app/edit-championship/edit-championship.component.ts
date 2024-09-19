import { Component, OnInit } from '@angular/core';
import { ChampionshipService } from '../services/ChampionshipService';
import { Championship } from '../model/Championship';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-championship',
  templateUrl: './edit-championship.component.html',
  styleUrls: ['./edit-championship.component.scss']
})
export class EditChampionshipComponent implements OnInit {
  championship: Championship = {
    id: 0,
    name: '',
    startDate: new Date(),
    endDate: new Date(),
  };

  constructor(
    private championshipService: ChampionshipService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.championshipService.getChampionships().subscribe((championships) => {
      const selectedChampionship = championships.find(c => c.id === id);
      if (selectedChampionship) {
        this.championship = selectedChampionship;
      }
    });
  }

  updateChampionship(): void {
    this.championshipService.updateChampionship(this.championship.id, this.championship)
      .subscribe(() => {
        this.router.navigate(['/championships']);
      });
  }
}
