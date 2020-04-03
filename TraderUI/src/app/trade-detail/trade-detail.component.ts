import { Component, OnInit } from '@angular/core';
import { TradeService } from '../shared/trade.service';
import { CounterpartyService } from '../shared/counterparty.service';
import { ITrade } from '../shared/trade';
import { ActivatedRoute } from '@angular/router'
import { ICounterparty } from '../shared/counterparty';

@Component({
  templateUrl: './trade-detail.component.html',
  styleUrls: ['./trade-detail.component.css']
})
export class TradeDetailComponent implements OnInit {
  public componentName: string = "Trade Detail";

  public counterparties: ICounterparty[] = [];
  public trade: ITrade;
  
  constructor(
    private route: ActivatedRoute, 
    private tradeService: TradeService, 
    private counterpartyService: CounterpartyService) {
  }

  ngOnInit() {
    let identifier = this.route.snapshot.paramMap.get('id');

    this.counterpartyService.getCounterparties().subscribe({
      next: counterparties => this.counterparties = counterparties,
      error: error => console.error(error)
    });
    
    if (identifier == "-1")
    {
      this.trade = { 
        counterpartyId: 0,
        counterpartyName: "", 
        product: "", 
        quantity: 0,
        tradeId: null, 
        price: 0, 
        direction: "Buy", 
        date: new Date()
      };
    } else {
      this.tradeService.getTrade(Number(identifier)).subscribe({
        next: trade => this.trade = trade,
        error: error => console.error(error)
      });
    }
  }

  submit(): void {
    this.tradeService.submitTrade(this.trade).subscribe();
  }
}