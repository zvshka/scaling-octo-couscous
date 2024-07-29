import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDevice } from '../types/IDevice';
import { HttpClient } from '@angular/common/http';
import DevExpress from 'devextreme';
import Device = DevExpress.Device;

@Injectable({
  providedIn: 'root',
})
export class DevicesService {

  constructor(private http: HttpClient) {
  }

  getDevicesList(search: string, currentPage: number, pageSize: number): Observable<IDevice[]> {
    return this.http.get<IDevice[]>('/api/Devices', {
      headers: {
        authorization: `Bearer ${localStorage.getItem('accessToken')}`,
      },
      params: {
        search: search,
        page: currentPage,
        pageSize: pageSize,
      },
    })
  }

  deleteDevice(id: string): Observable<IDevice> {
    return this.http.delete<IDevice>(`/api/Devices/${id}`, {
      headers: {
        authorization: `Bearer ${localStorage.getItem('accessToken')}`,
      },
    })
  }

  createDevice(name: string) {
    return this.http.post<IDevice>(`/api/Devices/`, {name}, {
      headers: {
        authorization: `Bearer ${localStorage.getItem('accessToken')}`,
      },
    })
  }

  updateDevice(id:string, name: string) {
    return this.http.put<IDevice>(`/api/Devices/${id}`, {name}, {
      headers: {
        authorization: `Bearer ${localStorage.getItem('accessToken')}`,
      },
    })
  }
}
