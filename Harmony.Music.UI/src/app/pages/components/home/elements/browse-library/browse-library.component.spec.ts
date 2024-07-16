import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrowseLibraryComponent } from './browse-library.component';

describe('BrowseLibraryComponent', () => {
  let component: BrowseLibraryComponent;
  let fixture: ComponentFixture<BrowseLibraryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BrowseLibraryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrowseLibraryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
