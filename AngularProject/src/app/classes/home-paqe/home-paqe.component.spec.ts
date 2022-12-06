import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePaqeComponent } from './home-paqe.component';

describe('HomePaqeComponent', () => {
  let component: HomePaqeComponent;
  let fixture: ComponentFixture<HomePaqeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomePaqeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomePaqeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
