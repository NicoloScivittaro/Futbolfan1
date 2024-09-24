export interface TeamSave {
  id: number;          // L'ID del salvataggio
  name: string;        // Il nome del salvataggio
  teamId: number;      // L'ID del team associato al salvataggio
  createdAt: Date;     // La data in cui è stato creato il salvataggio
  newName?: string;    // Il nuovo nome del salvataggio (opzionale per la rinomina)
}
