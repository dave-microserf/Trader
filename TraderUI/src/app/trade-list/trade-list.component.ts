import { Component, OnInit } from '@angular/core';
import { ITrade } from '../shared/trade';
import { TradeService } from '../shared/trade.service';
import { CounterpartyService } from '../shared/counterparty.service';
import { Router } from '@angular/router';
import { ICounterparty } from '../shared/counterparty';

@Component({
  selector: 'app-trade-list',
  templateUrl: './trade-list.component.html',
  styleUrls: ['./trade-list.component.css']
})
export class TradeListComponent implements OnInit {
  public componentName: string = "Trade List";
  
  public trades: ITrade[] = [];
  public counterparty: ICounterparty = null;
  public counterparties: ICounterparty[] = [];
  
  constructor(private tradeService: TradeService, private counterpartyService: CounterpartyService, private router: Router) { }

  ngOnInit() {
    this.counterpartyService.getCounterparties().subscribe({
      next: counterparties => this.counterparties = counterparties,
      error: error => console.error(error)
    })    
  }

  searchByCounterparty(): void {
    console.info(this.counterparty);
    
    this.tradeService.getTradesForCounterparty(this.counterparty.counterpartyId).subscribe({
      next: trades => this.trades = trades,
      error: error => console.error(error)
    });
  }

  createNewTrade(): void {
    this.router.navigate(['trade', -1]);
  }
}