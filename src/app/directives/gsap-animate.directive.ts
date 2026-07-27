import { Directive, ElementRef, Input, AfterViewInit, OnDestroy } from '@angular/core';
import { gsap } from 'gsap';
import { ScrollTrigger } from 'gsap/ScrollTrigger';

gsap.registerPlugin(ScrollTrigger);

@Directive({
  selector: '[appGsapAnimate]',
  standalone: true
})
export class GsapAnimateDirective implements AfterViewInit, OnDestroy {
  @Input('appGsapAnimate') animationType: 'stagger-fade-up' | 'zoom-3d' | 'slide-rotate' = 'stagger-fade-up';
  
  private trigger: globalThis.ScrollTrigger | null = null;

  constructor(private el: ElementRef) {}

  ngAfterViewInit() {
    const element = this.el.nativeElement;
    
    // Default initial states based on animation type
    if (this.animationType === 'stagger-fade-up') {
      gsap.set(element, { y: 50, opacity: 0 });
    } else if (this.animationType === 'zoom-3d') {
      gsap.set(element, { scale: 0.8, opacity: 0, rotationX: 15 });
    } else if (this.animationType === 'slide-rotate') {
      gsap.set(element, { x: -100, opacity: 0, rotation: -5 });
    }

    // Create ScrollTrigger animation
    this.trigger = ScrollTrigger.create({
      trigger: element,
      start: 'top 85%', // Starts animation when element is 85% down the viewport
      onEnter: () => this.animateIn(element)
    });
  }

  private animateIn(element: any) {
    if (this.animationType === 'stagger-fade-up') {
      gsap.to(element, { y: 0, opacity: 1, duration: 0.8, ease: 'power3.out' });
    } else if (this.animationType === 'zoom-3d') {
      gsap.to(element, { scale: 1, opacity: 1, rotationX: 0, duration: 1, ease: 'back.out(1.7)' });
    } else if (this.animationType === 'slide-rotate') {
      gsap.to(element, { x: 0, opacity: 1, rotation: 0, duration: 1, ease: 'power4.out' });
    }
  }

  ngOnDestroy() {
    if (this.trigger) {
      this.trigger.kill();
    }
  }
}
