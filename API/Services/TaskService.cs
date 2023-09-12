using API.Controllers;
using API.Data;
using API.Domain.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TaskService : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TaskService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ActionResult<string>> CreateTask(TaskDTO taskDTO){
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == taskDTO.UserId);
            var task = new Domain.Entities.Task
            {
                Name = taskDTO.Name,
                User = user,
                UserId = user.Id,
                Category = taskDTO.Category.ToLower(),
                Priority = taskDTO.Priority.ToLower(),
                Deadline = taskDTO.Deadline,
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return "Task Created";
        }

        public async Task<ActionResult<string>> EditTask(TaskDTO taskDTO){
            var task = await _context.Tasks.FindAsync(taskDTO.Id);

            task.Name = taskDTO.Name;
            task.Category = taskDTO.Category.ToLower();
            task.Priority = taskDTO.Priority.ToLower();
            task.Deadline = taskDTO.Deadline;
            task.IsCompleted = taskDTO.IsCompleted;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return "Task Updated!";

        }

        public async Task<ActionResult<TaskDTO>> GetTaskByTaskId(int id){
            var task = await _context.Tasks.FindAsync(id);

            var taskToReturn = new TaskDTO{
                Name = task.Name,
                Category = task.Category,
                Priority = task.Priority,
                Deadline = task.Deadline,
                IsCompleted = task.IsCompleted,
                UserId = task.UserId,
                Id = id
                
            };

            return taskToReturn;

        }

        public async Task<ActionResult<string>> DeleteTaskById(int id){
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return "Task Deleted!";
        }

        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetUserTasksByUsername(string username){
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null){
                return null;
            }

            var tasks = await _context.Tasks
                .Where(x => x.User.Username == username)
                .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return tasks;
        }

        public async Task<ActionResult<List<TaskDTO>>> GetTaskByCategory(string username ,string category){
        
            var tasks = await _context.Tasks
                .Where(x => x.User.Username == username)
                .Where(x => x.Category == category.ToLower())
                .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return tasks;
        }

        public async Task<ActionResult<List<TaskDTO>>> GetTaskByPriority(string username ,string priority){
            
            var tasks = await _context.Tasks
                .Where(x => x.User.Username == username)
                .Where(x => x.Priority == priority.ToLower())
                .ProjectTo<TaskDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return tasks;
        }

        public async Task<ActionResult<TaskDTO>> MarkTask(int id){
            var task = await _context.Tasks.FindAsync(id);

            if(task == null){
                return BadRequest("task does not exist");
            }

            task.IsCompleted = !task.IsCompleted;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return new TaskDTO{
                Id = task.Id,
                Name = task.Name,
                Category = task.Category,
                Priority = task.Priority,
                Deadline = task.Deadline,
                UserId = task.UserId,
                IsCompleted = task.IsCompleted
            };
        }

    }
}