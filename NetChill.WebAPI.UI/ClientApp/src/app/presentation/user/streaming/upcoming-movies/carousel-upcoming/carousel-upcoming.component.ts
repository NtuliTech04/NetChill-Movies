import { DatePipe } from '@angular/common';
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
  selector: 'app-carousel-upcoming',
  templateUrl: './carousel-upcoming.component.html',
  styleUrls: ['./carousel-upcoming.component.css']
})
export class CarouselUpcomingComponent implements OnInit {

  upcomingInfoData: MovieBaseInfo[] =  new Array();
  upcomingMediaData: MovieClip[] =  new Array();
  // upcomingMoviesFull: any[] = [];

  
  constructor (
    private streamingService: MovieStreamingService,
    private datePipe: DatePipe,
    private router: Router
   ) {}

  ngOnInit(): void {
    this.upcomingInfoes();
    this.upcomingFiles();
  }



  //Get and populate upcoming movies base info
  upcomingInfoes() {
    this.streamingService.getUpcomingInfoList()
    .subscribe({
      next:(data) => {
        this.upcomingInfoData = data['data'] as MovieBaseInfo[];
        // this.matchMergeMovies();
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate upcoming movies info data', ex);
      }
    });
  }  

  //Get and populate upcoming movies files
  upcomingFiles() {
    this.streamingService.getUpcomingMediaList()
    .subscribe({
      next:(data) => {
        this.upcomingMediaData = data['data'] as MovieClip[];
        // this.matchMergeMovies();
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate upcoming movies files data', ex);
      }    
    });
  }


    //=> User Interactions
    
    clickHandler(id: Guid): void {
      this.router.navigateByUrl('/SelectedUpcoming/'+id)
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

    
    //Creates a group of arrays containing data for matching movies.
    // matchMergeMovies() {
    //   let fullMovieData: any[] = new Array();
    //   for (let i = 0; i < this.upcomingInfoData.length; i++) {
    //     for (let m = 0; m < this.upcomingMediaData.length; m++) {
    //       if(this.upcomingInfoData[i].movieId == this.upcomingMediaData[m].movieRef){
    //         fullMovieData.push(this.upcomingInfoData[i]);
    //         fullMovieData.push(this.upcomingMediaData[m]);
    //         this.upcomingMoviesFull.push(fullMovieData); 
    //         fullMovieData = [];
    //         break;
    //       }
    //     }    
    //   }   
    // }

    //Creating poster image url path
    public posterUrl = (serverPath: string) => {
      return `${environment.baseUrl}/${serverPath}`;
    }

    //Extract the month from date
    selectMonth(date: Date): string {
      return this.datePipe.transform(date, 'MMMM') || '';
    }
}
