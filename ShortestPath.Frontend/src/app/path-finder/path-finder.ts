import { Component } from '@angular/core';
import { Data } from '../../path-finder/data';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-path-finder',
  imports: [CommonModule, FormsModule],
  templateUrl: './path-finder.html',
  styleUrl: './path-finder.css'
})
export class PathFinderComponent {
  actors: string[] = [];
  selectedActor: string = '';
  path: string[] = [];

  constructor(private data: Data) {
    this.actors = this.data.getActors();
  }

  findPath() {
    this.path = this.data.getPathToTomCruise(this.selectedActor);
  }
}