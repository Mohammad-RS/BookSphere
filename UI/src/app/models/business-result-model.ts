export interface BusinessResult<T> {
  success: Boolean;
  data: T;
  errorCode: number;
  errorMessage: string;
}
