import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormDevisClassicComponent } from './form-devis-classic.component';

describe('FormDevisClassicComponent', () => {
  let component: FormDevisClassicComponent;
  let fixture: ComponentFixture<FormDevisClassicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormDevisClassicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormDevisClassicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
