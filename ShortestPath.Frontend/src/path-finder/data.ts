import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Data {
  getActors(): string[] {
    return [
      'Steve Guttenberg', 'Daniel Stern', 'Julia Roberts', 'Tom Cruise'
      // Add all actors you want here
    ];
  }

  getPathToTomCruise(selectedActor: string): string[] {
    // Simulate a path: [actor1, film1, actor2, film2, Tom Cruise]
    // Replace with your real logic / backend call
    if (selectedActor === 'Julia Roberts') {
      return ['Julia Roberts', 'Flatliners', 'Tom Cruise'];
    } else if (selectedActor === 'Steve Guttenberg') {
      return ['Steve Guttenberg', 'Diner', 'Tom Cruise'];
    } else {
      return [selectedActor, 'Unknown', 'Tom Cruise'];
    }
  }
}
