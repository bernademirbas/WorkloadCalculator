import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkloadSummaryComponent } from './workload-summary.component';

describe('WorkloadSummaryComponent', () => {
  let component: WorkloadSummaryComponent;
  let fixture: ComponentFixture<WorkloadSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkloadSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkloadSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
