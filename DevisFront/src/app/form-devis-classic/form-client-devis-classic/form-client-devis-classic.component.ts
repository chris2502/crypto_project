import { Component, OnInit } from '@angular/core';
import {Client} from '../../_interfaces/client.interface';

@Component({
  selector: 'app-form-client-devis-classic',
  templateUrl: './form-client-devis-classic.component.html',
  styleUrls: ['./form-client-devis-classic.component.css']
})
export class FormClientDevisClassicComponent implements OnInit {

  public client: Client;
  constructor() { }

  ngOnInit() {
    this.client = {
      id: 1,
      siteName: 'ESGI',
      sector: 'vers Paris',
      address: 'je sais pas',
      postalCode: '75012',
      city: 'ESGI',
      phone: '0123456789',
      fax: '0123242344',
      mail: 'esgi@christian.fr',
      interlocutor: 'christiannnnnn'
    };
  }

  save(isValid: boolean, f: Client) {
    if (!isValid) return;
    console.log(f);
  }

}


