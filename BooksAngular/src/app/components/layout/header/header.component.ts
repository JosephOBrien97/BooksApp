import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ThemeToggleComponent } from '../../shared/theme-toggle/theme-toggle.component'; 

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  imports: [ThemeToggleComponent, CommonModule, RouterModule],
  styleUrls: ['../layout.component.scss']
})
export class HeaderComponent {
  constructor(public authService: AuthService, private router: Router) { }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
