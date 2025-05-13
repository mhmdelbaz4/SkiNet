import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/Pagination';
import { Product } from '../../shared/models/Product';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/';
  brands:string[]=[];
  types:string[]=[];
  
  getProducts(shopParams: ShopParams){
    let params: HttpParams = new HttpParams();
    if(shopParams.brands.length > 0)
      params = params.append('brands',shopParams.brands.join(','));
 
    if(shopParams.types.length > 0)
      params = params.append('types', shopParams.types.join(','));
    
    if(shopParams.sort)
      params = params.append("sortBy", shopParams.sort);

    if(shopParams.search)
      params = params.append("Search", shopParams.search);

    params = params.append('pageSize',shopParams.pageSize);
    params = params.append('pageIndex',shopParams.pageIndex);

    return this.http.get<Pagination<Product>>(this.baseUrl + "products",{params});
  }

  getProduct(id:string){
    return this.http.get<Product>(this.baseUrl + `products/${id}`);
  }

  getBrands(){
    if(this.brands.length>0)return;

    this.http.get<string[]>(this.baseUrl+'products/brands').subscribe({
      next: res => this.brands =res,
      error: err => console.log(err) 
    });
  }

  getTypes(){
    if(this.types.length > 0)return;
    
    this.http.get<string[]>(this.baseUrl+'products/types').subscribe({
      next: res => this.types =res,
      error: err => console.log(err) 
    });
  }

}
