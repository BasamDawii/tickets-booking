import { Component, OnInit } from '@angular/core';
import {WebsocketService} from "../services/websocket.service";

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss'],
})
export class TicketComponent {
  lastPrice: number = 0;
  currentPrice: number = 0;
  priceArrow: string = '';

  constructor(private wsService: WebsocketService) {
    this.wsService.onMessage().subscribe((newPrice: number) => {
      this.updatePrice(newPrice);
    });
  }

  updatePrice(newPrice: number) {
    if (newPrice > this.lastPrice) {
      this.priceArrow = '↑';
    } else if (newPrice < this.lastPrice) {
      this.priceArrow = '↓';
    } else {
      this.priceArrow = '';
    }

    this.lastPrice = newPrice;
    this.currentPrice = newPrice;
  }

  buyTicket() {
    this.wsService.buyTicket();
  }

  refundTicket() {
    this.wsService.refundTicket();
  }
}
