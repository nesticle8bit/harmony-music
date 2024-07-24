import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeFormat',
  standalone: true, // Make this pipe standalone
})
export class TimeFormatPipe implements PipeTransform {
  transform(value: number): string {
    const minutes: number = Math.floor(value / 60);
    const seconds: number = value % 60;
    const formattedSeconds: string =
      seconds < 10 ? `0${seconds}` : `${Math.floor(seconds)}`;

    return `${minutes}:${formattedSeconds}`;
  }
}
