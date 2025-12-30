import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { TpnFormComponent } from './tpn-form/tpn-form.component';
import { AppInitService } from './services/app-init.service';
import { TpnCalculationService } from './services/tpn-calculation.service';

export function initializeApp(appInitService: AppInitService) {
  return (): Promise<void> => appInitService.init();
}

@NgModule({
  declarations: [
    AppComponent,
    TpnFormComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot([])
  ],
  providers: [
    AppInitService,
    TpnCalculationService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AppInitService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }