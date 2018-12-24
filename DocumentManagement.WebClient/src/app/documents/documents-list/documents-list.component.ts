import {
	Component, Output, Input, ViewEncapsulation, ViewChild, ViewContainerRef,
	ChangeDetectionStrategy, ChangeDetectorRef, OnInit, TemplateRef, NgZone
} from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import 'rxjs/add/operator/map';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { FileUploader, FileItem, ParsedResponseHeaders, FileLikeObject } from 'ng2-file-upload';
import { FormatFileSizePipe } from '@app-base/pipes';

import { DocumentsService } from '@app-base/documents/services/documents.service';
import { ToasterService } from '@app-base/shared/toast/toaster.service';

import { DocumentModel } from '@app-base/models/document.model';
import { DocumentTypeModel } from '@app-base/models/document-type.model';
import { Toast } from '@app-base/shared/toast/toast';
import { DocumentDatabase } from '@app-base/documents/documents-list/document-database';
import { DocumentDataSource } from '@app-base/documents/documents-list/document-data-source';

import { environment } from '@app-environments/environment';

@Component({
	selector: 'app-documents-list',
	templateUrl: './documents-list.component.html',
	styleUrls: ['./documents-list.component.scss']
})

export class DocumentsListComponent implements OnInit {

	public dataSource: DocumentDataSource | null;
	public modalRef: BsModalRef;
	public uploader: FileUploader;
	public displayedColumns: string[] = ['name', 'download', 'delete'];

	private documents: DocumentModel[];

	private errorMessage: string;
	private allowedFileType: string[] = [];
	private allowedMineType: string[] = [];
	private readonly megaByte: number = 1024;
	private readonly maxFileSize: number = 100 * this.megaByte * this.megaByte;
	private readonly queueLimit = 10;

	constructor(
		private translateService: TranslateService,
		private toasterService: ToasterService,
		private documentsService: DocumentsService,
		private cd: ChangeDetectorRef,
		private modalService: BsModalService,
		private formatFileSizePipe: FormatFileSizePipe
	) {
	}

	public ngOnInit() {

		this.getDocuments();
		this.getDocumentsTypes();
		this.cd.markForCheck();
	}

	public openModal(template: TemplateRef<any>, data?: any, cssClass?: string) {
		this.modalRef = this.modalService.show(template, { class: cssClass });
		this.modalRef.content = data;
	}

	private getDocuments() {
		this.documentsService.getDocuments().map((documents: DocumentModel[]) => {
			this.documents = documents;
			const database = new DocumentDatabase(this.documents);
			this.dataSource = new DocumentDataSource(database);
		}).subscribe();
	}

	private getDocumentsTypes() {
		this.documentsService.getDocumentsTypes().map((documents: DocumentTypeModel[]) => {
			this.allowedFileType = documents.map(x => x.extention);
			this.allowedMineType = documents.map(x => x.contentType);
			
			this.initUploader();
		})
		.subscribe();
	}

	private initUploader() {
		this.uploader = new FileUploader({
			url: environment.apiBaseUrl + '/documents/',
			headers: [{ name: 'userName', value: 'user' }, { name: 'Admin', value: '1' }],
			removeAfterUpload: true,
			maxFileSize: this.maxFileSize,
			allowedMimeType: this.allowedMineType,
			queueLimit: 10
		});
		this.uploader.onErrorItem = (item, response, status, headers) => this.onErrorItem(item, response, status, headers);
		this.uploader.onSuccessItem = (item, response, status, headers) => this.onSuccessItem(item, response, status, headers);
		this.uploader.onWhenAddingFileFailed = (item, filter, options) => this.onWhenAddingFileFailed(item, filter, options);
		this.uploader.onAfterAddingFile = () => {
			this.errorMessage = '';
		};
		this.uploader.onCompleteAll = () => { };
	}

	private uploadAll(target: any) {
		this.uploader.uploadAll();
	}

	private onSuccessItem(item: FileItem, response: string, status: number, headers: ParsedResponseHeaders): any {
		this.documents.push(JSON.parse(response));
		this.dataSource.setDocumentsData(this.documents);
		this.modalRef.hide();
		this.translateService.get('file-upload-file-uploaded-success-message').subscribe((message: string) => {
			this.toasterService.pop('success', 'success', message);
		});
		this.cd.markForCheck();
	}

	private onErrorItem(item: FileItem, response: string, status: number, headers: ParsedResponseHeaders): any {
		this.modalRef.hide();
		this.translateService.get('file-upload-error-message').subscribe((message: string) => {
			this.toasterService.pop('error', 'error', message);
		});
		this.cd.markForCheck();
	}

	private onWhenAddingFileFailed(item: FileLikeObject, filter: any, options: any): any {
		switch (filter.name) {
			case 'fileSize':
				this.translateService.get('file-upload-max-size-exceeded-error-message').subscribe((message: string) => {
					const itemSize = this.formatFileSizePipe.transform(item.size, false);
					const maxFileSize = this.formatFileSizePipe.transform(this.maxFileSize, false);
					this.errorMessage = message.replace('{0}', itemSize).replace('{1}', maxFileSize);
				});
				break;
			case 'mimeType':
				this.translateService.get('file-upload-wrong-file-type-error-message').subscribe((message: string) => {
					const allowedTypes = this.allowedFileType.join(', ');
					this.errorMessage = message.replace('{0}', allowedTypes);
				});
				break;
			case 'queueLimit':
				this.translateService.get('file-upload-queue-limit-reached-error-message').subscribe((message: string) => {
					this.errorMessage = message.replace('{0}', `${this.queueLimit}`);
				});
				break;
			default:
				break;
		}
	}

	public download(documentId: number, name: string) {
		this.documentsService.downloadDocument(documentId, name)
	}

	public deleteDocument(target: any, documentId: number) {
		this.documentsService.deleteDocument(documentId)
			.map(() => {
				const removedItemIndex = this.documents.findIndex((item: DocumentModel) => {
					return item.documentId === documentId;
				});

				if (removedItemIndex > -1) {
					this.documents.splice(removedItemIndex, 1);
					this.dataSource.setDocumentsData(this.documents);
					this.translateService.get('delete-file-success-message').subscribe((message: string) => {
						this.toasterService.pop('success', 'success', message);
					});
					this.cd.markForCheck();
					this.modalRef.hide();
				}
			})
			.subscribe();
	}
}