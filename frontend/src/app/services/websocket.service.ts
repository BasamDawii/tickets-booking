import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
  private socket!: WebSocket;
  private subject = new Subject<number>();

  connect(username: string) {
    this.socket = new WebSocket('ws://localhost:8181');

    this.socket.onopen = () => {
      const signInMessage = {
        eventType: 'ClientWantsToSignInWithName',
        Name: username,
      };
      this.socket.send(JSON.stringify(signInMessage));
    };

    this.socket.onmessage = (event) => {
      const message = event.data;
      const price = parseFloat(message.replace(/[^0-9.-]+/g, ''));
      this.subject.next(price);
    };

    this.socket.onclose = () => {
      console.log('Disconnected from WebSocket');
    };

    this.socket.onerror = (error) => {
      console.error('WebSocket Error: ' + error);
    };
  }

  onMessage(): Observable<number> {
    return this.subject.asObservable();
  }

  buyTicket() {
    const buyTicketMessage = {
      eventType: 'ClientWantsToBuyTicket',
      UserName: 'username',
    };
    this.socket.send(JSON.stringify(buyTicketMessage));
  }

  refundTicket() {
    const refundTicketMessage = {
      eventType: 'ClientWantsToRefundTicket',
      UserName: 'username',
    };
    this.socket.send(JSON.stringify(refundTicketMessage));
  }
}
