export class ShopParams{
    brands: string[]=[];
    types: string[]=[];
    sort:string="name";
    search?:string;
    pageSize = 10;
    pageIndex = 1;
}