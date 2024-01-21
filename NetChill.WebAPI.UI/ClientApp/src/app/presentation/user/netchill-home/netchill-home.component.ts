import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { MovieBaseInfo } from 'src/app/core/models/movie/movie-base-info.model';
import { MovieClip } from 'src/app/core/models/movie/movie-clip.model';
import { MovieProduction } from 'src/app/core/models/movie/movie-production.model';
import { MovieStreamingService } from 'src/app/shared/services/streaming/movie-streaming.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-netchill-home',
  templateUrl: './netchill-home.component.html',
  styleUrls: ['./netchill-home.component.css']
})

export class NetchillHomeComponent implements OnInit{

  BaseInfoData: MovieBaseInfo[] = [];
  ProductionData: MovieProduction[] = [];
  MediaFilesData: MovieClip[] = [];
  id = "34d129fe-4c6d-475e-8546-fa471cfa8616" //Test

  constructor(private apiService: MovieStreamingService)
  {
  }

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


  ngOnInit() {
    this.populateBaseInfoes();
    this.populateProductions();
    this.populateMediaFiles();
  }

  //Get and populate movies base info
  populateBaseInfoes() {
    this.apiService.getBaseInfoList()
    .subscribe({
      next:(data) => {
        this.BaseInfoData = data['data'] as MovieBaseInfo[]
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate movies info data', ex);
      }
    });
  }

  //Get and populate movie productions
  populateProductions() {
    this.apiService.getProductionList()
    .subscribe({
      next:(data) => {
        this.ProductionData = data['data'] as MovieProduction[]
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate movie productions data', ex);
      }
    });
  }

  //Get and populate movie files
  populateMediaFiles() {
    this.apiService.getAllMediaFiles()
    .subscribe({
      next:(data) => {
        this.MediaFilesData = data['data'] as MovieClip[]
      },
      error: (ex: HttpErrorResponse) => {
        console.log('Failed to populate movie streaming data', ex);
      }
    });
  }



  //Creating media files path
  public filePath = (serverPath: string) => {
    return `${environment.baseUrl}/${serverPath}`;
  }
}
