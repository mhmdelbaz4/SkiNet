import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../../shared/models/Product';
import { ProductsService } from '../../../core/services/products.service';
import { ActivatedRoute } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { CurrencyPipe } from '@angular/common';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-product-details',
  imports: [
   CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  productService = inject(ProductsService);
  activatedRoute = inject(ActivatedRoute);
  product?:Product;


  ngOnInit(): void {
    this.getproduct();
  }

  getproduct(){
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if(!id)return;

    this.productService.getProduct(id).subscribe({
      next: product => this.product = product,
      error: error => console.log(error)
    });

  }

}
