import { Component, inject } from '@angular/core';
import { ProductsService } from '../../../core/services/products.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-fiters-dialog',
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton,
    FormsModule
  ],
  templateUrl: './fiters-dialog.component.html',
  styleUrl: './fiters-dialog.component.scss'
})
export class FitersDialogComponent {
  productService = inject(ProductsService);
  private dialogRef = inject(MatDialogRef<FitersDialogComponent>);
  date = inject(MAT_DIALOG_DATA);

  selectedBrands:string[]=this.date.selectedBrands;
  selectedTypes:string[]=this.date.selectedTypes;

  applyFilters(){
    this.dialogRef.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes
    });
  }


}
