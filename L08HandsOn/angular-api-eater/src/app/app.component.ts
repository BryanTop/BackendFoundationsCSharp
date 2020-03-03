import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  fruits;

  getApi() {
    const url = 'http://localhost:5001/api/values';
    fetch(url)
        .then(resp => resp.json())
        .then(resp => (this.fruits = resp));
  }

  

}
