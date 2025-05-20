import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Book } from '../../../models/book.model';
import { BookService } from '../../../services/book.service';

@Component({
  selector: 'app-book-delete',
  standalone: true,
  templateUrl: './book-delete.component.html',
  imports: [CommonModule]
})
export class BookDeleteComponent implements OnInit {
  book?: Book;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookService: BookService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.bookService.deleteBook(+id).subscribe({
        next: (deletedBook) => {
          this.book = deletedBook;

          setTimeout(() => {
            this.router.navigate(['/books']);
          });
        }
      });
    }
  }
}
