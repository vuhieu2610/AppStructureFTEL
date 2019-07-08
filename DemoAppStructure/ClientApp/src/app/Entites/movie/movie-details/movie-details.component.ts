import { Component, OnInit } from '@angular/core';
import { MovieService } from 'src/app/Services/movie.service';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/Models/movie.model';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.sass']
})
export class MovieDetailsComponent implements OnInit {
  private idMovie: number;
  private urlMovie: string[];
  movie: Movie;

  constructor(private _movieService: MovieService,
    private _route: ActivatedRoute) { }

  ngOnInit() {
    this.getSingleMovie();
  }

  getSingleMovie() {
    this.urlMovie = window.location.href.split('/');
    this.idMovie = parseInt(this.urlMovie[this.urlMovie.length - 1]);
    return this._movieService.getSingleMovies(this.idMovie)
      .subscribe((movie) => {
        this.movie = movie;
        console.log(this.movie);
      }, (error) => {
        console.log(error.message);
      }, () => console.log('Lấy dữ liệu thành công'));
  }
}
