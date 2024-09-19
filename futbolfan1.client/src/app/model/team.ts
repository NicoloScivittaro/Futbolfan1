// src/app/models/team.model.ts
import { Player } from './player';  // Ensure the correct path to Player model

export interface Team {
  id: number;
  name: string;
  coach: string;
  transferBudget: number;
  salaryBudget: number;
  currentFormationId?: number;  // Optional field
  overall: number;  // Calculated field, likely filled later
  players: Player[];  // Players associated with the team
}
