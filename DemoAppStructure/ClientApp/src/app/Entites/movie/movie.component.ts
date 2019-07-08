import { Component, OnInit } from '@angular/core';
import { MovieService } from 'src/app/Services/movie.service';
import { Movie } from 'src/app/Models/movie.model';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.sass']
})
export class MovieComponent implements OnInit {
  movies: Movie[];
  constructor(private movieService: MovieService) { }
  
  ngOnInit() {
    this.getMovies();
  }

  getMovies(){
    this.movieService.getMovies()
            .subscribe((movies) => {
                this.movies = movies;
            },
            (error) => {
              console.log(error.message);
            });
  }
}
