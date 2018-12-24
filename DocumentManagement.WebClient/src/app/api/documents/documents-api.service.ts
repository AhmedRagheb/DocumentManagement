import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '@app-environments/environment';
import { DocumentModel } from '@app-base/models/document.model';

@Injectable()
export class DocumentsApiService {
	constructor(private httpClient: HttpClient) {

	}

	public getDocuments(): Observable<DocumentModel[]> {

		const headers = this.getDefaultHeaders();
		const url = environment.apiBaseUrl + '/documents/';

		return this.httpClient.get<DocumentModel[]>(url, { headers: headers });
	}

	public downloadDocument(documentId: number): Observable<any> {
		const headers = new HttpHeaders({'userName': 'user'});
		const url = environment.apiBaseUrl + '/documents/downloads/' + documentId;

		return this.httpClient.get(url, {responseType: 'blob', headers: headers});
	}
	
	public deleteDocument(documentId: number): Observable<{}> {

		const headers = this.getDefaultHeaders();
		const url = environment.apiBaseUrl + '/documents/' + documentId;

		return this.httpClient.delete<{}>(url, { headers: headers });
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
