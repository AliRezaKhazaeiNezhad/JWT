import { AppComponent } from './app/app.component';
// main.ts
import { importProvidersFrom } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(),
  ]
})
.catch(err => console.error(err));