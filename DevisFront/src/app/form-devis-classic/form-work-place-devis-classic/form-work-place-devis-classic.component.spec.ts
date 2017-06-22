import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormWorkPlaceDevisClassicComponent } from './form-work-place-devis-classic.component';

describe('FormWorkPlaceDevisClassicComponent', () => {
  let component: FormWorkPlaceDevisClassicComponent;
  let fixture: ComponentFixture<FormWorkPlaceDevisClassicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormWorkPlaceDevisClassicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormWorkPlaceDevisClassicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
