import { AfterViewInit, Component, Input } from '@angular/core';
import { IMeasurement } from '../../types/IMeasurement';
import { MeasurementsService } from '../../services/measurements.service';
import { DevExtremeModule, DxDataGridModule } from 'devextreme-angular';
import * as dayjs from 'dayjs';
import DevExpress from 'devextreme';
import ValueChangedEvent = DevExpress.ui.dxDateBox.ValueChangedEvent;

@Component({
  selector: 'app-measurements',
  templateUrl: './measurements.component.html',
  styleUrls: ['./measurements.component.scss'],
  standalone: true,
  imports: [
    DxDataGridModule,
    DevExtremeModule,
  ],
})
export class MeasurementsComponent implements AfterViewInit {
  @Input() deviceId: string = "";
  searchDate = dayjs().toDate()
  measurements: IMeasurement[] = []

  constructor(private measurementService: MeasurementsService) {
  }

  ngAfterViewInit(): void {
      this.measurementService.getMeasurementsByDeviceId(this.deviceId, this.searchDate).subscribe({
        next: (data: IMeasurement[]) => {
          this.measurements = data
        }
      })
  }

  selectDate(event: any) {
    this.searchDate = event.value
    this.measurementService.getMeasurementsByDeviceId(this.deviceId, this.searchDate).subscribe({
      next: (data: IMeasurement[]) => {
        this.measurements = data
      }
    })
  }

  protected readonly dayjs = dayjs;
}
