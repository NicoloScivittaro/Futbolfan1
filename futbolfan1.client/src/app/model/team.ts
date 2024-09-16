// src/app/models/team.model.ts

import { Player } from './player';

export interface Team {
  id: number;
  name: string;
  coach: string;
  transferBudget: number;
  salaryBudget: number;
  currentFormationId?: number;  // '?' indica che può essere null
  overall: number;
  players: Player[]; // Devi anche definire l'interfaccia Player se non l'hai già fatto
}
