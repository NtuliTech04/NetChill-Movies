import { Guid } from 'guid-typescript';
import { Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MovieClipService } from 'src/app/shared/services/movie/movie-clip.service';


@Component({
  selector: 'app-movie-clip',
  templateUrl: './movie-clip.component.html',
  styleUrls: ['./movie-clip.component.css']
})

export class MovieClipComponent implements OnInit {

  @ViewChild('poster', { static: false }) posterInput: ElementRef;
  @ViewChild('clip', { static: false }) clipInput: ElementRef;

  movieRef: Guid;
  submitted: boolean = false;
  uploadFilesForm: FormGroup;
  formData: FormData = new FormData();

  //Image file properties
  posterFile: any;
  posterPreview: any;
  imageMaxSize: number = 2097152; //2MB
  imageAllowedTypes: string[] = ['image/png', 'image/jpeg'];

  //Video file properties
  videoFile: any;
  videoMaxSize: number = 26214400; //25MB
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
      movieTrailerUrl: [null],
      videoClip: [null, [Validators.required]]
    });

    //On trailer input change
    this.uploadFilesForm.get('movieTrailerUrl').valueChanges.subscribe((trailer) => {
      
      const videoControl = this.uploadFilesForm.get('videoClip');
      
      //Clear existing validators
      videoControl.clearValidators();

      //Add new conditional based validators
      if(trailer.length > 0){
        videoControl.setValidators([Validators.nullValidator])
      } else{
        videoControl.setValidators([Validators.required])
      }
      
      //Trigger (update) Validation
      videoControl.updateValueAndValidity();
    });
  }


  //Getter to access form control
  get validateForm() {
    return this.uploadFilesForm.controls;
  }


  /*** Movie Poster ***/

  //If button is clicked the input is also clicked
  browsePoster(){
    this.posterInput.nativeElement.click();
  }

  //Poster file drop handler
  onDroppedPoster(poster: any): void {

    //Sets the poster input element to a variable
    let input = this.posterInput.nativeElement;

    //Creates an instance of a DataTransfer Object
    let dataTransfer = new DataTransfer();

    //Then sets the poster file to it.
    dataTransfer.items.add(poster[0]);

    //Assigns object files to input files
    input.files = dataTransfer.files;

    //Triggers the change and input event for file input element
    input.dispatchEvent(new Event('change'));
    input.dispatchEvent(new Event('input'));

    //=> Goes to posterInputEvent()
  }

  //Gets selected movie poster image file
  posterInputEvent(event: any): void {
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
            reader.readAsDataURL(this.posterFile);
            reader.onload = () => {
              this.posterPreview = reader.result;
            };
            return null; //Validation Succeeds
          }
        }
      }
    };

  /*** //Movie Poster ***/


  /*** Movie Clip ***/
  
  //If button is clicked the input is also clicked
  browseClip(){
    this.clipInput.nativeElement.click();
  }

  //Video file drop handler
  onDroppedClip(video: any): void {

    //Sets the clip input element to a variable
    let input = this.clipInput.nativeElement;

    //Creates an instance of a DataTransfer Object
    let dataTransfer = new DataTransfer();

    //Then sets the clip file to it.
    dataTransfer.items.add(video[0]);

    //Assigns object files to input files
    input.files = dataTransfer.files;

    //Triggers the change and input event for file input element
    input.dispatchEvent(new Event('change'));
    input.dispatchEvent(new Event('input'));

    //=> Goes to videoInputEvent()
  }

  //Gets selected movie video file
  videoInputEvent(event: any): void {

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
            //Show preview & upload progress
            this.videoFile.progress = 0; //Adds progress property to video file info.
            setTimeout(() => {
              const progressInterval = setInterval(() => {
                if (this.videoFile.progress === 100) {
                  clearInterval(progressInterval);
                }
                else {
                  this.videoFile.progress += 5;
                }
              }, 200);
            }, 1000);

            return null; //Validation Succeeds
          }
        }
      }
    };

  //Removes the selected file & resets the file input
  deleteClipFile() {
    this.videoFile = null;
    this.formData.set('videoClip', null);
    this.clipInput.nativeElement.click();
  }

  formatBytes(bytes: any, decimals: any) {
    if (bytes === 0) {
      return '0 Bytes';
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals || 2;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
  }

  /*** //Movie Clip ***/

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

    //Add trailer link to form data
    let trailerLink = this.uploadFilesForm.get('movieTrailerUrl').value;
    this.formData.append('movieTrailerUrl', trailerLink);

    //Use api service
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
