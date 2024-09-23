import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-team-selection',
  templateUrl: './team-selection.component.html',
  styleUrls: ['./team-selection.component.scss']
})
export class TeamSelectionComponent implements OnInit {
  teams: any[] = [];
  saves: { [teamId: number]: { id: number; name: string; newName?: string }[] } = {};
  errorMessage: string = '';

  constructor(private teamService: TeamService, private router: Router) { }

  ngOnInit(): void {
    this.getTeams();
    this.loadSaves(); // Carica i salvataggi all'inizio
  }

  // Metodo per ottenere le squadre
  getTeams(): void {
    this.teamService.getTeams().subscribe(
      (teams: any[]) => this.teams = teams,
      (error: any) => this.errorMessage = 'Errore nel caricamento delle squadre'
    );
  }

  // Metodo per caricare i salvataggi
  loadSaves(): void {
    this.teamService.getSaves().subscribe(
      (saves: { id: number; name: string; teamId: number }[]) => {
        console.log('Salvataggi ricevuti:', saves); // Log dei salvataggi ricevuti
        // Organizza i salvataggi per teamId
        saves.forEach(save => {
          if (!this.saves[save.teamId]) {
            this.saves[save.teamId] = [];
          }
          this.saves[save.teamId].push({ id: save.id, name: save.name, newName: '' });
        });
      },
      (error: any) => this.errorMessage = 'Errore nel caricamento dei salvataggi'
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
        (error: any) => this.errorMessage = 'Errore nel rinominare il salvataggio'
      );
    } else {
      this.errorMessage = 'Il nuovo nome del salvataggio non può essere vuoto';
    }
  }

  loadSave(saveId: number): void {
    this.teamService.loadSave(saveId).subscribe(
      (team: any) => {
        console.log('Caricamento del salvataggio:', team);

        // Aggiorna lo stato della squadra caricata
        const index = this.teams.findIndex(t => t.id === team.id);
        if (index !== -1) {
          // Se la squadra esiste già nella lista, aggiorna i suoi dati
          this.teams[index] = team;
        } else {
          // Se la squadra non è nella lista, la aggiungi
          this.teams.push(team);
        }

        // Se vuoi anche gestire i giocatori, assicurati di aggiornare i dati dei giocatori
        // Puoi farlo in questo modo, se i giocatori sono già parte dell'oggetto team
        const players = team.players || []; // Assicurati che ci sia un array di giocatori
        console.log('Giocatori caricati:', players);
        // Qui puoi aggiungere logica per gestire i giocatori, se necessario

        // Naviga alla pagina dei dettagli della squadra
        this.router.navigate(['/team-details', team.id]);
      },
      (error: any) => this.errorMessage = 'Errore nel caricamento del salvataggio'
    );
  }


  }
