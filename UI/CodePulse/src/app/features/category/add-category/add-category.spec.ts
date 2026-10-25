import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';

import { AddCategory } from './add-category';

describe('AddCategory', () => {
  let component: AddCategory;
  let fixture: ComponentFixture<AddCategory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddCategory],
      providers: [provideHttpClient(), provideRouter([])],
    }).compileComponents();

    fixture = TestBed.createComponent(AddCategory);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('shows the required error for a touched empty name', () => {
    component.nameFormControl.markAsTouched();
    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('This field is required.');
  });

  it('shows the maxlength error when the name exceeds 50 characters', () => {
    component.nameFormControl.setValue('a'.repeat(51));
    component.nameFormControl.markAsTouched();
    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('Category name can only be 50 characters long.');
  });

  it('marks fields touched instead of submitting an invalid form', () => {
    component.onSubmit();

    expect(component.nameFormControl.touched).toBeTrue();
    expect(component.urlHandleFormControl.touched).toBeTrue();
  });
});
