import { DataSource } from '@angular/cdk/collections';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/observable/merge';
import 'rxjs/add/operator/map';

import { DocumentDatabase } from '@app-base/documents/documents-list/document-database';
import { DocumentModel } from '@app-base/models/document.model';


export class DocumentDataSource extends DataSource<any> {
	constructor(private documentDatabase: DocumentDatabase) {
		super();
	}

	public connect(): Observable<DocumentModel[]> {
		const displayDataChanges = [
			this.documentDatabase.dataChange,
		];

		return Observable.merge(...displayDataChanges).map(() => {
			return this.getDocumentsData();
		});
	}

	public disconnect() {
		return undefined;
	}

	public setDocumentsData(data: DocumentModel[]): void {
		this.documentDatabase.data = data;
	}

	public getDocumentsData(): DocumentModel[] {
		const data = this.documentDatabase.data.slice();

		return data;
	}
}
