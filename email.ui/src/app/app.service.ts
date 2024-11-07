import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn:'root'
})
export class AppService{
    public url = 'http://localhost:7777/api/'
    constructor(private http:HttpClient){

    }

    getUsersList():Observable<any> {
        return this.http.get<any>(this.url+'Users');
    }

    sendEmailToUsers(obj:any):Observable<any> {
        return this.http.put(this.url+'Users/sendEmail',obj);
    }

    getDatabaseLogs():Observable<any> {
        return this.http.get<any>(this.url+'Logs');
    }
}