import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';

import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { MovieStreamingService } from 'src/app/shared/services/streaming/movie-streaming.service';
import { environment } from 'src/environments/environment.development';


@Component({
  selector: 'app-carousel-latest',
  templateUrl: './carousel-latest.component.html',
  styleUrls: ['./carousel-latest.component.css']
})
export class CarouselLatestComponent implements OnInit {

  latestInfoData: MovieBaseInfo[] =  [];
  latestMediaData: MovieClip[] =  [];

  movieDurations: { movieId: Guid, duration: string }[] = [];



  constructor (
    private streamingService: MovieStreamingService,
    private router: Router
  ) {

  }

  ngOnInit() {
    this.latestInfoes();
    this.latestFiles();
  }


  //Get and populate latest movies base info
  latestInfoes() {
    this.streamingService.getLatestInfoList()
    .subscribe({
      next:(data) => {
        this.latestInfoData = data['data'] as MovieBaseInfo[];
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate latest movies info data', ex);
      }
    });
  }

  //Get and populate latest movies files
  latestFiles() {
    this.streamingService.getLatestMediaList()
    .subscribe({
      next:(data) => {
        this.latestMediaData = data['data'] as MovieClip[];
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate latest movies files data', ex);
      },    
      complete: () => {
        this.getMovieDurations();
      }    
    });
  }


  //User Interactions

  clickHandler(id: Guid): void {
    this.router.navigateByUrl('/SelectedMovie/'+id)
    .then(() => {
      this.router.navigate([this.router.url]);
    });
  }




  //Helper Functions

  //Gets movie duration from an array using its Id
  public getDuration = (id: Guid) => {
    return this.movieDurations.find(x=>x.movieId == id)?.duration;
  }

  //Creates an array of movie durations with an identifier 
  getMovieDurations() {
    this.latestMediaData.forEach((movieData) => {
      const movie = document.createElement('video');
      movie.src = this.fileUrl(movieData.videoClipPath);
       //Add 'loadedmetadata' event listner
       movie.addEventListener('loadedmetadata', () => {
        //Get movie duration
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

  //Creating media files url path
  public fileUrl = (serverPath: string) => {
    return `${environment.baseUrl}/${serverPath}`;
  }

  //Creating poster image url path
  public posterUrl = (id: Guid) => {
    let serverPath = this.latestMediaData.find(x => x.movieRef === id)?.moviePosterPath;
    return `${environment.baseUrl}/${serverPath}`;
  }
}
