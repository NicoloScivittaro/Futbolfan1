import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router'; // <-- Import RouterModule
import { HttpClientModule } from '@angular/common/http';  // Importa il modulo HTTP
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { TeamSelectionComponent } from './team-selection/team-selection.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { TeamListComponent } from './team-list/team-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { EditTeamComponent } from './edit-team/edit-team.component';
import { EditPlayerComponent } from './edit-player/edit-player.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { CreatePlayerComponent } from './create-player/create-player.component';
import { TeamsComponent } from './teams/teams.component';
import { FormsModule } from '@angular/forms'; // Import FormsModule here
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { CreateChampionshipComponent } from './create-championship/create-championship.component';
import { ChampionshipListComponent } from './championship-list/championship-list.component';
import { EditChampionshipComponent } from './edit-championship/edit-championship.component';
import { TeamDetailComponent } from './team-detail/team-detail.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TeamsComponent,
    TeamSelectionComponent,
    ChampionshipListComponent,
    PlayerListComponent,
    CreatePlayerComponent,
    TeamListComponent,
    TeamDetailComponent,
    NavbarComponent,
    EditTeamComponent,
    RegisterComponent,
    LoginComponent,
    ProfileComponent,
    CreateTeamComponent,
    CreateChampionshipComponent,
    EditPlayerComponent,
    EditChampionshipComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule, // Make sure this is imported
    RouterModule, // Make sure RouterModule is imported
    HttpClientModule
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
