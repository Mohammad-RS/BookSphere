export interface BookModel {
  id: number;
  author: string;
  category: string;
  title: string;
  summary: string;
  isbn: string;
  availableCopies: number;
  coverImage: string;
  dateCreated: Date;
  dateModified: Date;
}
