export interface AddCategoryRequest{
    name:string;
    UrlHandle:string;
}

export interface UpdateCategoryRequest{
    name:string;
    UrlHandle:string;
}

export interface Category{
    id:string;
    name:string;
    urlHandle:string;
}