import { Routes } from '@angular/router';
import { SendEmailComponent } from './components/send-email/send-email.component';
import { ViewLogsComponent } from './components/view-logs/view-logs.component';

export const routes: Routes = [
    {path:'',component:SendEmailComponent},
    {path:'sendEmail',component:SendEmailComponent},
    {path:'view-logs',component:ViewLogsComponent}
];
