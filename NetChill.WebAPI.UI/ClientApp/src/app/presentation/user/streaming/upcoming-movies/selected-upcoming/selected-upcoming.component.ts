import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-selected-upcoming',
  templateUrl: './selected-upcoming.component.html',
  styleUrls: ['./selected-upcoming.component.css']
})
export class SelectedUpcomingComponent implements OnInit{

  constructor ( private router: Router) 
  {

  }

  ngOnInit(): void {

  }
}






// private lastPoppedUrl: string;
// private routerSubscription: Subscription | undefined;

  // ngAfterViewInit(): void {
  //   this.location.subscribe((ev: PopStateEvent) => {
  //    this.lastPoppedUrl = ev.url;
  //   });
  //   this.routerSubscription = this.router.events.subscribe(event => {
  //     if (event instanceof NavigationStart) {
  //        if(this.lastPoppedUrl == event.url){
  //         window.location.reload()
  //        }
  //     }
  //   });
  // }

  // ngOnDestroy(): void {
  //   this.routerSubscription?.unsubscribe();
  // }