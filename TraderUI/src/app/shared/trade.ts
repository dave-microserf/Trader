// Rename to CounterpartyTrade
export interface ITrade
{
    counterpartyId: number,
    counterpartyName: string,
    tradeId: number,
    product: string,
    quantity: number,
    price: number,
    date: Date,
    direction: string
}