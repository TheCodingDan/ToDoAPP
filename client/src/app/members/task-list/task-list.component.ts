import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { TaskService } from 'src/app/_services/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  constructor(private accountService: AccountService, public taskService: TaskService){}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe((user) => {
      if(user){
        this.getTasks(user.username)
      }
    })
  }

  getTasks(username: string){
    this.taskService.getTasks(username);
  }
}
