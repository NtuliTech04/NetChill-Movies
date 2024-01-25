import { Location, PopStateEvent } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-selected-upcoming',
  templateUrl: './selected-upcoming.component.html',
  styleUrls: ['./selected-upcoming.component.css']
})
export class SelectedUpcomingComponent implements OnInit, AfterViewInit, OnDestroy{

  private lastPoppedUrl: string;
  private routerSubscription: Subscription | undefined;

  constructor ( private router: Router, private location: Location ) 
  {

  }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.location.subscribe((ev: PopStateEvent) => {
     this.lastPoppedUrl = ev.url;
    });
    this.routerSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
         if(this.lastPoppedUrl == event.url){
          window.location.reload()
         }
      }
    });
  }

  ngOnDestroy(): void {
    this.routerSubscription?.unsubscribe();
  }
}
