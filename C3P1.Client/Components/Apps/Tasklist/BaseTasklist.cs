using Blazorise;
using C3P1.Client.Services.Apps;
using Microsoft.AspNetCore.Components;

namespace C3P1.Client.Components.Apps.Tasklist
{
    public abstract class BaseTasklist : ComponentBase
    {
        protected Filter filter = Filter.Todo;
        protected Validations? validations;
        protected string? title;
        protected Guid currentUserId;

        protected List<TodoItem>? tasklist;
        protected IEnumerable<TodoItem> Tasklist
        {
            get
            {
                if (tasklist != null)
                {
                    var query = from t in tasklist select t;

                    if (filter == Filter.Todo)
                    {
                        query = from q in query where !q.Completed select q;
                    }

                    if (filter == Filter.Done)
                    {
                        query = from q in query where q.Completed select q;
                    }

                    return query;
                }
                else // return blank list during tasklist loading
                {
                    return new List<TodoItem>();
                }
            }
        }

        [Inject]
        ITasklistService? tasklistService { get; set; }
        protected void SetFilter(Filter filter)
        {
            this.filter = filter;
        }

        protected async void AddTask()
        {
            if (await validations!.ValidateAll())
            {
                var newTask = new TodoItem()
                {
                    Completed = false,
                    Title = title ?? string.Empty,
                };

                await tasklistService!.AddTaskAsync(currentUserId, newTask);
                await LoadData();
                title = null;

                await validations.ClearAll();

                await InvokeAsync(StateHasChanged);
            }
        }

        protected async Task DeleteDoneTasks()
        {
            await tasklistService!.DeleteTasklistDoneAsync(currentUserId);
            await LoadData();

            await InvokeAsync(StateHasChanged);
        }
        protected async Task MarkAllAsDone()
        {
            await tasklistService!.MarkTasklistAsDoneAsync(currentUserId);
            await LoadData();

            await InvokeAsync(StateHasChanged);
        }
        protected async Task LoadData()
        {
            // load tasklist
            tasklist = await tasklistService!.GetTasklistAsync(currentUserId);
        }

        protected Task OnTodoStatusChanged(bool isChecked)
        {
            return InvokeAsync(StateHasChanged);
        }
    }
}
