import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router'

import { AppComponent } from './app.component';
import { TradeListComponent } from './trade-list/trade-list.component';
import { TradeDetailComponent } from './trade-detail/trade-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    TradeListComponent,
    TradeDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'trades', component: TradeListComponent },
      { path: 'trade/:id', component: TradeDetailComponent },
      { path: '', redirectTo: 'trades', pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }