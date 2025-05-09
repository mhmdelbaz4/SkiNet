import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/Product';
import { ProductsService } from '../../core/services/products.service';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FitersDialogComponent } from './fiters-dialog/fiters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ShopParams } from '../../shared/models/shopParams';
import { Pagination } from '../../shared/models/Pagination';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-shop',
  imports: [
    ProductItemComponent,
    MatIcon,
    MatButton,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger, 
    MatPaginator,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private dialogService = inject(MatDialog);
  productService = inject(ProductsService);
  products?: Pagination<Product>;
  shopParams: ShopParams = new ShopParams();
  pageSizeOptions=[5,10,15,20];

  sortOptions:any[] = [
    {name:"Alphapetical", value:"name"},
    {name:"Price: Low-High", value:"priceAsc"},
    {name:"Price: High-Low", value:"priceDesc"}
  ];

  ngOnInit(): void {
    this.productService.getBrands();
    this.productService.getTypes();
    this.getProductsData();  
  }

  getProductsData(){
    this.productService.getProducts(this.shopParams).subscribe({
      next: res => this.products = res,
      error: err => console.log(err)
    });
  }

  onSearchChange(){
    this.shopParams.pageIndex = 1;
    this.getProductsData();
  }

  openFiltersDialog(){
    const dialogRef = this.dialogService.open(FitersDialogComponent, {
      minWidth:'500px',
      data:{
        selectedTypes: this.shopParams.types,
        selectedBrands: this.shopParams.brands,
      }
    });

    dialogRef.afterClosed().subscribe({
      next: res =>{
        if(res){
          this.shopParams.brands = res.selectedBrands,
          this.shopParams.types = res.selectedTypes
        }
        this.applyFilters();
      }
    })
  }

  applyFilters(){
    this.productService.getProducts(this.shopParams).subscribe({
      next: res => this.products = res
    })
  }
  
  appySort(event:MatSelectionListChange){
    let selectedOption = event.options[0];
    if(selectedOption){
      this.shopParams.sort = selectedOption.value;
      console.log(selectedOption.value);
      this.getProductsData();
    }

  }

  applyPaging(event: PageEvent){
    this.shopParams.pageIndex = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProductsData();
  }
}
