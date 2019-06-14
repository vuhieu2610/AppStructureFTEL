export class ReturnResult<T>{
  message: string;
  code: string;
  listItem: T[];
  item: T;
  totalCount: number;
  hasData: boolean;
  hasError: boolean;
}
