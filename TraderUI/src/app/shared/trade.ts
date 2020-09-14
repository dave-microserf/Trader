import { ICounterparty } from './counterparty';

// Rename to CounterpartyTrade
export interface ITrade
{
    tradeId: number,
    date: Date,
    counterpartyId: number,
    product: string,
    quantity: number,
    price: number,
    direction: string
    counterparty: ICounterparty
}