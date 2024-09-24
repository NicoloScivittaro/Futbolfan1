import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PlayerService } from '../services/PlayerService';
import { Player } from '../model/player';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-player',
  templateUrl: './edit-player.component.html',
  styleUrls: ['./edit-player.component.scss']
})
export class EditPlayerComponent implements OnInit {
  playerId!: number;
  player!: Player;
  editPlayerForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private playerService: PlayerService
  ) { }

  ngOnInit(): void {
    this.playerId = +this.route.snapshot.paramMap.get('id')!; // Ensure playerId is never null
    this.createForm();
    this.loadPlayer();
  }

  // Create the form structure with validators
  createForm(): void {
    this.editPlayerForm = this.fb.group({
      name: ['', Validators.required],
      position: ['', Validators.required],
      age: [null, [Validators.required, Validators.min(18), Validators.max(40)]],
      cost: ['', Validators.required],
      salary: ['', Validators.required],
      speed: ['', Validators.required],
      shooting: ['', Validators.required],
      passing: ['', Validators.required],
      dribbling: ['', Validators.required],
      defense: ['', Validators.required],
      physical: ['', Validators.required],
      teamId: [null, Validators.required]
    });
  }

  // Load the player details from the service and patch the form with the current player data
  loadPlayer(): void {
    this.playerService.getPlayerById(this.playerId).subscribe(
      (data: Player) => {
        this.player = data;
        this.editPlayerForm.patchValue(data); // Patch form with player data
      },
      (error) => {
        console.error('Failed to load player', error);
      }
    );
  }

  // Submit the updated player data back to the service
  onSubmit(): void {
    if (this.editPlayerForm.valid) {
      console.log('Form is valid and ready to submit');
      const updatedPlayer: Player = {
        ...this.editPlayerForm.value,
        id: this.playerId // Ensure the ID is set correctly
      };

      this.playerService.updatePlayer(this.playerId, updatedPlayer).subscribe(
        () => {
          this.router.navigate(['/players']); // Navigate to player list after successful update
        },
        (error) => {
          console.error('Failed to update player', error);
        }
      );
    } else {
      console.log('Form is invalid');
      Object.keys(this.editPlayerForm.controls).forEach(key => {
        const controlErrors = this.editPlayerForm.get(key)?.errors;
        if (controlErrors) {
          console.log(`Control: ${key}`, controlErrors);
        }
      });
    }
  }

}
