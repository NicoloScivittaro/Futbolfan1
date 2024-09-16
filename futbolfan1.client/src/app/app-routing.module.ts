import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TeamSelectionComponent } from './team-selection/team-selection.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { TeamListComponent } from './team-list/team-list.component';

const routes: Routes = [
  { path: '', component: HomeComponent },  // Pagina principale
  { path: 'team-selection', component: TeamSelectionComponent },  // Selezione squadra
  { path: 'players', component: PlayerListComponent },  // Lista giocatori
  { path: 'teams', component: TeamListComponent },  // Lista squadre
  { path: '**', redirectTo: '' },  // Rotta di fallback in caso di percorso non trovato
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
