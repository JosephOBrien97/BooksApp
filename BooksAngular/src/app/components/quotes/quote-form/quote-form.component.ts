import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { QuoteService } from 'src/app/services/quote.service';

@Component({
  selector: 'app-quote-form',
  templateUrl: './quote-form.component.html',
  styleUrls: ['./quote-form.component.scss']
})
export class QuoteFormComponent {
  quoteForm: FormGroup;

  constructor(private fb: FormBuilder, private quoteService: QuoteService) {
    this.quoteForm = this.fb.group({
      text: ['', [Validators.required, Validators.maxLength(250)]]
    });
  }

  onSubmit(): void {
    if (this.quoteService.getQuotes().length >= 5) {
      alert('Max 5 citat tillåtna.');
      return;
    }

    this.quoteService.addQuote(this.quoteForm.value.text);
    this.quoteForm.reset();
  }
}
