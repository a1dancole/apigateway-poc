import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { MsalModule } from '@azure/msal-angular';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HttpService } from './shared/http/http.service';
import { RouterModule } from '@angular/router';
import { AuthenticationInterceptor } from './shared/http/authentication.interceptor';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    MsalModule.forRoot({
      clientID: '',
      authority: "",
      redirectUri: "http://localhost:4200/",
      cacheLocation : "sessionStorage",
      navigateToLoginRequestUrl : false,
      consentScopes: ["profile", "email"],
      protectedResourceMap: [["http://localhost:8080",[]]],
    }),
    HttpClientModule
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
