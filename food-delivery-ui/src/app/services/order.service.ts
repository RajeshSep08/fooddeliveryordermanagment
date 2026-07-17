import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../models/order.model';
import { environment } from '../../environments/environment';

export interface DashboardSummary {
  totalOrders: number;
  placedOrders: number;
  preparingOrders: number;
  outForDeliveryOrders: number;
  deliveredOrders: number;
  cancelledOrders: number;
  totalRevenue: number;
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private readonly baseUrl = `${environment.apiUrl}/orders`;

  constructor(private readonly http: HttpClient) {}

  /** Retrieves all orders from the API. */
  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl);
  }

  /** Retrieves a single order by its identifier. */
  getOrderById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.baseUrl}/${id}`);
  }

  /** Searches orders by customer name and/or status. */
  searchOrders(customerName?: string, status?: string): Observable<Order[]> {
    let params = new HttpParams();

    if (customerName) {
      params = params.set('customerName', customerName);
    }

    if (status) {
      params = params.set('status', status);
    }

    return this.http.get<Order[]>(`${this.baseUrl}/search`, { params });
  }

  /** Creates a new order. */
  createOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.baseUrl, order);
  }

  /** Updates an existing order. */
  updateOrder(id: number, order: Order): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, order);
  }

  /** Updates the status of an existing order. */
  updateOrderStatus(id: number, status: string): Observable<void> {
    return this.http.patch<void>(`${this.baseUrl}/${id}/status`, { status });
  }

  /** Deletes an existing order by its identifier. */
  deleteOrder(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  /** Retrieves dashboard metrics for order reporting. */
  getDashboardSummary(): Observable<DashboardSummary> {
    return this.http.get<DashboardSummary>(`${this.baseUrl}/dashboard-summary`);
  }
}
