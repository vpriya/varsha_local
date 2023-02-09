import { Component, OnInit } from '@angular/core';
import { Card } from './models/card.model';
import { CardsService } from './service/cards.service';
import { FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'cards';
  cards: Card[] = [];

  card: Card = {
    id: '',
    cardNumber: '',
    cardholderName: '',
    expiryMonth: '',
    expiryYear: '',
    cvc:  ''
  }

  // cardForm = new FormGroup({
  //   numberControl: new FormControl('', Validators.required),
  // })

  constructor(private cardsService: CardsService){

  }

  ngOnInit(): void {
    this.getAllCards();
  }

  getAllCards() {
    this.cardsService.getAllCards()
    .subscribe(
      response => {
        this.cards = response;
        console.log(response);
      }
    );
  }

  onSubmit (){
    if (this.card.id === '') {
      this.cardsService.addCard(this.card)
        .subscribe(
          response => {
            this.getAllCards();
            this.card = {
              id: '',
              cardNumber: '',
              cardholderName: '',
              expiryMonth: '',
              expiryYear: '',
              cvc:  ''
            };
            //console.log(response);
          }
        )

    } else {
      this.updateCard(this.card);
    }

    //console.log(this.card);
    
  }
  
  deleteCard(id: string) {
  this.cardsService.deleteCard(id)
  .subscribe(
    response => {
      this.getAllCards();
      this.card = {
        id: '',
        cardNumber: '',
        cardholderName: '',
        expiryMonth: '',
        expiryYear: '',
        cvc:  ''
      };
    }
  );
  }

  populateForm(card: Card) {
    this.card = card;
  }

  updateCard(card: Card) {
    this.cardsService.updateCard(card)
    .subscribe(
      response => {
        this.getAllCards();
      }
    );
  }

}


