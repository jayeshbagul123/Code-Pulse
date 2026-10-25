import { Component, effect, inject, input } from '@angular/core';
import { CategoryService } from '../services/category-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-category.html',
  styleUrl: './edit-category.css',
})
export class EditCategory {
  constructor() {
   effect(() => {
     if(this.categoryService.updateCategoryStatus() === 'success'){
      this.categoryService.updateCategoryStatus.set('idle');  
      this.router.navigate(['/admin/categories']);
    }

    if(this.categoryService.updateCategoryStatus() === 'error'){
      console.error('something went wrong while updating the category!');
    }
   })
  };

  id = input<string>();
  private categoryService = inject(CategoryService);
  private router = inject(Router);
  categoryResourceRef = this.categoryService.getCategoryById(this.id);
  categoryReponse = this.categoryResourceRef.value;

    editCategoryFormGroup = new FormGroup({
    name: new FormControl<string>('', { nonNullable:true,
       validators: [Validators.required, Validators.maxLength(50)]}),
    urlHandle: new FormControl<string>('', { nonNullable:true, 
      validators: [Validators.required, Validators.maxLength(50)]}),
  });

  // this is a getter method that returns the form control for the 'name' field in the editCategoryFormGroup.
  //  It allows easy access to the form control for validation and value retrieval.
  get nameFormControl(){
    return this.editCategoryFormGroup.controls.name;
  }
  get urlHandleFormControl(){
    return this.editCategoryFormGroup.controls.urlHandle;
  }

  effectRef = effect(() => {
    this.editCategoryFormGroup.controls.name.patchValue(this.categoryReponse()?.name ?? '');
    this.editCategoryFormGroup.controls.urlHandle.patchValue(this.categoryReponse()?.urlHandle ?? '');
  })

  onSubmit(){
    const id = this.id();
    if(!id || !this.editCategoryFormGroup.valid){
      return;
    }
    const fromRawValue = this.editCategoryFormGroup.getRawValue();
    const updateCategoryRequestDto = {
      name: fromRawValue.name,
      UrlHandle: fromRawValue.urlHandle   
    }
    this.categoryService.updateCategory(id, updateCategoryRequestDto);
  }
}
