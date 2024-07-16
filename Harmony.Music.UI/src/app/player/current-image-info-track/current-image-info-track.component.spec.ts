import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentImageInfoTrackComponent } from './current-image-info-track.component';

describe('CurrentImageInfoTrackComponent', () => {
  let component: CurrentImageInfoTrackComponent;
  let fixture: ComponentFixture<CurrentImageInfoTrackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrentImageInfoTrackComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrentImageInfoTrackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
