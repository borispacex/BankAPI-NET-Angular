import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Empleado } from 'src/app/interfaces/empleado';
import { DialogAddEditComponent } from '../dialog-add-edit/dialog-add-edit.component';

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrls: ['./dialog-delete.component.css']
})
export class DialogDeleteComponent implements OnInit {
  
    constructor(
      private dialogoReferencia: MatDialogRef<DialogAddEditComponent>,
      @Inject(MAT_DIALOG_DATA) public dataEmpleado: Empleado
    ) { }

    ngOnInit(): void {

    }

    confirmarEliminar() {
      if (this.dataEmpleado) {
        this.dialogoReferencia.close('eliminar');
      }
    }
  
}
