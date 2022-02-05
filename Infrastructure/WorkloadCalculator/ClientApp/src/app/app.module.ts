import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { WorkloadCalculatorComponent } from './workload-calculator/workload-calculator.component';
import { WorkloadSummaryComponent } from './workload-summary/workload-summary.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
const routes: Routes = [
  { path: '', component: WorkloadCalculatorComponent},
  { path: 'workload-summary', component: WorkloadSummaryComponent }
];
@NgModule({
  providers: [{ provide: LocationStrategy, useClass: HashLocationStrategy }],
  declarations: [
    AppComponent,
    NavMenuComponent,
    WorkloadCalculatorComponent,
    WorkloadSummaryComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    //RouterModule.forRoot([
    //  { path: 'workload', component: WorkloadCalculatorComponent },// pathMatch: 'full' },
    //  { path: 'workload-summary', component: WorkloadSummaryComponent }
    //]),
    RouterModule.forRoot(routes, { useHash: true })
  ],

  //imports: [RouterModule.forRoot(routes)],
  //exports: [RouterModule],

  //providers: [],
  bootstrap: [AppComponent]

})
export class AppModule { }
