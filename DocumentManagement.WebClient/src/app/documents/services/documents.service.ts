import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import { saveAs } from 'file-saver/FileSaver';

import { DocumentsApiService, DocumentsTypesApiService } from '@app-api/documents';

import { DocumentModel } from '@app-base/models/document.model';
import { DocumentTypeModel } from '@app-base/models/document-type.model';

@Injectable()
export class DocumentsService {

    constructor(
        private documentsApiService: DocumentsApiService,
        private documentsTypesApiService: DocumentsTypesApiService) 
    {}

	public getDocuments(): Observable<DocumentModel[]> {
        return this.documentsApiService.getDocuments();
    }

    public downloadDocument(id: number, name: string): void {
        this.documentsApiService.downloadDocument(id).subscribe(res => {
			saveAs(res, name);
		});
    }

    public deleteDocument(id: number): Observable<{}> {
        return this.documentsApiService.deleteDocument(id);
    }

    public getDocumentsTypes(): Observable<DocumentTypeModel[]> {
        return this.documentsTypesApiService.getDocumentsTypes();
    }
}