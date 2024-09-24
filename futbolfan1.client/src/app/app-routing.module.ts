import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TeamSelectionComponent } from './team-selection/team-selection.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { TeamListComponent } from './team-list/team-list.component';
import { TeamsComponent } from './teams/teams.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { EditTeamComponent } from './edit-team/edit-team.component';
import { CreatePlayerComponent } from './create-player/create-player.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { ChampionshipListComponent } from './championship-list/championship-list.component';
import { CreateChampionshipComponent } from './create-championship/create-championship.component';
import { EditChampionshipComponent } from './edit-championship/edit-championship.component';
import { TeamDetailComponent } from './team-detail/team-detail.component';
import { EditPlayerComponent } from './edit-player/edit-player.component';
import { PlayersComponent } from './players/players.component';

const routes: Routes = [
  { path: '', component: HomeComponent },  // Home page
  { path: 'team-selection', component: TeamSelectionComponent },
  { path: 'players', component: PlayerListComponent },
  { path: 'players1/:id', component: PlayersComponent },
  { path: 'teams', component: TeamsComponent },
  { path: 'team/:id', component: TeamDetailComponent },
  { path: 'team-details/:id', component: TeamDetailComponent },
  { path: 'create-team', component: CreateTeamComponent },
  { path: 'edit-team/:id', component: EditTeamComponent },
  { path: 'edit-player/:id', component: EditPlayerComponent },
  { path: 'create-player', component: CreatePlayerComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'championships', component: ChampionshipListComponent },
  { path: 'create-championship', component: CreateChampionshipComponent },
  { path: 'edit-championship/:id', component: EditChampionshipComponent },
  { path: '**', redirectTo: '' }  // Rotta di fallback
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
