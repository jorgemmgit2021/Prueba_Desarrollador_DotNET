import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PacientesEditarComponent } from './pacientes-editar.component';

describe('PacientesEditarComponent', () => {
  let component: PacientesEditarComponent;
  let fixture: ComponentFixture<PacientesEditarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PacientesEditarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PacientesEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
