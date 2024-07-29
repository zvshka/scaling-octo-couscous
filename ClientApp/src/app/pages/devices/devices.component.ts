import { Component, OnInit } from '@angular/core';
import 'devextreme/data/odata/store';
import { DevicesService } from '../../shared/services/devices.service';
import { IDevice } from '../../shared/types/IDevice';
import notify from 'devextreme/ui/notify';

@Component({
  templateUrl: 'devices.component.html',
})

export class DevicesComponent implements OnInit {

  currentPage = 1
  pageSize = 10
  search = ''
  devices: IDevice[] = []

  constructor(private devicesService: DevicesService) {
  }

  ngOnInit(): void {
    this.devicesService.getDevicesList(this.search, this.currentPage, this.pageSize).subscribe({
      next: (data: IDevice[]) => {
        this.devices = data
      },
    })
  }

  deleteDevice(event: any) {
    this.devicesService.deleteDevice(event.data.id).subscribe({
      next: (data: IDevice) => {
        notify(`Вы успешно удалили ${data.name}`, 'success', 2000)
      },
    })
  }

  createDevice(event: any) {
    this.devicesService.createDevice(event.data.name).subscribe({
      next: () => {
        this.ngOnInit()
      }
    })
  }

  updateDevice(event: any) {
    this.devicesService.updateDevice(event.data.id, event.data.name).subscribe({
      next: () => {
        this.ngOnInit()
      }
    })
  }
}
