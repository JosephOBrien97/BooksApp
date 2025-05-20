import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from '../../../models/book.model';
import { BookService } from '../../../services/book.service';

@Component({
  selector: 'app-book-form',
  standalone: true,
  templateUrl: './book-form.component.html',
  imports: [ReactiveFormsModule, CommonModule]
})
export class BookFormComponent implements OnInit {
  bookForm!: FormGroup;
  bookId!: number;
  isEdit = false;
  

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  private formatDate(date: string | Date): string {
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      publicationDate: [this.formatDate(new Date()), Validators.required]
    });

    
    if (id) {
      this.isEdit = true;
      this.bookId = +id;
      this.bookService.getBook(this.bookId).subscribe((book: Book) => {
        this.bookForm.patchValue({
          title: book.title,
          author: book.author,
          publicationDate: this.formatDate(book.publicationDate)
        });
      });
    }
  }

  onSubmit(): void {
    const book: Book = this.bookForm.value;
    if (this.isEdit) {
      book.id = this.bookId;
      this.bookService.updateBook(book).subscribe(() => this.router.navigate(['/books']));
    } else {
      this.bookService.addBook(book).subscribe(() => this.router.navigate(['/books']));
    }
  }
}
