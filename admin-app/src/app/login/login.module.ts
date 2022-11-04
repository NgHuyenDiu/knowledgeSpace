import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { TranslateModule } from '@ngx-translate/core';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { NgxSpinnerService } from 'ngx-spinner';
import {TranslateFakeLoader,TranslateLoader,TranslateModule,TranslateService } from '@ngx-translate/core';

@NgModule({
    imports: [
        CommonModule,
       // TranslateModule.forChild(),
       TranslateModule.forRoot({
                  loader: {
                    provide: TranslateLoader,
                    useClass: TranslateFakeLoader
                  }
                }),
        LoginRoutingModule],
    declarations: [LoginComponent],
    providers:[NgxSpinnerService]
})
export class LoginModule {}
