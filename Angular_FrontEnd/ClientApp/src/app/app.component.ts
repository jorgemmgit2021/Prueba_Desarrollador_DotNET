import { Component } from '@angular/core';
import {trigger, animate, style, group, animateChild, query, stagger, transition} from '@angular/animations';
// Animations
const fade = [
  query(':self', 
    [
      style({ opacity: 0 })
    ], 
    { optional: true }
  ),

  query(':self',
    [
      style({ opacity: 0 }),
      animate('.3s', style({ opacity: 1 }))
    ], 
    { optional: true }
  )
];

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  animations: [
    trigger('routerAnimations', [
      transition('* => *', fade)
    ])
  ]
})
export class AppComponent {
  title = 'Aplicacion de prueba';
prepareRouteTransition(outlet) {
  const animation = outlet.activatedRouteData['animation'] || {};
  return animation['value'] || null;
}
}

