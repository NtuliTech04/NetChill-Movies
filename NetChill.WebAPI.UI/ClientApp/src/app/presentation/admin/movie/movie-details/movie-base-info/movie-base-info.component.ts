import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';


import { MovieGenre } from 'src/app/core/models/movie/movie-genre.model';
import { MovieLanguage } from 'src/app/core/models/movie/movie-language.model';
import { MovieBaseInfoService } from 'src/app/shared/services/movie/movie-base-info.service';
import { MovieGenreService } from 'src/app/shared/services/movie/accessories/movie-genre.service';
import { MovieLanguageService } from 'src/app/shared/services/movie/accessories/movie-language.service';

//Declares variables & functions from script file

/**Regex Validators**/
declare const titleCaseRegex: any;
declare const sentenceCaseRegex: any;


@Component({
  selector: 'app-movie-base-info',
  templateUrl: './movie-base-info.component.html',
  styleUrls: ['./movie-base-info.component.css']
})

export class MovieBaseInfoComponent implements OnInit {

  submitted: boolean = false;
  addBaseInfoForm: FormGroup;

  movieRef: Guid;
  yearsList: number[] = [];
  genreData: MovieGenre[] = [];
  languageData: MovieLanguage[] = [];


  constructor(
    private router: Router,
    private ngZone: NgZone,
    private datePipe: DatePipe,
    public builder: FormBuilder,
    private toastr: ToastrService,
    private genreService: MovieGenreService,
    private apiService: MovieBaseInfoService,
    private languageService: MovieLanguageService,
  ) {
      this.baseInfoValidation();
  }

  ngOnInit() {
    //Populates these on dropdown lists
    this.populateGenres();
    this.populateLanguages();
    this.populateYears();
  }


  //Checks if Selected date >= today's date
  validateDate(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let selectedDate = control.value;

      if(!selectedDate) {return null;}

      let dateToday = new Date().toDateString();
      dateToday = this.datePipe.transform(dateToday,"yyyy-MM-dd");

      if(selectedDate < dateToday) {
        return {invalidDate: true};
      }
      else{
        return null;
      }
    };
  }

  //Form Validation
  baseInfoValidation() {
    this.addBaseInfoForm = this.builder.group({
      title: ['',
        [
          Validators.required,
          Validators.maxLength(225),
          Validators.pattern(titleCaseRegex)
        ]
      ],
      genre: ['', [Validators.required]],
      description: ['',
        [
          Validators.required,
          Validators.maxLength(8000),
          Validators.pattern(sentenceCaseRegex)
        ]
      ],
      languages: ['', [Validators.required]],
      isFeatured: [false],
      yearReleased: ['', [Validators.required]],
      availableFrom: ['', [Validators.required, this.validateDate()]]
    });
  }

  // Getter to access form control
  get validateForm() {
    return this.addBaseInfoForm.controls;
  }



  //Gets and populates genres data from api services
  populateGenres(){
    this.genreService.getAllGenres().subscribe(data => {
      this.genreData = data['data'];
    });
  }

  //Gets and populates languages data from api services
  populateLanguages(){
    this.languageService.getAllLanguages().subscribe(data => {
      this.languageData = data['data'];
    });
  }

  //Creates and populates years list
  populateYears(){
    const startYear = 1900;
    const endYear = new Date().getFullYear();

    for(let year = endYear; year >= startYear; year--) {
      this.yearsList.push(year);
    }
  }



  onSubmit() {
    this.submitted = true;
    if (!this.addBaseInfoForm.valid) {
      return false;
    }
    else {
      //Converts Array to String
      const genreString = this.addBaseInfoForm.get('genre').value.join(', ');
      const languageString = this.addBaseInfoForm.get('languages').value.join(', ');

      //Replaces form array with a string
      this.addBaseInfoForm.get('genre').setValue(genreString);
      this.addBaseInfoForm.get('languages').setValue(languageString);

      //Calls insert function
      this.insertBaseInfo();
    }
  }


  insertBaseInfo() {
    return this.apiService.addBaseInfo(this.addBaseInfoForm.value)
    .subscribe({
      next: (response) => {
        this.movieRef = response.data;
      },
      error: (ex) => {
        console.log('Failed to create movie base information: ', ex);
        this.toastr.error('Failed To Save', 'Something Went Wrong!')
      },
      complete: () => {
        this.toastr.success('Saved Successfully', 'Step 1: Movie Details')
        this.ngZone.run(() => this.router.navigateByUrl("/MovieProduction/"+this.movieRef));
      }
    });
  }
}



