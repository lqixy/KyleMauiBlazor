using System;
using KyleMauiBlazor.TodoList.Data;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace KyleMauiBlazor.TodoList.Pages
{
    public partial class TodoItemDetail
    {
        [Parameter]
        public int Id { get; set; }

        private bool _deadlineMenu;
        private bool _timeMenu;
        private bool _remindTimeMenu;
        private bool _remarkMenu;

        [Inject]
        public NavigationManager Nav { get; set; } = default;

        private MForm? _mForm;
        private List<SelectData> _selectDatas => todoItemService.GetRepeatList();

        public TodoItem TodoItem { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TodoItem = await todoItemService.GetTestById(Id);
        }

        private void NavigateToIndex()
        {
            Nav.NavigateTo("");
        }
    }
}

