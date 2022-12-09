import { DialogDeleteComponent } from './../dialogs/dialog-delete/dialog-delete.component';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Empleado } from 'src/app/interfaces/empleado';
import { EmpleadoService } from 'src/app/services/empleado.service';
import { DialogAddEditComponent } from '../dialogs/dialog-add-edit/dialog-add-edit.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements AfterViewInit, OnInit {

  displayedColumns: string[] = ['NombreCompleto', 'Departamento', 'Sueldo', 'FechaContrato', 'Acciones'];
  dataSource = new MatTableDataSource<Empleado>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _empleadoService: EmpleadoService,
    public dialog: MatDialog,
    private _snackBar: MatSnackBar
    ) {

  }

  ngOnInit(): void {
    this.mostrarEmpleados();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  mostrarEmpleados(){
    this._empleadoService.getList().subscribe({
      next: (data) => {
        this.dataSource.data = data;
      }, error: (error) => {
        console.log('Error: ' + error);
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  dialogoNuevoEmpleado() {
    this.dialog.open(DialogAddEditComponent, {
      disableClose: true,
      width: '350px',
    }).afterClosed().subscribe(resultado => {
      if (resultado == 'creado') {
        this.mostrarEmpleados();
      }
    });
  }

  dialogoEditarEmpleado(dataEmpleado: Empleado) {
    this.dialog.open(DialogAddEditComponent, {
      disableClose: true,
      width: '350px',
      data: dataEmpleado
    }).afterClosed().subscribe(resultado => {
      if (resultado == 'editado') {
        this.mostrarEmpleados();
      }
    });
  }

  dialogoEliminarEmpleado(dataEmpleado: Empleado) {
    this.dialog.open(DialogDeleteComponent, {
      disableClose: true,
      width: '350px',
      data: dataEmpleado
    }).afterClosed().subscribe(resultado => {
      if (resultado == 'eliminar') {
        this._empleadoService.delete(dataEmpleado.idEmpleado).subscribe({
          next: (data) => {
            this.mostrarAlerta('Empleado eliminado correctamente', 'Listo');
            this.mostrarEmpleados();
          }, error: (error) => { 
            console.log('Error: ' + error);
          }
        });
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

}
