import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserProfileVisitComponent } from './user-profile-visit.component';

describe('UserProfileVisitComponent', () => {
  let component: UserProfileVisitComponent;
  let fixture: ComponentFixture<UserProfileVisitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserProfileVisitComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserProfileVisitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
