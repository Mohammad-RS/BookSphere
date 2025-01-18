import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user-service';
import { UserProfileModel } from '../../models/user-profile-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  imports: [CommonModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css',
})
export class UserProfileComponent implements OnInit {
  constructor(private userService: UserService) {}
  userProfile: UserProfileModel | null = null;

  ngOnInit(): void {
    this.Profile();
  }

  Profile() {
    this.userService.getProfile().subscribe((response) => {
      if (response.success) {
        this.userProfile = {
          username: response.data.username,
          email: response.data.email,
          fullname: response.data.fullname,
          avatar: response.data.avatar,
          dateCreated: new Date(response.data.dateCreated),
          isVerified: response.data.isVerified,
          isActive: response.data.isActive,
        };
      } else {
        alert(response.errorMessage);
      }
    });
  }
}
