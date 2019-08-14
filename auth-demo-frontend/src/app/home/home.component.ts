import { Component, OnInit } from '@angular/core';
import { HttpService } from '../shared/http/http.service';
import { forkJoin } from 'rxjs';

@Component({
  template: `<div style="text-align:center">
              <h1>Hi {{ name }}</h1>
              <h2>Your favourite colour is {{ favouriteColour }}</h2>
            </div>`
})

export class HomeComponent implements OnInit {
  public name: string;
  public favouriteColour: string;

  constructor(private _httpService: HttpService) { }

  ngOnInit() {
    this.initialize()
  }

  private initialize(): void {
    forkJoin(this._httpService.httpGetRequest("http://localhost:8080/userapi/user"),
      this._httpService.httpGetRequest("http://localhost:8080/userapi/user/GetCurrentUsersFavouriteColour")).subscribe(([user, usersFavouriteColor]: [any, any]) => {
        this.name = user.displayName;
        this.favouriteColour = usersFavouriteColor.favouriteColour;
      })
  }
}
