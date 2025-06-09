import { Component } from '@angular/core';
import { PathFinderComponent } from './path-finder/path-finder';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PathFinderComponent, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected title = 'ShortestPath.Frontend';
}