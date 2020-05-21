import { Pizza } from './pizza';

export class Order {
    public id: number;
    public accountId: number;
    public name: string;
    public surname: string;
    public phone: string;
    public address: string;
    public total: number;
    public pizzas: Pizza[];
}
