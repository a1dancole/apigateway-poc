import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MsalModule, MsalGuard } from '@azure/msal-angular';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AuthenticationInterceptor } from "./shared/http/authentication.interceptor"
import { HttpService } from './shared/http/http.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [MsalGuard] },
      { path: 'counter', component: CounterComponent, canActivate: [MsalGuard] },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [MsalGuard] },
    ]),
    MsalModule.forRoot({
      clientID: '492a7463-3a97-44db-b129-5dcd55b4ac43',
      authority: "https://login.microsoftonline.com/a869bac5-300a-4536-ad8d-a30d554a593a/",
      redirectUri: "http://localhost:8090",
      cacheLocation: "sessionStorage",
      navigateToLoginRequestUrl: false,
      consentScopes: ["profile", "email"],
      protectedResourceMap: [["http://localhost:8080", []]],
    })
  ],
  providers: [
    HttpService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true
    },
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
