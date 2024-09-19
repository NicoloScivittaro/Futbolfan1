import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Championship } from '../model/Championship';

@Injectable({
  providedIn: 'root'
})
export class ChampionshipService {
  private apiUrl = 'https://localhost:5001/api/Championships';

  constructor(private http: HttpClient) { }

  // Ottieni tutti i campionati
  getChampionships(): Observable<Championship[]> {
    return this.http.get<Championship[]>(this.apiUrl);
  }

  // Crea un nuovo campionato
  createChampionship(championship: Championship): Observable<Championship> {
    return this.http.post<Championship>(this.apiUrl, championship);
  }

  // Modifica un campionato esistente
  updateChampionship(id: number, championship: Championship): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, championship);
  }

  // Cancella un campionato
  deleteChampionship(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
