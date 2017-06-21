import { Component, OnInit } from '@angular/core';
import {Workplace} from '../../_interfaces/workplace.interface';

@Component({
  selector: 'app-form-work-place-devis-classic',
  templateUrl: './form-work-place-devis-classic.component.html',
  styleUrls: ['./form-work-place-devis-classic.component.css']
})
export class FormWorkPlaceDevisClassicComponent implements OnInit {

  public workplace: Workplace;

  constructor() { }


  ngOnInit() {
    this.workplace = {
      id: 1,
      adress: '',
      postalCode: '',
      city: '',
      phoneNumber: '',
      faxNumber: '',
      email : '',
      phoneContactNumber: '',
      date: ''
    };
  }

  save(isValid: boolean, f: Workplace) {
    if (!isValid) return;
    console.log(f);
  }

}
