import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private darkMode = new BehaviorSubject<boolean>(this.getStoredTheme());
  darkMode$ = this.darkMode.asObservable();

  constructor() {
    this.applyTheme();
  }

  toggleTheme(): void {
    this.darkMode.next(!this.darkMode.value);
    this.storeTheme();
    this.applyTheme();
  }

  private getStoredTheme(): boolean {
    const storedTheme = localStorage.getItem('darkMode');
    return storedTheme ? JSON.parse(storedTheme) : false;
  }

  private storeTheme(): void {
    localStorage.setItem('darkMode', JSON.stringify(this.darkMode.value));
  }

  private applyTheme(): void {
    if (this.darkMode.value) {
      document.body.classList.add('dark-theme');
    } else {
      document.body.classList.remove('dark-theme');
    }
  }
}
