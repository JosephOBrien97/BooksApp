import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { QuoteService } from '../../../services/quote.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quote-form',
  standalone: true,
  templateUrl: './quote-form.component.html',
  imports: [ReactiveFormsModule, CommonModule]
})
export class QuoteFormComponent {
  quoteForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private quoteService: QuoteService,
    private router: Router
  ) {
    this.quoteForm = this.fb.group({
      text: ['', [Validators.required, Validators.maxLength(250)]]
    });
  }

  onSubmit(): void {
    this.quoteService.getQuotes().subscribe(quotes => {
      if (quotes.length >= 5) {
        alert('Max 5 citat tillåtna.');
        return;
      }
      this.quoteService.addQuote(this.quoteForm.value).subscribe(() => this.router.navigate(['/quotes']));
      this.quoteForm.reset();
    });
  }
}
