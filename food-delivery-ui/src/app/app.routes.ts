import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { OrderFormComponent } from './components/order-form/order-form.component';

@Component({
  selector: 'app-order-list',
  standalone: true,
  template: `
    <div class="container py-4">
      <h2 class="h3">Order List</h2>
      <p class="text-muted">Order list view will be implemented here.</p>
    </div>
  `
})
class OrderListComponent {}

@Component({
  selector: 'app-order-form-route',
  standalone: true,
  imports: [OrderFormComponent],
  template: `<app-order-form [mode]="mode" [orderId]="orderId"></app-order-form>`
})
class OrderFormRouteComponent implements OnInit {
  mode: 'create' | 'edit' = 'create';
  orderId?: number;

  constructor(private readonly route: ActivatedRoute) {}

  ngOnInit(): void {
    const path = this.route.snapshot.url[0]?.path;
    this.mode = path === 'edit' ? 'edit' : 'create';

    const idParam = this.route.snapshot.paramMap.get('id');
    this.orderId = idParam ? Number(idParam) : undefined;
  }
}

export const routes: Routes = [
  { path: '', component: DashboardComponent, pathMatch: 'full' },
  { path: 'orders', component: OrderListComponent },
  { path: 'orders/new', component: OrderFormRouteComponent },
  { path: 'orders/edit/:id', component: OrderFormRouteComponent }
];
