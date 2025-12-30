import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedbackFormComponent } from './feedback-form/feedback-form.component';
import { TpnFormComponent } from './tpn-form/tpn-form.component';

const routes: Routes = [
  { path: 'tpn-form', component: TpnFormComponent }, 
  { path: 'feedback', component: FeedbackFormComponent },
  { path: '**', redirectTo: 'tpn-form', pathMatch: 'full' } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
