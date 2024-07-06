import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MovieLanguageService } from 'src/app/shared/services/movie/accessories/movie-language.service';


//Declares variables & functions from script file

/**Regex Validators**/
declare const properCaseRegex: any;
declare const sentenceCaseRegex: any;

@Component({
  selector: 'app-movie-language',
  templateUrl: './movie-language.component.html',
  styleUrls: ['./movie-language.component.css']
})
export class MovieLanguageComponent implements OnInit {

  submitted: boolean = false;
  createLanguageForm: FormGroup;

constructor(
  public builder: FormBuilder,
  private router: Router,
  private ngZone: NgZone,
  private toastr: ToastrService,
  private apiService: MovieLanguageService
) {
    this.languageFormValidation();
}

ngOnInit() {}

//Form Validation
languageFormValidation() {
  this.createLanguageForm = this.builder.group({
    spokenLanguage: ['', [Validators.required, Validators.pattern(properCaseRegex)]],
    languageNotes: ['', [Validators.required, Validators.pattern(sentenceCaseRegex)]]
  });
}

//Getter to access form control
get validateForm() {
  return this.createLanguageForm.controls;
}

onSubmit() {
  this.submitted = true;
  if (!this.createLanguageForm.valid) {
    return false;
  }
  else{
    //Calls insert function
    this.insertMovieLanguage();
  }
}


insertMovieLanguage() {
  return this.apiService.createLanguage(this.createLanguageForm.value)
  .subscribe({
    complete: () => {
      this.toastr.success('Saved Successfully', 'Movie Language')
      this.ngZone.run(() => this.router.navigateByUrl('/'));
    },
    error: (ex) => {
      console.log('Failed to create movie language: ', ex);
      this.toastr.error('Failed To Save', 'Something Went Wrong!')
    }
  });
}

}
