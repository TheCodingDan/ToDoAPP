using API.Domain.DTO;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class TaskController : BaseAPIController
    {
        private readonly TaskService _taskService;
        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateTask(TaskDTO taskDTO){
            return await _taskService.CreateTask(taskDTO);
        }

        [HttpGet("get")]
        public async Task<ActionResult<TaskDTO>> GetTasksById(int id){
            return await _taskService.GetTaskByTaskId(id);
        }

        [HttpPut("update")]
        public async Task<ActionResult<string>> EditTask(TaskDTO taskDTO){
            return await _taskService.EditTask(taskDTO);
        }
        
        [HttpDelete("delete")]
        public async Task<ActionResult<string>> DeleteTaskByTaskId(int id){
            return await _taskService.DeleteTaskById(id);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetUserTasksByUsername(string username){
            return await _taskService.GetUserTasksByUsername(username);
        }

        [HttpGet("{username}/category/{category}")]
        public async Task<ActionResult<List<TaskDTO>>> GetTasksByCategory(string username, string category){
            return await _taskService.GetTaskByCategory(username, category);
        }

        [HttpGet("{username}/priority/{priority}")]
        public async Task<ActionResult<List<TaskDTO>>> GetTasksByPriority(string username, string priority){
            return await _taskService.GetTaskByPriority(username, priority);
        }

        [HttpPut("mark/{id}")]
        public async Task<ActionResult<TaskDTO>> MarkTask(int id){
            return await _taskService.MarkTask(id);
        }
    }
}