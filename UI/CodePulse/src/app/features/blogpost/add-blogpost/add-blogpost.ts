import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-blogpost',
  imports: [ReactiveFormsModule],
  templateUrl: './add-blogpost.html',
  styleUrl: './add-blogpost.css',
})
export class AddBlogpost {
  addBlogpostForm = new FormGroup({
    title: new FormControl<string>('',{
      nonNullable:true,
       validators: [ Validators.required, Validators.minLength(10), Validators.maxLength(100)]
      }),
    shortDescription: new FormControl<string>('',{
      nonNullable:true, 
      validators: [ Validators.required, Validators.minLength(50), Validators.maxLength(500)]
    }),  
    content: new FormControl<string>('',{
      nonNullable:true, 
      validators: [ Validators.required, Validators.minLength(100), Validators.maxLength(1000)]
    }),  
    featuredImageUrl: new FormControl<string>('',{
      nonNullable:true, 
      validators: [ Validators.required,Validators.minLength(1), Validators.maxLength(200)]
    }),  
    urlHandle: new FormControl<string>('',{
      nonNullable:true,
       validators: [ Validators.required, Validators.minLength(3), Validators.maxLength(100)]
      }),  
    publishedDate: new FormControl<string>(new Date().toISOString().split('T')[0],{
      nonNullable:true, 
      validators: [ Validators.required ]
    }),  
    author: new FormControl<string>('',{
      nonNullable:true, 
      validators: [ Validators.required, Validators.minLength(2), Validators.maxLength(100)]
    }),  
    isVisible: new FormControl<boolean>(false,{
      nonNullable:true, validators: [ Validators.required ]
    })  
  });

  onSubmit(){
    const fromRawValue = this.addBlogpostForm.getRawValue();
    console.log(fromRawValue);
  }
}
