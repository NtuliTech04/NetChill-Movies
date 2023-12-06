import { Guid } from 'guid-typescript';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MovieClipService } from 'src/app/shared/services/movie/movie-clip.service';


@Component({
  selector: 'app-movie-clip',
  templateUrl: './movie-clip.component.html',
  styles: [
  ]
})

export class MovieClipComponent implements OnInit {

  movieRef: Guid;
  submitted: boolean = false;
  uploadFilesForm: FormGroup;
  formData: FormData = new FormData();

  //Image file properties
  posterFile: any;
  posterPreview: any;
  imageMaxSize: number = 2097152;
  imageAllowedTypes: string[] = ['image/png', 'image/jpeg'];

  //Video file properties
  videoFile: any;
  videoMaxSize: number = 26214400;
  videoAllowedTypes: string[] = ['video/mp4','video/webm','video/ogg'];


  constructor
  (
    public builder: FormBuilder,
    private router: Router,
    private activatedroute: ActivatedRoute,
    private ngZone: NgZone,
    private toastr: ToastrService,
    private apiService: MovieClipService
  )
  {
    this.getIdFromURL();
    this.uploadFormValidation();
  }

  ngOnInit() {
    this.formData.append('movieRef', this.movieRef.toString());
  }

  //Gets movie referecence id from URL
  getIdFromURL() {
    this.activatedroute.params.subscribe(params => {
      this.movieRef = params['id'];
    });
  }

  //Form Validation
  uploadFormValidation() {
    this.uploadFilesForm = this.builder.group({
      moviePoster: [null, [Validators.required]],
      videoClip: [null, [Validators.required]]
    });
  }

  //Getter to access form control
  get validateForm() {
    return this.uploadFilesForm.controls;
  }


  //Gets selected movie poster image file
  onSelectedPoster(event: any): void {

    //Assign image file data to global variable
    this.posterFile = event.target.files[0];

    let posterFile = this.uploadFilesForm.get('moviePoster');
    posterFile.setValidators(
      [
        posterFile.validator,
        this.imageFileValidator(this.imageAllowedTypes, this.imageMaxSize).bind(this)
      ]
    )
    posterFile.updateValueAndValidity();  //Trigger validation
  }

  //Validates image file and appends to formData
  imageFileValidator(imageTypes: string[], maxSize: number): ValidatorFn {
    return (): ValidationErrors | null => {
        if(this.posterFile){
          if(imageTypes && imageTypes.indexOf(this.posterFile.type) === -1) {
            return { invalidImageType: true };
          }
          if(this.posterFile.size > maxSize) {
            return { invalidImageSize: true };
          }
          else{
            this.formData.append('moviePoster', this.posterFile);

            //Show Poster Preview
            var reader = new FileReader();

            reader.onload = () => {
              this.posterPreview = reader.result;
            };
            reader.readAsDataURL(this.posterFile);

            return null; //Validation Succeeds
          }
        }
      }
    };


  //Gets selected movie video file
  onSelectedVideo(event: any): void {

    //Assign file data to global variable
    this.videoFile = event.target.files[0];

    let videoFile = this.uploadFilesForm.get('videoClip');
    videoFile.setValidators(
      [
        videoFile.validator,
        this.videoFileValidator(this.videoAllowedTypes, this.videoMaxSize).bind(this)
      ]
    )
    videoFile.updateValueAndValidity();  //Trigger validation
  }

  //Validates movie video file and appends to formData
  videoFileValidator(videoTypes: string[], maxSize: number): ValidatorFn {
    return (): ValidationErrors | null => {
        if(this.videoFile){
          if(videoTypes && videoTypes.indexOf(this.videoFile.type) === -1) {
            return { invalidVideoType: true };
          }
          if(this.videoFile.size > maxSize) {
            return { invalidVideoSize: true };
          }
          else{
            this.formData.append('videoClip', this.videoFile);
            return null; //Validation Succeeds
          }
        }
      }
    };


  onSubmit() {
    this.submitted = true;
    if (!this.uploadFilesForm.valid) {
      return false;
    }
    else{
      this.uploadMovieFiles();
    }
  }


  uploadMovieFiles() {
    return this.apiService.uploadMovieFiles(this.movieRef, this.formData)
    .subscribe({
      next: (response) => {
        this.movieRef = response.data;
      },
      error: (ex) => {
        console.log('Failed to upload movie files: ', ex);
        this.toastr.error('Failed To Save', 'Something Went Wrong!')
      },
      complete: () => {
        this.toastr.success('Movie Uploading Completed', 'Step 3: Movie Files')
        this.ngZone.run(() => this.router.navigateByUrl("/Dashboard"));
      }
    });
  }
}
