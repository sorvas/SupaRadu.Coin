import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RaduImageComponent } from './radu-image.component';

describe('RaduImageComponent', () => {
  let component: RaduImageComponent;
  let fixture: ComponentFixture<RaduImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RaduImageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RaduImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
