import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appUnless]'
})
export class UnlessDirective {

  constructor(el:ElementRef) {
    el.nativeElement.style.backgroundColor = 'yellow';

   }

}
