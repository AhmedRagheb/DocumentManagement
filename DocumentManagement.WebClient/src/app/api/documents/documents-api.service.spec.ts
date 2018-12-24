import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { environment } from '@app-environments/environment';
import { DocumentModel } from '@app-base/models/document.model';
import { DocumentsApiService } from '@app-base/api/documents';

describe('DocumentsApiService', () => {
	let documentsApiService: DocumentsApiService;
	let httpMock: HttpTestingController;

	beforeEach(() => {
		TestBed.configureTestingModule({
			imports: [
				HttpClientModule,
				HttpClientTestingModule
			],
			providers: [
				DocumentsApiService,
			]
		});

		documentsApiService = TestBed.get(DocumentsApiService);
		httpMock = TestBed.get(HttpTestingController);
	});



	describe('getDocuments', () => {

		it('should issue a request',
			inject([HttpClient, HttpTestingController, DocumentsApiService],
				(http: HttpClient, backend: HttpTestingController, service: DocumentsApiService) => {
					// 3. send a simple request
					service.getDocuments().subscribe(returnValue => {
						backend.expectOne({
							url: '/documents/',
							method: 'GET'
						});
					});
				})
		);
	});

	describe('downloadDocument', () => {

		it('should issue a request',
			inject([HttpClient, HttpTestingController, DocumentsApiService],
				(http: HttpClient, backend: HttpTestingController, service: DocumentsApiService) => {
					// 3. send a simple request
					service.downloadDocument(1).subscribe(returnValue => {
						backend.expectOne({
							url: '/documents/downloads/',
							method: 'GET'
						});
					});
				})
		);
	});

	describe('deleteDocument', () => {

		it('should issue a request',
			inject([HttpClient, HttpTestingController, DocumentsApiService],
				(http: HttpClient, backend: HttpTestingController, service: DocumentsApiService) => {
					// 3. send a simple request
					service.deleteDocument(1).subscribe(returnValue => {
						backend.expectOne({
							url: '/documents/',
							method: 'DELETE'
						});
					});
				})
		);
	});

});