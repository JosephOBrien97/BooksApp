import { Component, OnInit } from '@angular/core';
import { QuoteService } from '../../../services/quote.service';
import { CommonModule } from '@angular/common';
import { Quote } from '../../../models/quote.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-quote-list',
  standalone: true,
  templateUrl: './quote-list.component.html',
  imports: [CommonModule, RouterModule],
})
export class QuoteListComponent implements OnInit {
  quotes: Quote[] = [];

  constructor(
    private quoteService: QuoteService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.quoteService.getQuotes().subscribe(data => this.quotes = data);
  }
}
