import { Guid } from 'guid-typescript';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MovieProductionService } from 'src/app/shared/services/movie/movie-production.service';

@Component({
  selector: 'app-movie-production',
  templateUrl: './movie-production.component.html',
  styleUrls: ['./movie-production.component.css']
})


export class MovieProductionComponent implements OnInit {

  submitted: boolean = false;
  movieProductionForm: FormGroup;
  movieRef: Guid;

  myItems: string[] = new Array();
  IsForUpdate: boolean = false;
  newItem: string = "";
  updatedItem: any;
  count: number = 0;

  constructor(
    public builder: FormBuilder,
    private router: Router,
    private activatedroute: ActivatedRoute,
    private ngZone: NgZone,
    private toastr: ToastrService,
    private apiService: MovieProductionService
  ) {
      this.getIdFromURL();
      this.productionFormValidation();
    }


  ngOnInit() {}


  //Gets movie referecence id from URL
  getIdFromURL() {
    this.activatedroute.params.subscribe(params => {
      this.movieRef = params['id'];
    });
  }

  //Form Validation
  productionFormValidation() {
    this.movieProductionForm = this.builder.group({
      movieRef: [this.movieRef],
      directors: [null, [Validators.required]],
      writers: [null],
      movieStars: [null]
    })
  }

  //Getter to access form control
  get validateForm() {
    return this.movieProductionForm.controls;
  }




  //Adding item to array
  AddItem() {
    if (this.myItems.length == 0) {

      if (this.newItem != ""){
          this.myItems.push(this.newItem.trim());
          this.newItem = "";
        }
    }
    else{
      var exist = (<HTMLDivElement>document.getElementById("isExist"));

      if (this.newItem != ""){
        this.count = 0;

        for (let i = 0; i < this.myItems.length; i++) {
          if (this.myItems[i] == this.newItem.trim()) {
            exist.style.display = "block";
            addEventListener("change", () => {
            exist.style.display = "none";
            });
            this.count++;
            break;
          }
        }

        if (this.count == 0) {
          this.myItems.push(this.newItem.trim());
          this.newItem = "";
        }
     }
    }
  }


//Edit array item
EditItem(i: number) {
  this.newItem = this.myItems[i];
  this.IsForUpdate = true;
  this.updatedItem = i;
}

//Update array item
UpdateItem() {
  var exist = (<HTMLDivElement>document.getElementById('isExist'));
  let pos = this.updatedItem;
  this.count = 0;

  for (let i = 0; i < this.myItems.length; i++) {
    if (this.myItems[i] == this.newItem) {
      exist.style.display = "block";
      addEventListener("change", () => {
      exist.style.display = "none";
      });
      this.count++;
      break;
    }
  }

  if (this.newItem != ""){
    for (let i = 0; i < this.myItems.length; i++) {
      if (i == pos && this.count == 0) {
        this.myItems[i] = this.newItem.trim();
        break;
      }
    }
  }

  this.newItem = "";
  this.IsForUpdate = false;
  }


  //Remove array item
  DeleteItem(i: number) {
    this.myItems.splice(i, 1);
  }


  //Populating Text Areas
  populateTextArea(event: any): void{
    if(this.myItems.length !== 0){

      let items = this.myItems.join(', ');

      //Get clicked element.
      let textArea = event.target as HTMLElement;

      //Set value in the respective text area
      this.movieProductionForm.get(textArea.id).setValue(items);

      //Clear array items
      this.myItems.splice(0, this.myItems.length);
    }
  }


  //Clear Text Areas
  clearTextArea(event: any): void{
      //Get clicked element.
      let textArea = event.target as HTMLElement;
      this.movieProductionForm.get(textArea.id).setValue('');
  }

  onSubmit() {
    this.submitted = true;
    if (!this.movieProductionForm.valid) {
      return false;
    }
    else {
      this.insertProductionInfo();
    }
  }


  insertProductionInfo() {
    return this.apiService.creatMovieProduction(this.movieRef, this.movieProductionForm.value)
    .subscribe({
      next: (response) => {
        this.movieRef = response.data;
      },
      error: (ex) => {
        console.log('Failed to create movie production information: ', ex);
        this.toastr.error('Failed To Save', 'Something Went Wrong!')
      },
      complete: () => {
        this.toastr.success('Saved Successfully', 'Step 2: Movie Production')
        this.ngZone.run(() => this.router.navigateByUrl("/MovieMedia/"+this.movieRef));
      }
    });
  }
}
