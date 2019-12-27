import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NumbersGameComponent } from './numbers-game.component';

describe('NumbersGameComponent', () => {
  let component: NumbersGameComponent;
  let fixture: ComponentFixture<NumbersGameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NumbersGameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NumbersGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
