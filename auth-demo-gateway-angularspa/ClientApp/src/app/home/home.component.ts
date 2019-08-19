import { Component, OnInit } from '@angular/core';
import { HttpService } from '../shared/http/http.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public name: string;

  constructor(private _http: HttpService) {

  }

  ngOnInit() {
    this.initialize();
  }

  private initialize(): void {
    this._http.get("http://localhost:8080/angularspa/user").subscribe((response: any) => {
      this.name = response.displayName;
    });
  }
}
