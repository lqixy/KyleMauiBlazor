using System;
using KyleMauiBlazor.TodoList.Data;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace KyleMauiBlazor.TodoList.Pages
{
    public partial class Index
    {
        private bool _hasMore = true;
        private MList _card;
        private int _count = 0;

        private const int takeCount = 20;

        private string _kekyword;

        public string Keyword
        {
            get => _kekyword;
            set
            {
                _kekyword = value;
                KeywordChanged(_kekyword);
            }
        }

        [Inject]
        public NavigationManager Nav { get; set; } = default;

        private void KeywordChanged(string keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                _todoList = _dataSources
                    .Where(x => x.Deadline < today && x.Content.Contains(keyword))
                    .ToList();
            }
            else
            {
                _todoList = _dataSources.Take(takeCount).ToList();
            }
        }

        private DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        private IEnumerable<Category> _thisCategories;

        private List<TodoItem> _todoList = new();
        private List<TodoItem> _dataSources = new();

        private IEnumerable<Category> _categories;

        protected override async Task OnInitializedAsync()
        {
            _thisCategories = await categoryService.GetTest();
            _dataSources = (await todoItemService.GetTestAll()).ToList();

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await TodoListLoadMore();
                StateHasChanged();
            }
        }


        private async Task TodoListLoadMore()
        {
            var append = new List<TodoItem>();
            var total = 0;
            if (!string.IsNullOrWhiteSpace(_kekyword))
            {
                append = _dataSources.Where(x => x.Content.Contains(_kekyword))
                    .Skip(_count)
                  .Take(takeCount).ToList();
                total = _dataSources.Count(x => x.Content.Contains(_kekyword));
            }
            else
            {
                append = _dataSources.Skip(_count)
                  .Take(takeCount).ToList();
                total = _dataSources.Count();
            }
            _todoList.AddRange(append);
            _hasMore = _todoList.Count < total;
            _count += takeCount;
        }


        private void NavigateToDetails(int id)
        {
            Nav.NavigateTo($"/details/{id}");
        }


    }
}

