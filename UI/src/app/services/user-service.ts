import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessResult } from '../models/business-result-model';
import { UserRegisterModel } from '../models/user-register-model';
import { UserLoginModel } from '../models/user-login-model';
import { UserProfileModel } from '../models/user-profile-model';
import { _env } from '../env/env';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) {}

  postRegister(request: UserRegisterModel) {
    let url = `${_env.baseUrl}/user/register`;
    return this.http.post<BusinessResult<Number>>(url, request);
  }

  postLogin(request: UserLoginModel) {
    let url = `${_env.baseUrl}/user/login`;
    return this.http.post<BusinessResult<string>>(url, request);
  }

  getProfile() {
    let url = `${_env.baseUrl}/user/profile`;
    return this.http.get<BusinessResult<UserProfileModel>>(url);
  }
}
