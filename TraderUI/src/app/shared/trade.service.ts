import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITrade } from './trade';

@Injectable({providedIn: 'root'})
export class TradeService
{
    readonly counterpartiesUrl: string = "http://localhost:58452/api/counterparties";
    readonly tradesUrl: string = "http://localhost:58452/api/trades";

    constructor(private http: HttpClient)
    {
    }

    getTradesForCounterparty(counterpartyId: number): Observable<ITrade[]>
    {
        let counterpartyTradesUrl: string = `${this.counterpartiesUrl}/${counterpartyId}/trades`;
        
        return this.http.get<ITrade[]>(counterpartyTradesUrl)
    }

    getTrade(tradeId: number): Observable<ITrade>
    {
        let tradeUrl: string = `${this.tradesUrl}/${tradeId}`;
        console.info(tradeUrl);
        return this.http.get<ITrade>(tradeUrl);
    }

    submitTrade(trade: ITrade): Observable<ITrade>
    {
        var httpHeaders = new HttpHeaders({
            'content-type': 'application/json'
        });
        
        console.info(trade);

        return this.http.put<ITrade>(this.tradesUrl, trade, { headers: httpHeaders });
    }
}