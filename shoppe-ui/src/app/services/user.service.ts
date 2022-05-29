import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly userIdkey = 'USER_ID';

  constructor() { }

  getUserId() {
    let userId = localStorage.getItem(this.userIdkey);

    if (!userId) {
      userId = Guid.create().toString();
      localStorage.setItem(this.userIdkey, userId);
    }
    
    return userId;
  }
}
