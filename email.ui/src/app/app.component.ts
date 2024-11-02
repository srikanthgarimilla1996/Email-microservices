import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DxButtonModule, DxDropDownBoxModule, DxSelectBoxComponent, DxSelectBoxModule, DxTagBoxComponent, DxTagBoxModule, DxTextAreaComponent, DxTextAreaModule, DxToastModule } from 'devextreme-angular';
import { AppService } from './app.service';
import DataSource from 'devextreme/data/data_source';
import ArrayStore from 'devextreme/data/array_store';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, DxButtonModule,DxTagBoxModule,DxTextAreaModule,DxToastModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  @ViewChild('usersBox',{static:false}) selectedUsers!: DxTagBoxComponent;
  @ViewChild('userMessage',{static:false}) message!:DxTextAreaComponent;
  users = new DataSource({
    store: new ArrayStore({
        data: [],
        key: 'id'
    })
  });
  constructor(private appservice:AppService) {
    
  }

  ngOnInit(): void {
      this.getUsers();
  }

  getUsers() {
    this.appservice.getUsersList().subscribe((res:any)=> {
      this.users = new DataSource({
        store:new ArrayStore({
            data: res,
            key: 'id'
        })
      })
    })   
  }

  sendEmail(_e:any) {
    const obj = {
      message:this.message.value,
      users:this.selectedUsers.selectedItems
    };
    console.warn(obj);

    this.appservice.sendEmailToUsers(obj).subscribe({
      next:(res:any)=>{
        notify('Email sent successfully','success',300);
      },
      error:(error:any)=>{
        if(error.status === 200) {
          notify('Email sent successfully','success',300);
        } else {
          notify('Something went wrong','error',300);
        }
        
      }
    })
  }

}
