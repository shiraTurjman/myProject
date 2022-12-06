//import { CloneVisitor } from '@angular/compiler/src/i18n/i18n_ast';
import { Directive, ElementRef, HostListener } from '@angular/core';


@Directive({
  selector: '[appBprice]'
})
export class BpriceDirective {


  constructor(private el: ElementRef) {
  }


  // @HostListener('mouseover') onmouseover() {
  //   this.el.nativeElement.style.background = 'yellow';

  // }
  // @HostListener('mouseenter') onmouseenter() {
  //   this.el.nativeElement.style.color = 'red';
  // }
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


