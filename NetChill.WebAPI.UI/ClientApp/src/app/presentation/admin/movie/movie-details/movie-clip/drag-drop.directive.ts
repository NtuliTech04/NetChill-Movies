import {
  Directive,
  Output,
  EventEmitter,
  HostBinding,
  HostListener
} from '@angular/core';

@Directive({
  selector: '[appDragDrop]'
})

export class DragDropDirective {

  @HostBinding('class.drag-over') isDragging: boolean;
  @Output() fileDropped = new EventEmitter<any>();

  //Drag Over Listener
  @HostListener('dragover', ['$event']) onDragOver(event: any) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = true;
  }

  //Drag Leave Listener
  @HostListener('dragleave', ['$event']) public onDragLeave(event: any) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
  }

  //Drop Listener
  @HostListener('drop', ['$event']) public onDrop(event: any){
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;

    let file = event.dataTransfer.files;
    if(file.length > 0) {
      this.fileDropped.emit(file);
    }
  }
}
