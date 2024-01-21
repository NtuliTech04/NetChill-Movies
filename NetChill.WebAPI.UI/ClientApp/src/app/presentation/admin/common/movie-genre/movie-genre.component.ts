import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MovieGenreService } from 'src/app/shared/services/movie/accessories/movie-genre.service';


//Declares variables & functions from script file

/**Regex Validators**/
declare const properCaseRegex: any;
declare const sentenceCaseRegex: any;

@Component({
  selector: 'app-movie-genre',
  templateUrl: './movie-genre.component.html',
  styleUrls: ['./movie-genre.component.css']

})
export class MovieGenreComponent implements OnInit{

  submitted: boolean = false;
  createGenreForm: FormGroup;


constructor(
  public builder: FormBuilder,
  private router: Router,
  private ngZone: NgZone,
  private toastr: ToastrService,
  private apiService: MovieGenreService
) {
    this.genreFormValidation();
}

ngOnInit() {}

//Form Validation
genreFormValidation() {
  this.createGenreForm = this.builder.group({
    genreName: ['', [Validators.required, Validators.pattern(properCaseRegex)]],
    genreDescription: ['', [Validators.required, Validators.pattern(sentenceCaseRegex)]],
  });
}

//Getter to access form control
get validateForm() {
  return this.createGenreForm.controls;
}


onSubmit(){
  this.submitted = true;
  if (!this.createGenreForm.valid){
    return false;
  }
  else{
    //Calls insert function
    this.insertMovieGenre();
  }
}


insertMovieGenre() {
  return this.apiService.createGenre(this.createGenreForm.value)
  .subscribe({
    complete: () => {
      this.toastr.success('Saved Successfully', 'Movie Genre')
      this.ngZone.run(() => this.router.navigateByUrl('/'));
    },
    error: (ex) => {
      console.log('Failed to create movie genre: ', ex);
      this.toastr.error('Failed To Save', 'Something Went Wrong!')
    }
  });
}

}
