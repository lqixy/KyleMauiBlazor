using KyleMauiBlazor.TodoList.Data;
using SQLite;
using System;
using System.Linq.Expressions;

namespace KyleMauiBlazor.TodoList.DbContext
{
    public class CategoryDatabase
    {
        public CategoryDatabase()
        {
        }

        SQLiteAsyncConnection Connection;

        async Task Init()
        {
            if (Connection is not null) return;
            Connection = new SQLiteAsyncConnection(Constants.DatabasePath,
                Constants.Flags);
            var result = Connection.CreateTableAsync<Category>();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            await Init();
            return await Connection.Table<Category>()
                .ToListAsync()
                ;
        }

        public async Task<Category> Find(Expression<Func<Category,bool>> pression)
        {
            await Init();
            return await Connection.Table<Category>()
                .FirstOrDefaultAsync(pression);
        }

        public async Task<int> Save(Category entity)
        {
            await Init();
            if(entity.Id != 0)
            {
                return await Connection.UpdateAsync(entity);
            }

            return await Connection.InsertAsync(entity);
        }

        public async Task<int> Delete(Category entity)
        {
            await Init();
            return await Connection.DeleteAsync(entity);
        }

    }


}

