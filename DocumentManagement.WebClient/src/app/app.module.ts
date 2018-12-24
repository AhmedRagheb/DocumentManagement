import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CdkTableModule } from '@angular/cdk/table';
import { BsDropdownModule, ModalModule, PopoverModule } from 'ngx-bootstrap';
import { FileUploadModule } from 'ng2-file-upload/ng2-file-upload';
import { FormatFileSizePipe } from '@app-base/pipes';

import { AppComponent } from './app.component';
import { DocumentsListComponent } from '@app-base/documents/documents-list/documents-list.component';
import { ApiModule } from '@app-base/api/api.module';
import { DocumentsService } from '@app-base/documents/services/documents.service';

import { ToasterModule } from '@app-shared/toast/toaster.module';
import { ToasterService } from '@app-shared/toast/toaster.service';

/* Factory exports */
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

@NgModule({
  declarations: [
    AppComponent,
    DocumentsListComponent,
    FormatFileSizePipe
  ],
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    ApiModule,
    BrowserModule,
    CdkTableModule,
    ToasterModule,
    ModalModule.forRoot(),
    FileUploadModule,
    TranslateModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  exports: [
    TranslateModule,
    ModalModule,
    CdkTableModule,
    FileUploadModule,
    FormatFileSizePipe,
    ToasterModule
  ],
  providers: [
    DocumentsService,
    FormatFileSizePipe,
    ToasterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
