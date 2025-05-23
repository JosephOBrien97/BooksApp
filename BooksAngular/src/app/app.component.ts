import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FooterComponent } from './components/layout/footer/footer.component';
import { HeaderComponent } from './components/layout/header/header.component'; 
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  imports: [FooterComponent, HeaderComponent, RouterOutlet, HttpClientModule]
})
export class AppComponent {

}
