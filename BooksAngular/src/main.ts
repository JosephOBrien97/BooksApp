import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/app.routes';
import { withInterceptors, provideHttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { authInterceptorFn } from './app/auth/auth.interceptor';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptors([authInterceptorFn]))
  ]
}).catch(err => console.error(err));
