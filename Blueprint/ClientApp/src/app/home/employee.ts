import { Position } from './position';

export interface Employee {
  id: number;
  fullName: string;
  address: string;
  phoneNumber: string;
  position: Position;
}
