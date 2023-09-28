import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from '../_models/task';


@Injectable({
  providedIn: 'root'
})
export class TaskService {
  baseUrl = 'https://localhost:7238/api/';
  tasks: Task[] = [];

 constructor(private http: HttpClient) { }

  getTasks(username: string){
    return this.http.get<Task[]>(this.baseUrl + 'task/' + username).subscribe({
      next: response => this.tasks = response,
      error: error => console.log(error),
      complete: () => console.log('Get User has Completed')
    })
  }
}
