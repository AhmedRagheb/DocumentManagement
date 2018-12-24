import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { environment } from '@app-environments/environment';
import { DocumentModel } from '@app-base/models/document.model';
import { DocumentsTypesApiService } from '@app-base/api/documents';

describe('DocumentsTypesApiService', () => {
	let documentsApiService: DocumentsTypesApiService;
	let httpMock: HttpTestingController;

	beforeEach(() => {
		TestBed.configureTestingModule({
			imports: [
				HttpClientModule,
				HttpClientTestingModule
			],
			providers: [
				DocumentsTypesApiService,
			]
		});

		documentsApiService = TestBed.get(DocumentsTypesApiService);
		httpMock = TestBed.get(HttpTestingController);
	});



	describe('getDocumentsTypes', () => {

		it('should issue a request',
			inject([HttpClient, HttpTestingController, DocumentsTypesApiService],
				(http: HttpClient, backend: HttpTestingController, service: DocumentsTypesApiService) => {
					// 3. send a simple request
					service.getDocumentsTypes().subscribe(returnValue => {
						backend.expectOne({
							url: '/documents-Types/',
							method: 'GET'
						});
					});
				})
		);
	});

});