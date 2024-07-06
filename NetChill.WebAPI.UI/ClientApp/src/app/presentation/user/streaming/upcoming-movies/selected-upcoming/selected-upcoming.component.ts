import { DatePipe, Location, PopStateEvent } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Subscription } from 'rxjs';


import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { MovieProduction } from 'src/app/core/models/movie/movie-production.model';
import { MovieStreamingService } from 'src/app/shared/services/streaming/movie-streaming.service';
import { environment } from 'src/environments/environment.development';


//Declares variables & functions from script file

/**Regex Validators**/
declare const youtubeRegex: any;

@Component({
  selector: 'app-selected-upcoming',
  templateUrl: './selected-upcoming.component.html',
  styleUrls: ['./selected-upcoming.component.css']
})


export class SelectedUpcomingComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('poster', { static: true }) posterView: ElementRef<HTMLImageElement>;
  @ViewChild('trailer', { static: false }) movieTrailer: ElementRef<HTMLIFrameElement>;

  selectedMovieId: Guid;

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
        this.renderTrailer();
      }
    });
  }

  //#endregion


  //#region Helper Functions

  //Generate file path url
  public fileUrl = (serverPath: string) => {
    return `${environment.baseUrl}/${serverPath}`;
  }

  //Processes and displays poster image
  renderPoster() {
    let posterImg = this.posterView.nativeElement;
    posterImg.src = this.fileUrl(this.mediaData.moviePosterPath);
  }

  //Get DateToLongString from UTC date
  dateToLongString(date: Date): string {
    return this.datePipe.transform(date, 'dd MMMM YYYY') || '';
  }

  //Processes movie trailer url and embeds it to the ifram element
  renderTrailer() {
    let iframe = this.movieTrailer.nativeElement;
    let matchYouTubeUrl = this.mediaData.movieTrailerUrl.match(youtubeRegex);

    if(matchYouTubeUrl){
      const embedUrl = "https://www.youtube.com/embed/"; 
      iframe.src = `${embedUrl+ matchYouTubeUrl[1]}`;
    }
  }

  //#endregion
  
  
  //#region Dispose Functions
  ngAfterViewInit(): void {
    const currentUrl = this.router.url;
    this.subscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if(event.url != currentUrl && event.url != currentUrl+'#watch-trailer'){
          this.infoData = null;
          this.productionData =null;
          this.mediaData = null;
          this.posterView.nativeElement.src = '';
          this.movieTrailer.nativeElement.src = ''; 
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  //#endregion
}