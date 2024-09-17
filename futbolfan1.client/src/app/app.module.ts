import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';  // Importa il modulo HTTP
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
    AppRoutingModule,
    HttpClientModule  // Importa HttpClientModule per le chiamate API
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
