import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Book } from '../../models/book.model';

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
      this.bookService.getBookById(this.bookId).subscribe(book => {
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
      this.bookService.createBook(book).subscribe(() => this.router.navigate(['/books']));
    }
  }
}
