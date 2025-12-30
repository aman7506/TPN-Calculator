import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AppInitService {
  constructor(private http: HttpClient) {}

  init(): Promise<void> {
    return new Promise((resolve, reject) => {
      console.log('AppInitService: Initializing application...');
      
      // Simulate async initialization (e.g., fetching config)
      setTimeout(() => {
        console.log('AppInitService: Initialization complete.');
        resolve();
      }, 1000);
    });
  }
}
