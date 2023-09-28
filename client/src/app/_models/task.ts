export interface Task{
    id: number;
    userId: number;
    name: string;
    category: string;
    priority: string;
    isCompleted: boolean;
    deadline: Date;
}