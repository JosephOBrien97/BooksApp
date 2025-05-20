import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { BookDeleteComponent } from './components/books/book-delete/book-delete.component';
import { BookFormComponent } from './components/books/book-form/book-form.component';
import { BookListComponent } from './components/books/book-list/book-list.component';
import { QuoteListComponent } from './components/quotes/quote-list/quote-list.component';
import { QuoteFormComponent } from './components/quotes/quote-form/quote-form.component';
import { QuoteDeleteComponent } from './components/quotes/quote-delete.component.ts/quote-delete.component';


export const appRoutes: Routes = [
  { path: '', redirectTo: '/books', pathMatch: 'full' },
  { path: 'books', component: BookListComponent, canActivate: [AuthGuard] },
  { path: 'books/new', component: BookFormComponent, canActivate: [AuthGuard] },
  { path: 'books/:id', component: BookDeleteComponent, canActivate: [AuthGuard] },
  { path: 'books/:id/edit', component: BookFormComponent, canActivate: [AuthGuard] },
  { path: 'quotes', component: QuoteListComponent, canActivate: [AuthGuard] },
  { path: 'quotes/new', component: QuoteFormComponent, canActivate: [AuthGuard] },
  { path: 'quotes/:id', component: QuoteDeleteComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '/books' }
];
