import { Component, effect, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddCategoryRequest } from '../models/category.model';
import { CategoryService } from '../services/category-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  imports: [ReactiveFormsModule],
  templateUrl: './add-category.html',
  styleUrl: './add-category.css',
})
export class AddCategory {

  private router = inject(Router);

  constructor(){
     effect(() => {
      if(this.CategoryService.addCategoryStatus() === 'success'){
          this.CategoryService.addCategoryStatus.set('idle');
          this.router.navigate(['/admin/categories'])
      }
      if(this.CategoryService.addCategoryStatus() === 'error'){
          console.log('adding category request failed');
      }
    });
  }
  // Injecting the CategoryService using Angular's dependency injection system.
  private CategoryService = inject(CategoryService);
  addCategoryFormGroup = new FormGroup({
    name: new FormControl<string>('', { nonNullable:true,
       validators: [Validators.required, Validators.maxLength(50)]}),
    urlHandle: new FormControl<string>('', { nonNullable:true, 
      validators: [Validators.required, Validators.maxLength(50)]}),
  });

  // this is a getter method that returns the form control for the 'name' field in the addCategoryFormGroup.
  //  It allows easy access to the form control for validation and value retrieval.
  get nameFormControl(){
    return this.addCategoryFormGroup.controls.name;
  }
  get urlHandleFormControl(){
    return this.addCategoryFormGroup.controls.urlHandle;
  }

  onSubmit() {
    const addCategoryFormValue = this.addCategoryFormGroup.getRawValue();

    const addCategoryRequestDto: AddCategoryRequest = {
      name: addCategoryFormValue.name,
      UrlHandle: addCategoryFormValue.urlHandle
    };

    this.CategoryService.addCategory(addCategoryRequestDto);

   
  }
}
