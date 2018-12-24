import { DataSource } from '@angular/cdk/collections';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { DocumentModel } from '@app-base/models/document.model';

export class DocumentDatabase {
	public dataChange: BehaviorSubject<DocumentModel[]> = new BehaviorSubject<DocumentModel[]>([]);
	get data(): DocumentModel[] { return this.dataChange.value; }
	set data(data: DocumentModel[]) { this.dataChange.next(data) }
	constructor(data: DocumentModel[]) {
		this.dataChange.next(data);
	}
}
