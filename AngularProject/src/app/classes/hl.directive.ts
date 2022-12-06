import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appHl]'
})
export class HlDirective {

  constructor(private el :ElementRef) { }

  @HostListener('mouseenter') mouseenter()
  {
    this.el.nativeElement.style.fontWeight='bold'
  
  }
  
  @HostListener('mouseenter')
  onMouseEnter() {
    this.Highlight('yellow')
  }
  Highlight(color: string): void {
    this.el.nativeElement.style.backgroundColor = color;
  }


  @HostListener('mouseleave')
  onMouseleave() {
    this.Highlight('');
  }
}
