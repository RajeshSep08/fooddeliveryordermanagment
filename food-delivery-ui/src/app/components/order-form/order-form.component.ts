import { CommonModule, Location } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Order } from '../../models/order.model';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {
  @Input() mode: 'create' | 'edit' = 'create';
  @Input() orderId?: number;

  orderForm!: FormGroup;
  isSubmitting = false;
  readonly orderStatuses = ['Placed', 'Preparing', 'OutForDelivery', 'Delivered', 'Cancelled'];

  constructor(
    private readonly fb: FormBuilder,
    private readonly orderService: OrderService,
    private readonly location: Location
  ) {}

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      customerName: ['', [Validators.required, Validators.maxLength(100)]],
      customerPhone: ['', [Validators.required, Validators.maxLength(20)]],
      foodItem: ['', [Validators.required, Validators.maxLength(100)]],
      quantity: [1, [Validators.required, Validators.min(1)]],
      price: [0, [Validators.required, Validators.min(0)]],
      deliveryAddress: ['', [Validators.required, Validators.maxLength(250)]],
      status: ['Placed', [Validators.required]],
      orderDate: [this.getDefaultOrderDate(), [Validators.required]]
    });

    if (this.mode === 'edit' && this.orderId) {
      this.loadOrder(this.orderId);
    }
  }

  getControl(controlName: string) {
    return this.orderForm.get(controlName);
  }

  isInvalid(controlName: string): boolean {
    const control = this.getControl(controlName);
    return !!control && (control.touched || control.dirty) && control.invalid;
  }

  getErrorMessages(controlName: string): string[] {
    const control = this.getControl(controlName);

    if (!control || !control.errors) {
      return [];
    }

    const messages: string[] = [];

    if (control.errors['required']) {
      messages.push('This field is required.');
    }

    if (control.errors['min']) {
      messages.push('Value must be greater than or equal to 0.');
    }

    if (control.errors['maxlength']) {
      messages.push(`Maximum length is ${control.errors['maxlength'].requiredLength}.`);
    }

    return messages;
  }

  onSubmit(): void {
    if (this.orderForm.invalid) {
      this.orderForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    const orderPayload: Order = this.orderForm.value as Order;

    const request = this.mode === 'create'
      ? this.orderService.createOrder(orderPayload)
      : this.orderService.updateOrder(this.orderId!, orderPayload);

    request.subscribe({
      next: () => {
        this.isSubmitting = false;
        this.location.back();
      },
      error: () => {
        this.isSubmitting = false;
      }
    });
  }

  goBack(): void {
    this.location.back();
  }

  private loadOrder(id: number): void {
    this.orderService.getOrderById(id).subscribe({
      next: (order) => {
        this.orderForm.patchValue(order);
      }
    });
  }

  private getDefaultOrderDate(): string {
    return new Date().toISOString().split('T')[0];
  }
}
