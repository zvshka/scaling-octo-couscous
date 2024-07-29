export interface IMeasurement {
  id: string;
  deviceId: string;
  measurementDate: Date;
  activeInput: number;
  activeOutput: number;
  reactiveInput: number;
  reactiveOutput: number;
}
