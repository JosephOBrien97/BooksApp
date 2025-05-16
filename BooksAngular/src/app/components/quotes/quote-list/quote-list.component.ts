import { Component } from '@angular/core';
import { QuoteService } from '../../../services/quote.service';

@Component({
  selector: 'app-quote-list',
  templateUrl: './quote-list.component.html',
  styleUrls: ['./quote-list.component.scss']
})
export class QuoteListComponent {
  constructor(public quoteService: QuoteService) { }

  removeQuote(index: number): void {
    this.quoteService.deleteQuote(index);
  }
}
