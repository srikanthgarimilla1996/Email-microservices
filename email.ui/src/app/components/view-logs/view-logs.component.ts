import { Component, OnInit } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-quartz.css';
import { ColDef } from 'ag-grid-community';
import { AppService } from '../../app.service';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-view-logs',
  standalone: true,
  imports: [AgGridAngular],
  templateUrl: './view-logs.component.html',
  styleUrl: './view-logs.component.scss'
})
export class ViewLogsComponent implements OnInit{
  rowData:any;
  private hubConnection!:signalR.HubConnection;
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
    this.establishSignalRConnection();
  }

  getLogs() {
    this.appService.getDatabaseLogs().subscribe((res:any)=>{
      this.rowData = res;
    });
  }

  establishSignalRConnection() {
    //Establish signalR Connection
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7245/logsHub').build();

    // Start the connection and handle new data
    this.hubConnection.start().then(()=>{
      console.log('signalR Connection established');

      // Listen for receive event
      this.hubConnection.on('RecieveLog',(log:any)=>{
        console.log('New log Received:',log);

        //Update grid data with new log
        //this.rowData = [log, ...this.rowData];
        this.getLogs();
      })
    }).catch(err => console.error('Error in establishing signalR Connection:',err));
  }
}
