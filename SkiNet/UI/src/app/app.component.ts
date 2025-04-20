import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Pagination } from './shared/models/Pagination';
import { Product } from './shared/models/Product';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private baseUrl: string = 'https://localhost:5001/api/';
  title:string = 'SkiNet';
  http: HttpClient = inject(HttpClient);
  products:Product[]=[];
  
  ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.baseUrl + 'products').subscribe({
      next: (response) => this.products=response.data,
      error: error => console.log(error),
      complete: ()=> console.log('completed!')
    });
  }
  data:string='';
}
