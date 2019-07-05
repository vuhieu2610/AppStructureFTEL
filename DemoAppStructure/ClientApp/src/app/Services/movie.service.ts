import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../Models/movie.model';

@Injectable({
  providedIn: 'root'
})
export class MovieService implements OnInit{
  private staticUrl = "https://localhost:44311/movie/GetMovieList";
  constructor(private _http: HttpClient) { }

  ngOnInit(): void {
    
  }

  getMovies(){
    return this._http.get<Movie[]>(this.staticUrl);
  }
}
