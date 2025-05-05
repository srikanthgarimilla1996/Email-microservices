import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn:'root'
})
export class AppService{
    public url = 'http://172.25.25.60:8081/api/' // For deployment change this to the ip v4 address
    // public url = 'http://localhost:8081/api/' // For local running use this
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