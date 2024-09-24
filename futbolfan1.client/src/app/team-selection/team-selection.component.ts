import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TeamService } from '../services/team.service';
import { Team } from '../model/team'; // Assicurati di avere il modello Team
import { Save } from '../model/Save'; // Assicurati di avere il modello Save

@Component({
  selector: 'app-team-selection',
  templateUrl: './team-selection.component.html',
  styleUrls: ['./team-selection.component.scss']
})
export class TeamSelectionComponent implements OnInit {
  teams: Team[] = []; // Modello Team per la lista di squadre
  saves: { [teamId: number]: { id: number; name: string; newName?: string }[] } = {}; // Modello per i salvataggi
  errorMessage: string = '';

  constructor(private teamService: TeamService, private router: Router) { }

  ngOnInit(): void {
    this.getTeams();
    this.loadSaves(); // Carica i salvataggi all'inizio
  }

  // Metodo per ottenere le squadre
  getTeams(): void {
    this.teamService.getTeams().subscribe(
      (teams: Team[]) => this.teams = teams,
      (error: string) => this.errorMessage = 'Errore nel caricamento delle squadre'
    );
  }

  // Metodo per caricare i salvataggi
  loadSaves(): void {
    this.teamService.getSaves().subscribe(
      (saves: Save[]) => {
        console.log('Salvataggi ricevuti:', saves); // Log dei salvataggi ricevuti
        // Organizza i salvataggi per teamId
        saves.forEach(save => {
          if (!this.saves[save.teamId]) {
            this.saves[save.teamId] = [];
          }
          this.saves[save.teamId].push({ id: save.id, name: save.name, newName: '' });
        });
      },
      (error: string) => this.errorMessage = 'Errore nel caricamento dei salvataggi'
    );
  }

  // Metodo per rinominare un salvataggio
  renameSave(teamId: number, saveId: number, newName: string | undefined): void {
    if (newName?.trim()) {
      this.teamService.renameSave(saveId, newName).subscribe(
        () => {
          const savesForTeam = this.saves[teamId]; // Ottieni i salvataggi per la squadra
          const save = savesForTeam?.find(s => s.id === saveId); // Trova il salvataggio
          if (save) {
            save.name = newName;
            save.newName = ''; // Resetta il campo di nuova denominazione
          }
          this.errorMessage = '';
        },
        (error: string) => this.errorMessage = 'Errore nel rinominare il salvataggio'
      );
    } else {
      this.errorMessage = 'Il nuovo nome del salvataggio non può essere vuoto';
    }
  }

  // Metodo per caricare un salvataggio
  loadSave(saveId: number): void {
    this.teamService.loadSave(saveId).subscribe(
      (team: Team) => {
        // Aggiorna la squadra
        const index = this.teams.findIndex(t => t.id === team.id);
        if (index !== -1) {
          this.teams[index] = team; // Aggiorna i dati della squadra
        } else {
          this.teams.push(team); // Aggiungi la squadra
        }

        // Assicurati di gestire i giocatori
        console.log('Giocatori caricati:', team.players);
        // Qui puoi gestire i dati dei giocatori se necessario

        this.router.navigate(['/team-details', team.id]);
      },
      (error: string) => this.errorMessage = 'Errore nel caricamento del salvataggio'
    );
  }

  // Metodo per eliminare un salvataggio
  deleteSave(teamId: number, saveId: number): void {
    this.teamService.deleteSave(saveId).subscribe(
      () => {
        // Rimuovi il salvataggio dalla lista locale dopo l'eliminazione
        this.saves[teamId] = this.saves[teamId].filter(save => save.id !== saveId);
        this.errorMessage = ''; // Resetta eventuali messaggi di errore
      },
      (error: string) => {
        this.errorMessage = 'Errore nell\'eliminazione del salvataggio';
      }
    );
  }

}
