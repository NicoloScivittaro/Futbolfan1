import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateChampionshipComponent } from './create-championship.component';

describe('CreateChampionshipComponent', () => {
  let component: CreateChampionshipComponent;
  let fixture: ComponentFixture<CreateChampionshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateChampionshipComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateChampionshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
