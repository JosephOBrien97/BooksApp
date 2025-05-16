import { Component, OnInit } from '@angular/core';
// Fix 1: Correct the import path for BookService
import { BookService } from '../../../services/book.service';
import { Book } from '../../../models/book.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss']
})
export class BookFormComponent implements OnInit {
  bookForm!: FormGroup;
  isEdit = false;
  bookId!: number;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.bookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      publishedDate: ['', Validators.required]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEdit = true;
      this.bookId = +id;
      // Fix 3: Explicitly type the 'book' parameter
      this.bookService.getBook(this.bookId).subscribe((book: Book) => {
        this.bookForm.patchValue(book);
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
