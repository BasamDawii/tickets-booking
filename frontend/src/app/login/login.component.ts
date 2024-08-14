import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {WebsocketService} from "../services/websocket.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  username: string = '';

  constructor(private router: Router, private wsService: WebsocketService) {}

  signIn() {
    if (!this.username) {
      alert('Please enter your name.');
      return;
    }

    this.wsService.connect(this.username);
    this.router.navigate(['/ticket']);
  }
}
