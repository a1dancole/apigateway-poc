import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BroadcastService, MsalService } from '@azure/msal-angular';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

    constructor(private auth: MsalService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const scopes = this.auth.getScopesForEndpoint(req.url);
        if (scopes === null) {
            return next.handle(req);
        }

        const tokenStored = this.auth.getCachedTokenInternal(scopes);
        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${tokenStored.token}`,
            }
        });
        return next.handle(req).do(event => { }, err => { });
    }
}