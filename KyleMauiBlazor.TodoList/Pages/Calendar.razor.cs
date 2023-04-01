using System;
using KyleMauiBlazor.TodoList.Data;

namespace KyleMauiBlazor.TodoList.Pages
{
    public partial class Calendar
    {

        private DateOnly? _selectedDate;
        private DateOnly[] _arrayEvents;
        private IEnumerable<TodoItem> _thisList = Enumerable.Empty<TodoItem>();

        private IEnumerable<TodoItem> _dataList;


        protected override async Task OnInitializedAsync()
        {
            _dataList = await todoItemService.GetTestAll();
            _thisList = _dataList.Where(x => x.Deadline == DateOnly.FromDateTime(DateTime.Now));
            await base.OnInitializedAsync();

        }

        //private IEnumerable<TodoItem> InitDate()
        //{
        //    return Task.Run(async () => await todoItemService.GetTestAll()).Result;
        //}

        private void SelectedDateChanged(DateOnly? date)
        {
            if (date.HasValue)
                //_thisList = _dataList.Where(x => DateOnly.FromDateTime(x.Deadline.Value) == date.Value);
                _thisList = _dataList.Where(x => x.Deadline == date.Value);
            else
                _thisList = _dataList;
        }

        public DateOnly? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SelectedDateChanged(_selectedDate);
            }
        }

        //public DateOnly[] ArrayEvents
        //{
        //    get { return _arrayEvents; }
        //    set
        //    {
        //        _arrayEvents = _dataList.Where(x => x.Deadline.HasValue)
        //            .GroupBy(x => x.Deadline.Value)
        //            .Select(x => x.Key).ToArray();
        //    }
        //}
    }
}

