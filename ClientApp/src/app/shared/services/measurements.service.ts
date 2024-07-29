import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMeasurement } from '../types/IMeasurement';

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService {

  constructor(private http: HttpClient) { }

  getMeasurementsByDeviceId(id: string, date: Date = new Date("2024-07-01"), isDescending: boolean = false): Observable<IMeasurement[]> {
    return this.http.get<IMeasurement[]>(`/api/Measurements/${id}`, {
      headers: {
        authorization: `Bearer ${localStorage.getItem('accessToken')}`,
      },
      params:{
        Date: date.toISOString(),
        IsDescending: isDescending,
      }
    })
  }
}
