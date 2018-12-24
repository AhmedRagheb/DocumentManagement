import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '@app-environments/environment';
import { DocumentTypeModel } from '@app-base/models/document-type.model';

@Injectable()
export class DocumentsTypesApiService {
	constructor(private httpClient: HttpClient) {

	}

	public getDocumentsTypes(): Observable<DocumentTypeModel[]> {

		const headers = this.getDefaultHeaders();
		const url = environment.apiBaseUrl + '/documents-Types/';

		return this.httpClient.get<DocumentTypeModel[]>(url, { headers: headers });
	}

	private getDefaultHeaders(): HttpHeaders {
		const headers = new HttpHeaders({
			'userName': 'user',
			'Content-Type': 'application/json',
			Accept: 'q=0.8;application/json;q=0.9',
			Pragma: 'no-cache'
		});

		return headers;
	}
}
