import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { MovieStreamingService } from 'src/app/shared/services/streaming/movie-streaming.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-carousel-featured',
  templateUrl: './carousel-featured.component.html',
  styleUrls: ['./carousel-featured.component.css']
})
export class CarouselFeaturedComponent implements OnInit{

  featuredInfoData: MovieBaseInfo[] = new Array();
  featuredMediaData: MovieClip[] = new Array();

  movieDurations: { movieId: Guid, duration: string }[] = [];

  constructor (
    private streamingService: MovieStreamingService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.featuredInfoes();
    this.featuredFiles();
  }


  //Get and populate featured movies base info
  featuredInfoes() {
    this.streamingService.getFeaturedInfoList()
    .subscribe({
      next:(data) => {
        this.featuredInfoData = data['data'] as MovieBaseInfo[];
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate featured movies info data', ex);
      }
    });
  }

  //Get and populate featured movies files
  featuredFiles() {
    this.streamingService.getFeaturedMediaList()
    .subscribe({
      next:(data) => {
        this.featuredMediaData = data['data'] as MovieClip[];
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate featured movies files data', ex);
      },
      complete: () => {
        this.getMovieDurations();
      }
    });
  }




  //=> User Interactions
  clickHandler(id: Guid): void {
    this.router.navigateByUrl('/SelectedMovie/'+id)
    .then(() => {
      this.router.navigate([this.router.url]);
    });
  }


  //=> Configurations

  //Carousel Settings
  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: true,
    autoplay: true,
    autoplaySpeed: 1000,
    autoplayTimeout: 3000,
    autoplayHoverPause:true,
    touchDrag: true,
    pullDrag: false,
    dots: false,
    navSpeed: 600,
    navText: ['&#8249', '&#8250;'],
    responsive: {
      0: { items: 1 },
      400: { items: 2 },
      760: { items: 3 },
      1000: { items: 6 }
    },
    nav: false
  }
  //Carousel Settings



  //=> Helper Functions

  //Creating file url path
  public fileUrl = (serverPath: string) => {
    return `${environment.baseUrl}/${serverPath}`;
  }

  //Gets movie duration from an array using its Id
  public getDuration = (id: Guid) => {
    return this.movieDurations.find(x=>x.movieId == id)?.duration;
  }

  //Creates an array of movie durations with an identifier 
  getMovieDurations() {
    this.featuredMediaData.forEach((movieData) => {
      const movie = document.createElement('video');
      movie.src = this.fileUrl(movieData.videoClipPath);
        //Add 'loadedmetadata' event listner
        movie.addEventListener('loadedmetadata', () => {
        //Get movie duration & proccess it
        let floatMinutes = movie.duration / 60;
        let tempArray = floatMinutes.toString().split('.');
        let seconds = (parseInt(tempArray[1].slice(0,2)) * 0.6).toFixed(0);

        let duration = `${tempArray[0] + "m " +seconds+ "s"}`;
        tempArray.splice(0, 2);

        //Add duration to array
        this.movieDurations.push({ movieId: movieData.movieRef, duration });
      });
    });
  }
  

}
