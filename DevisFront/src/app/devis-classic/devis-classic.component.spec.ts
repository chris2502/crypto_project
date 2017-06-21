import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DevisClassicComponent } from './devis-classic.component';

describe('DevisClassicComponent', () => {
  let component: DevisClassicComponent;
  let fixture: ComponentFixture<DevisClassicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevisClassicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevisClassicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
