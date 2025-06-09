import { Component } from '@angular/core';
import { PathFinder } from './path-finder/path-finder';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PathFinder],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected title = 'ShortestPath.Frontend';
}