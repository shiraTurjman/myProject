import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaqeNotFoundComponent } from './paqe-not-found.component';

describe('PaqeNotFoundComponent', () => {
  let component: PaqeNotFoundComponent;
  let fixture: ComponentFixture<PaqeNotFoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaqeNotFoundComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaqeNotFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
