import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormClientDevisClassicComponent } from './form-client-devis-classic.component';

describe('FormClientDevisClassicComponent', () => {
  let component: FormClientDevisClassicComponent;
  let fixture: ComponentFixture<FormClientDevisClassicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormClientDevisClassicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormClientDevisClassicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
