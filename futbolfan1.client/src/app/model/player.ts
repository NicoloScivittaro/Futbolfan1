// src/app/models/player.model.ts

export interface Player {
  id: number;
  name: string;
  position: string;
  age: number;
  cost: number;
  salary: number;
  role?: string;  // Ruolo del giocatore, opzionale
  isStarting: boolean;  // Se il giocatore è titolare
  teamId?: number;  // ID della squadra a cui appartiene (opzionale)

  // Statistiche del giocatore
  speed: number;
  shooting: number;
  passing: number;
  dribbling: number;
  defense: number;
  physical: number;

  // Overall rating
  overall: number;  // Media delle statistiche principali
}
