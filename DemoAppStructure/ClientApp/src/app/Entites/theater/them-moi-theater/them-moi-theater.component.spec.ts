import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ThemMoiTheaterComponent } from './them-moi-theater.component';

describe('ThemMoiTheaterComponent', () => {
  let component: ThemMoiTheaterComponent;
  let fixture: ComponentFixture<ThemMoiTheaterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ThemMoiTheaterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ThemMoiTheaterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
