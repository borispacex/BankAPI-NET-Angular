import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { endPoint } from '../global';
import { Empleado } from '../interfaces/empleado';

@Injectable({
  providedIn: 'root'
})
export class EmpleadoService {

  private apiUrl: string = endPoint + 'empleado/';

  constructor(private http: HttpClient) { }

  getList(): Observable<Empleado[]>{
    return this.http.get<Empleado[]>(`${this.apiUrl}lista`);  
  }

  add(modelo: Empleado): Observable<Empleado>{
    return this.http.post<Empleado>(`${this.apiUrl}guardar`, modelo);
  }

  update(idEmpleado: number, modelo: Empleado): Observable<Empleado>{
    return this.http.put<Empleado>(`${this.apiUrl}actualizar/${idEmpleado}`, modelo);
  }

  delete(idEmpleado: number): Observable<Empleado>{
    return this.http.delete<Empleado>(`${this.apiUrl}eliminar/${idEmpleado}`);
  }
}
