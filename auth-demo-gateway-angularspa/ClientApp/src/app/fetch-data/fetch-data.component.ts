import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../shared/http/http.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(private _http: HttpService) {
    _http.get('http://localhost:8080/angularspa/sampledata/weatherforecasts').subscribe((response: WeatherForecast[]) => {
      this.forecasts = response;
    })
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
