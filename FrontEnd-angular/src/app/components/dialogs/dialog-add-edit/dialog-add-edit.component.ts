import { EmpleadoService } from 'src/app/services/empleado.service';
import { DepartamentoService } from './../../../services/departamento.service';
import { Component, OnInit, Inject } from '@angular/core';

import { MAT_DATE_FORMATS } from '@angular/material/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Departamento } from 'src/app/interfaces/departamento';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Empleado } from 'src/app/interfaces/empleado';
import * as moment from 'moment';

export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY'
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  }
};

@Component({
  selector: 'app-dialog-add-edit',
  templateUrl: './dialog-add-edit.component.html',
  styleUrls: ['./dialog-add-edit.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }
  ]
})
export class DialogAddEditComponent implements OnInit {

    formEmpleado: FormGroup;
    tituloAccion: string = 'Nuevo';
    botonAccion: string  = "Guardar"
    listaDepartamentos: Departamento[] = [];
  
    constructor(
      private dialogoReferencia: MatDialogRef<DialogAddEditComponent>,
      private fb: FormBuilder,
      private _snackBar: MatSnackBar,
      private _empleadoService: EmpleadoService,
      private _departamentoService: DepartamentoService,
      @Inject(MAT_DIALOG_DATA) public dataEmpleado: Empleado
    ) {
      this.formEmpleado = this.fb.group({
        nombreCompleto: ['', Validators.required],
        idDepartamento: ['', Validators.required],
        sueldo: ['', Validators.required],
        fechaContrato: ['', Validators.required]
      });

      this.mostrarDepartamentos();
    }

    ngOnInit(): void {
      if (this.dataEmpleado) {
        this.formEmpleado.patchValue({
          nombreCompleto: this.dataEmpleado.nombreCompleto,
          idDepartamento: this.dataEmpleado.idDepartamento,
          sueldo: this.dataEmpleado.sueldo,
          fechaContrato: moment(this.dataEmpleado.fechaContrato, 'DD/MM/YYYY').toDate()
        });
        this.tituloAccion = 'Editar';
        this.botonAccion = 'Actualizar';
      }
    }

    mostrarDepartamentos() {
      this._departamentoService.getList().subscribe({
        next: (data) => {
          this.listaDepartamentos = data;
        }, error: (e) => {
          console.log('Error: ', e);
        }
      });
    }

    mostrarAlerta(mensaje: string, accion: string) {
      this._snackBar.open(mensaje, accion, {
        horizontalPosition: 'end',
        verticalPosition: 'top',
        duration: 3000
      });
    }

    addEditEmpleado() {
      // console.log('valor formEmpleado', this.formEmpleado.value);
      const modelo: Empleado = {
        idEmpleado: 0,
        nombreCompleto: this.formEmpleado.value.nombreCompleto,
        idDepartamento: this.formEmpleado.value.idDepartamento,
        sueldo: this.formEmpleado.value.sueldo,
        fechaContrato: moment(this.formEmpleado.value.fechaContrato).format('DD/MM/YYYY')
      };
      if (this.dataEmpleado == null) {
        this._empleadoService.add(modelo).subscribe({
          next: (data) => {
            this.mostrarAlerta('Empleado guardado correctamente', 'Listo');
            this.dialogoReferencia.close('creado');
          }, error: (e) => {
            this.mostrarAlerta('No se pudo crear Empleado', 'Error');
          }
        });
      } else {
        this._empleadoService.update(this.dataEmpleado.idEmpleado, modelo).subscribe({
          next: (data) => {
            this.mostrarAlerta('Empleado editado correctamente', 'Listo');
            this.dialogoReferencia.close('editado');
          }, error: (e) => {
            this.mostrarAlerta('No se pudo editar Empleado', 'Error');
          }
        });
      }
      
    }
  
}
