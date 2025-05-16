import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './components/books/book-list.component';
import { BookFormComponent } from './components/books/book-form.component';
import { BookDetailComponent } from './components/books/book-detail.component';
import { QuoteListComponent } from './components/quotes/quote-list.component';
import { LoginComponent } from './components/auth/login.component';
import { RegisterComponent } from './components/auth/register.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
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

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
