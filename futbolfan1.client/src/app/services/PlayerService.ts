import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Player } from '../model/player';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private apiUrl = 'https://localhost:7293/api/players';  // URL dell'API per i giocatori

  constructor(private http: HttpClient) { }

  createPlayer(player: Player): Observable<any> {
    return this.http.post(this.apiUrl, player);
  }
}
