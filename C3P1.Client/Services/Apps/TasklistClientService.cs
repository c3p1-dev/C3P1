﻿using C3P1.Client.Components.Apps.Tasklist;
using System.Net.Http.Json;

namespace C3P1.Client.Services.Apps
{
    public class TasklistClientService : ITasklistService
    {
        private readonly HttpClient _httpClient;
        public TasklistClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TodoItem>> GetTasklistAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/apps/tasklist");

            return result!;
        }
        public async Task<List<TodoItem>> GetTasklistTodoAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/apps/tasklist/todo");

            return result!;
        }
        public async Task<List<TodoItem>> GetTasklistDoneAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/apps/tasklist/done");

            return result!;
        }

        public async Task<bool> AddTaskAsync(Guid userId, TodoItem task)
        {
            var result = await _httpClient.PostAsJsonAsync<TodoItem>("api/apps/tasklist", task);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskAsync(Guid userId, Guid taskId)
        {
            var result = await _httpClient.DeleteAsync("api/apps/tasklist/" + taskId.ToString());

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(TodoItem task)
        {
            var result = await _httpClient.PutAsJsonAsync("api/apps/tasklist", task);

            return result.IsSuccessStatusCode;
        }

        public async Task<List<TodoItem>> DeleteTasklistDoneAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/apps/tasklist/done/delete");

            return result!;
        }

        public async Task<List<TodoItem>> MarkTasklistAsDoneAsync(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/apps/tasklist/todo/markasdone");

            return result!;
        }
    }
}
