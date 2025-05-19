import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { BookDetailComponent } from './components/books/book-delete/book-delete.component';
import { BookFormComponent } from './components/books/book-form/book-form.component';
import { BookListComponent } from './components/books/book-list/book-list.component';
import { QuoteListComponent } from './components/quotes/quote-list/quote-list.component';

export const appRoutes: Routes = [
  { path: '', redirectTo: '/books', pathMatch: 'full' },
  { path: 'books', component: BookListComponent, canActivate: [AuthGuard] },
  { path: 'books/new', component: BookFormComponent, canActivate: [AuthGuard] },
  { path: 'books/:id', component: BookDetailComponent, canActivate: [AuthGuard] },
  { path: 'books/:id/edit', component: BookFormComponent, canActivate: [AuthGuard] },
  { path: 'quotes', component: QuoteListComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '/books' }
];
