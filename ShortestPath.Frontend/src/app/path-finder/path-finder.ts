import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-path-finder',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './path-finder.html',
  styleUrl: './path-finder.css'
})
export class PathFinderComponent {
  actors: string[] = [];
  selectedActor: string = '';
  path: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // Get the list of actors from your API
    this.http.get<string[]>('http://localhost:5255/api/path/actors')
      .subscribe(data => this.actors = data);
  }

  findPath() {
    if (this.selectedActor) {
      this.http.get<string[]>(`http://localhost:5255/api/path/path?fromActor=${encodeURIComponent(this.selectedActor)}`)
        .subscribe(data => this.path = data);
    }
  }
}