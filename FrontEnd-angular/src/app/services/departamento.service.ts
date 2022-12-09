import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { endPoint } from '../global';
import { Departamento } from '../interfaces/departamento';

@Injectable({
  providedIn: 'root'
})
export class DepartamentoService {

  private apiUrl: string = endPoint + 'departamento/';

  constructor(private http: HttpClient) { }

  getList(): Observable<Departamento[]>{
    return this.http.get<Departamento[]>(this.apiUrl + 'lista');  
  }
}
