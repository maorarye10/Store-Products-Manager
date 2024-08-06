import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterSortFormComponent } from './filter-sort-form.component';

describe('FilterSortFormComponent', () => {
  let component: FilterSortFormComponent;
  let fixture: ComponentFixture<FilterSortFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FilterSortFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FilterSortFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
