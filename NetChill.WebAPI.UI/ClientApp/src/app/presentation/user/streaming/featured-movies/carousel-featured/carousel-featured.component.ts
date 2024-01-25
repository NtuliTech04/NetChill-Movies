import { Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-carousel-featured',
  templateUrl: './carousel-featured.component.html',
  styleUrls: ['./carousel-featured.component.css']
})
export class CarouselFeaturedComponent {

  id = "34d129fe-4c6d-475e-8546-fa471cfa8616" //Test

  constructor () {}

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

  // ngOnInit() {}

}
