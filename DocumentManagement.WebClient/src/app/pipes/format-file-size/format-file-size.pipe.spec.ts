import { TestBed, inject } from '@angular/core/testing';
import { FormatFileSizePipe } from '@app-base/pipes';

describe('FormatFileSizePipe', () => {

	const megaByte = 1024;
	const fileSize = 100;

	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [FormatFileSizePipe]
		});
	});

	it('should be created', inject([FormatFileSizePipe], (pipe: FormatFileSizePipe) => {
		expect(pipe).toBeTruthy();
	}));

	it('should return 0.10 GB', inject([FormatFileSizePipe], (pipe: FormatFileSizePipe) => {
		const size = fileSize * megaByte * megaByte;
		expect(pipe.transform(size, false)).toBe('0.10 GB');
	}));

	it('should return 0.10 Gigabytes', inject([FormatFileSizePipe], (pipe: FormatFileSizePipe) => {
		const size = fileSize * megaByte * megaByte;
		expect(pipe.transform(size, true)).toBe('0.10 Gigabytes');
	}));

});
