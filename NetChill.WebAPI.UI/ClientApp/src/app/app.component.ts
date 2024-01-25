import { Component } from '@angular/core';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})

export class AppComponent {
  title = "NetChill Movies";
}



 // private routerSubscription: Subscription | undefined; 

  // constructor(private router: Router, private location: Location) {}

  // ngOnInit(): void {
   
  // }

  // ngAfterViewInit(): void {
  //   this.routerSubscription = this.router.events.subscribe((event: any) => {
  //     if (event instanceof NavigationEnd) {
  //       if (event.url == "/Home") {
  //         const href = window.location.origin + '#/Home';
  //         window.location.href = href;
  //       }
  //     }
  //   });
  // }

  // ngOnDestroy(): void {
  //   this.routerSubscription?.unsubscribe();
  // }

//private lastPoppedUrl: string;
// private yScrollStack: number[] = [];


  // ngOnInit(): void {
  //   this.scrollTop();
  // }

  // scrollTop() {
  //   this.location.subscribe((ev: PopStateEvent) => {
  //     this.lastPoppedUrl = ev.url;
  //   });
  //   this.router.events.subscribe((ev: any) => {
  //     if (ev instanceof NavigationStart) {
  //       if (ev.url != this.lastPoppedUrl)
  //         this.yScrollStack.push(window.scrollY);
  //     } else if (ev instanceof NavigationEnd) {
  //       if (ev.url == this.lastPoppedUrl) {
  //         this.lastPoppedUrl = undefined;
  //         window.scrollTo(0, this.yScrollStack.pop());
  //         setTimeout(() => { //Reload onpage landing
  //           window.location.reload();
  //         }, 0);
  //       }
  //       // else window.scrollTo(0, 0);
  //       else document.body.scrollTop = 0;
  //     }
  //   });
  // }



