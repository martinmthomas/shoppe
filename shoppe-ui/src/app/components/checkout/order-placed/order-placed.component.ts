import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'order-placed',
  templateUrl: './order-placed.component.html',
  styleUrls: ['./order-placed.component.scss']
})
export class OrderPlacedComponent implements OnInit {

  orderId = '';

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParamMap
      .subscribe(param => this.orderId = param.get('orderId') ?? '')
  }
}
