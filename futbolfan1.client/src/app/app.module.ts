import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module'; // Importa AppRoutingModule
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { TeamSelectionComponent } from './team-selection/team-selection.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { TeamListComponent } from './team-list/team-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TeamSelectionComponent,
    PlayerListComponent,
    TeamListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule  // Usa AppRoutingModule per gestire le rotte
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
