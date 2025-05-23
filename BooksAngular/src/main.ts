import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/app.routes';
import { withInterceptors, provideHttpClient } from '@angular/common/http';
import { authInterceptorFn } from './app/auth/auth.interceptor';
import { importProvidersFrom } from '@angular/core';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptors([authInterceptorFn])),
    importProvidersFrom(NgbCollapseModule)
  ]
}).catch(err => console.error(err));
