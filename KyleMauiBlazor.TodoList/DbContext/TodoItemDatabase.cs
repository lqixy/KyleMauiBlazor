using System;
using System.Linq.Expressions;
using KyleMauiBlazor.TodoList.Data;

namespace KyleMauiBlazor.TodoList.DbContext
{
    public class TodoItemDatabase
    {
        SQLite.SQLiteAsyncConnection Connection;
        public TodoItemDatabase()
        {
        }

        async Task Init()
        {
            if (Connection is not null) return;

            Connection = new SQLite.SQLiteAsyncConnection(Constants.DatabasePath,
                Constants.Flags);
            var result = Connection.CreateTableAsync<TodoItem>();
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            await Init();
            return await Connection.Table<TodoItem>()
                .ToListAsync()
                ;
        }

        public async Task<TodoItem> Find(Expression<Func<TodoItem, bool>> pression)
        {
            await Init();
            return await Connection.Table<TodoItem>()
                .FirstOrDefaultAsync(pression);
        }

        public async Task<int> Save(TodoItem entity)
        {
            await Init();
            if (entity.Id != 0)
            {
                return await Connection.UpdateAsync(entity);
            }

            return await Connection.InsertAsync(entity);
        }

        public async Task<int> Delete(TodoItem entity)
        {
            await Init();
            return await Connection.DeleteAsync(entity);
        }

    }
}

