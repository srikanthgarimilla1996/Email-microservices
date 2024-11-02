import { Component, OnInit } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-quartz.css';
import { ColDef } from 'ag-grid-community';
import { AppService } from '../../app.service';

@Component({
  selector: 'app-view-logs',
  standalone: true,
  imports: [AgGridAngular],
  templateUrl: './view-logs.component.html',
  styleUrl: './view-logs.component.scss'
})
export class ViewLogsComponent implements OnInit{
  rowData:any;
  colDefs:ColDef[]=[
    {
      field:'message',
      headerName: 'Message',
      flex:1
    },
    {
      field:'users',
      headerName: 'Users',
      flex:1
    },
    {
      field:'logLevel',
      headerName: 'Level',
      flex:1
    },
    {
      field:'dateTime',
      headerName: 'Date & Time',
      flex:1
    },
    {
      field:'exception',
      headerName: 'Exception',
      flex:1
    }
  ];


  constructor(private appService:AppService) {

  }
  ngOnInit(): void {
    this.getLogs();
  }

  getLogs() {
    this.appService.getDatabaseLogs().subscribe((res:any)=>{
      this.rowData = res;
    });
  }
}
