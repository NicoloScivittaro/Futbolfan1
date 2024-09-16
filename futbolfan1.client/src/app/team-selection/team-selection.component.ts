import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';  // Cambia il percorso in base alla struttura del progetto


@Component({
  selector: 'app-team-selection',
  templateUrl: './team-selection.component.html',
  styleUrls: ['./team-selection.component.scss']
})
export class TeamSelectionComponent implements OnInit {
  teams: any[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getTeams().subscribe(data => {
      this.teams = data;
    });
  }
}
