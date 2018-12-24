import { Pipe, PipeTransform } from '@angular/core';

const FILE_SIZE_UNITS = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
const FILE_SIZE_UNITS_LONG = ['Bytes', 'Kilobytes', 'Megabytes', 'Gigabytes', 'Pettabytes', 'Exabytes', 'Zettabytes', 'Yottabytes'];

@Pipe({
	name: 'formatFileSize'
})
export class FormatFileSizePipe implements PipeTransform {
	private readonly megaByte: number = 1024;
	private readonly precision: number = 2;

	public transform(sizeInBytes: number, longForm: boolean): string {
		const units = longForm
			? FILE_SIZE_UNITS_LONG
			: FILE_SIZE_UNITS;

		let power = Math.round(Math.log(sizeInBytes) / Math.log(this.megaByte));
		power = Math.min(power, units.length - 1);

		const size = sizeInBytes / Math.pow(this.megaByte, power); // size in new units
		const formattedSize = size.toFixed(this.precision);
		const unit = units[power];

		return `${formattedSize} ${unit}`;
	}
}
