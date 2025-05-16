import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../models/book.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:49272/api/books';

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl, this.getHttpOptions());
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/${id}`, this.getHttpOptions());
  }

  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book, this.getHttpOptions());
  }

  updateBook(book: Book): Observable<any> {
    return this.http.put(`${this.apiUrl}/${book.id}`, book, this.getHttpOptions());
  }

  deleteBook(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, this.getHttpOptions());
  }

  private getHttpOptions() {
    const token = this.authService.getToken();
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };
  }
}
