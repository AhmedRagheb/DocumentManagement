import { NgModule } from '@angular/core';
import { DocumentsApiService, DocumentsTypesApiService } from './documents';

@NgModule({
	providers: [
		DocumentsApiService,
		DocumentsTypesApiService
	]
})
export class ApiModule { }
