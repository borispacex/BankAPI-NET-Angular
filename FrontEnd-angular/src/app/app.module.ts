import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// 1.- Para trabajar con reactive Forms
import { ReactiveFormsModule } from '@angular/forms';
// 2.- para trabajar con peticiones http
import { HttpClientModule } from '@angular/common/http';
// 3.- Para trabajar con Tablas de material
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
// 4.- Para trabajar con controles de formularios de materia
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
// 5.- Para trabajar con mensajes de alertas
import { MatSnackBarModule } from '@angular/material/snack-bar';
// 6.- Para trabajar con iconos de material
import { MatIconModule } from '@angular/material/icon';
// 7.- Para trabajar con modales de amterial
import { MatDialogModule } from '@angular/material/dialog';
// 8.- Para trabajar con cuadriculas
import { MatGridListModule } from '@angular/material/grid-list';
import { HomeComponent } from './components/home/home.component';
import { DialogAddEditComponent } from './components/dialogs/dialog-add-edit/dialog-add-edit.component';
import { DialogDeleteComponent } from './components/dialogs/dialog-delete/dialog-delete.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DialogAddEditComponent,
    DialogDeleteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatSnackBarModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatGridListModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
