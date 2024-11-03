import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn:'root'
})
export class AppService{
    private usersUrl = 'https://localhost:7240/api/Users';
    private logsUrl = 'https://localhost:7245/api/Logs';
    constructor(private http:HttpClient){

    }

    getUsersList():Observable<any> {
        return this.http.get<any>(this.usersUrl);
    }

    sendEmailToUsers(obj:any):Observable<any> {
        return this.http.put(this.usersUrl+'/sendEmail',obj);
    }

    getDatabaseLogs():Observable<any> {
        return this.http.get<any>(this.logsUrl);
    }
}