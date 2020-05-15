import { Injectable } from '@angular/core';
import { HttpService } from './HttpService';
import { Observable } from 'rxjs';
import { IngredientDto } from 'src/dto/Pizza/IngredientDto';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class IngredientService {
    private readonly _httpService: HttpService;

    public constructor(httpService: HttpService) {
        this._httpService = httpService;
    }

    public getIngredients(): Observable<IngredientDto[]> {
        return this._httpService.get<IngredientDto[]>('api/Ingredient/GetIngredients');
    }

    public getIngredient(id: number): Observable<IngredientDto> {
        const params: HttpParams = new HttpParams()
        .set('id', id.toString());
        return this._httpService.get<IngredientDto>('api/Ingredient/GetIngredient', params);
    }

    public saveIngredient(ingredient: IngredientDto): Observable<IngredientDto> {
        return this._httpService.post<IngredientDto, IngredientDto>('api/Ingredient/SaveIngredient', ingredient);
    }
}
