import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators'
import { ICounterparty } from './counterparty';

@Injectable({providedIn: 'root'})
export class CounterpartyService
{
    private cache$: Observable<ICounterparty[]>;

    constructor(private http: HttpClient) {
    }

    getCounterparties(): Observable<ICounterparty[]>
    {
        if (!this.cache$)
        {
            this.cache$ = this.requestCounterparties().pipe(shareReplay(1));
        }

        return this.cache$;
    }

    private requestCounterparties(): Observable<ICounterparty[]>
    {
        const counterpartiesUrl: string = "http://localhost:58452/api/counterparties/";

        return this.http.get<ICounterparty[]>(counterpartiesUrl);
    }
}