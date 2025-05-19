import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  templateUrl: './footer.component.html',
  styleUrls: ['../layout.component.scss']
})

export class FooterComponent {
  currentYear = new Date().getFullYear();
}
