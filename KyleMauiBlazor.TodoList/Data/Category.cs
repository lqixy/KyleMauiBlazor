using System;
using SQLite;

namespace KyleMauiBlazor.TodoList.Data
{
    public class Category
    {
        public Category()
        {
        }

        public Category(int id, string title)
        {
            Id = id;
            Title = title;
            Sort = 1;
            Enable = true;
            CreationTime = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Enable { get; set; }

        public int Sort { get; set; }

        public int HasTaskCount { get; set; }

        public DateTime CreationTime { get; set; }

    }
}

