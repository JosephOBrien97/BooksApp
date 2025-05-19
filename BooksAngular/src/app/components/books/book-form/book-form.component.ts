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
  imports: [ReactiveFormsModule, CommonModule],
  styleUrls: ['../book.component.scss']
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
      publicationDate: ['', Validators.required]
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
