import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Quote } from '../../../models/quote.model';
import { QuoteService } from '../../../services/quote.service';

@Component({
  selector: 'app-quote-delete',
  standalone: true,
  templateUrl: './quote-delete.component.html',
  imports: [CommonModule]
})
export class QuoteDeleteComponent implements OnInit {
  quote?: Quote;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private quoteService: QuoteService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.quoteService.deleteQuote(+id).subscribe({
        next: (deletedQuote) => {
          this.quote = deletedQuote;

          setTimeout(() => {
            this.router.navigate(['/quotes']);
          });
        }
      });
    }
  }
}
