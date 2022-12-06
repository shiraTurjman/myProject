import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagementStudentListComponent } from './management-student-list.component';

describe('ManagementStudentListComponent', () => {
  let component: ManagementStudentListComponent;
  let fixture: ComponentFixture<ManagementStudentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManagementStudentListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagementStudentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
