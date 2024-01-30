import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';
import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { MovieProduction } from 'src/app/core/models/movie/movie-production.model';
import { MovieStreamingService } from 'src/app/shared/services/streaming/movie-streaming.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-selected-movie',
  templateUrl: './selected-movie.component.html',
  styleUrls: ['./selected-movie.component.css']
})
export class SelectedMovieComponent {
  
  @ViewChild('poster', { static: true }) posterView: ElementRef<HTMLImageElement>;
  @ViewChild('clip', { static: true }) clipPlayer: ElementRef<HTMLVideoElement>;


  selectedMovieId: Guid;
  movieDuration: string;

  infoData: MovieBaseInfo = new MovieBaseInfo;
  productionData: MovieProduction = new MovieProduction;;
  mediaData: MovieClip = new MovieClip;


  private subscription: Subscription | undefined;

  constructor (
    private streamingService: MovieStreamingService,
    private activatedRoute: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router,
  ) {
    //Get Movie Id from Url
    this.activatedRoute.params.subscribe(params => {
      this.selectedMovieId = params['id'];
    }); 
  }

  ngOnInit(): void {
    this.getMovieInfoData(this.selectedMovieId);
    this.getMovieProductionData(this.selectedMovieId);
    this.getMovieClipData(this.selectedMovieId);
  }

  //#region Get and populate selected movie by id

  getMovieInfoData(id: Guid) {
    this.streamingService.readMovieInfo(id)
    .subscribe({
      next: (data) => {
        this.infoData = data['data'] as MovieBaseInfo;
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate selected movie info data', ex);
      }
    });
  }

  getMovieProductionData(id: Guid) {
    this.streamingService.readMovieProduction(id)
    .subscribe({
      next: (data) => {
        this.productionData = data['data'] as MovieProduction;
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate selected movie production data', ex);
      }
    });
  }

  getMovieClipData(id: Guid) {
    this.streamingService.readMovieFiles(id)
    .subscribe({
      next: (data) => {
        this.mediaData = data['data'] as MovieClip;
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate selected movie files data', ex);
      },
      complete: () => {
        this.renderPoster();
        this.renderVideo();
        this.getMovieDuration();
      }
    });
  }


  //#endregion


  //#region Helper Functions


  //Creates an array of movie durations with an identifier 
  getMovieDuration() {
      const movie = document.createElement('video');
      movie.src = this.fileUrl(this.mediaData.videoClipPath);
      //Add 'loadedmetadata' event listner
      movie.addEventListener('loadedmetadata', () => {
      //Get movie duration
      let floatMinutes = movie.duration / 60;
      let tempArray = floatMinutes.toString().split('.');
      let seconds = (parseInt(tempArray[1].slice(0,2)) * 0.6).toFixed(0);

      let duration = `${tempArray[0] + "m " +seconds+ "s"}`;
      tempArray.splice(0, 2);

      this.movieDuration = duration;
    });
  }


  //Generate file path url
  public fileUrl = (serverPath: string) => {
    return `${environment.baseUrl}/${serverPath}`;
  }

  //Processes and displays poster image
  renderPoster() {
    let posterImg = this.posterView.nativeElement;
    posterImg.src = this.fileUrl(this.mediaData.moviePosterPath);
  }

  //Processes and displays movie clip
  renderVideo() {
    let movieClip = this.clipPlayer.nativeElement;
    movieClip.src = this.fileUrl(this.mediaData.videoClipPath);
  }

  //Get DateToLongString from UTC date
  dateToLongString(date: Date): string {
    return this.datePipe.transform(date, 'dd MMMM YYYY') || '';
  }

  //#endregion


  //#region Dispose Functions
  ngAfterViewInit(): void {
    const currentUrl = this.router.url;
    this.subscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if(event.url != currentUrl && event.url != currentUrl+'#watch-movie'){
          this.infoData = null;
          this.productionData = null;
          this.mediaData = null;
          this.posterView.nativeElement.src = '';
          this.clipPlayer.nativeElement.src = '';
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  //#endregion
}
