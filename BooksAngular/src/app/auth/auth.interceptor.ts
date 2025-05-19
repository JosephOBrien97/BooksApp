import { inject } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';


export const authInterceptorFn: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.getToken();

  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(authReq).pipe(
    catchError((error) => {
      if (error.status === 401) {
        authService.logout();
        if (router.url !== '/login') {
          router.navigate(['/login'], { queryParams: { returnUrl: router.url } });
        }
      } else if (error.status === 403) {
        router.navigate(['/forbidden']);
      }
      return throwError(() => error);
    })
  );
};

//@Injectable()
//export class AuthInterceptor implements HttpInterceptor {

//  constructor(private authService: AuthService, private router: Router) { }

//  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
//    const token = this.authService.getToken();

//    if (token) {
//      request = request.clone({
//        setHeaders: {
//          Authorization: `Bearer ${token}`
//        }
//      });
//    }

//    return next.handle(request).pipe(
//      catchError((error: HttpErrorResponse) => {
//        if (error.status === 401) {
//          this.authService.logout();

//          if (this.router.url !== '/login') {
//            this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.url } });
//          }
//        } else if (error.status === 403) {
//            this.router.navigate(['/forbidden']);
//        }
//        return throwError(() => error);
//      })
//    );
//  }
//}
