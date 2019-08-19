import { Observable } from 'rxjs/Rx'
import { HttpClient } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";

@Injectable()
export class HttpService {
  constructor(private http: HttpClient) { }

  public get(url: string) {
    return this.http.get(`${url}`).map(response => {
      return response;
    }).catch(response => (Observable.throw(response)
    ));
  }

}
