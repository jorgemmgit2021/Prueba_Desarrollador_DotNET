import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctoresEditarComponent } from './doctores-editar.component';

describe('DoctoresEditarComponent', () => {
  let component: DoctoresEditarComponent;
  let fixture: ComponentFixture<DoctoresEditarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctoresEditarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctoresEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
