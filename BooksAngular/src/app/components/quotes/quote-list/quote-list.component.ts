import { Component } from '@angular/core';
import { QuoteService } from '../../../services/quote.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-quote-list',
  standalone: true,
  templateUrl: './quote-list.component.html',
  imports : [CommonModule],
  styleUrls: ['../quote.component.scss']
})
export class QuoteListComponent {
  constructor(public quoteService: QuoteService) { }

  removeQuote(index: number): void {
    this.quoteService.deleteQuote(index);
  }
}
