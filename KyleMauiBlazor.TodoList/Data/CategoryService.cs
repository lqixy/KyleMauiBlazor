using System;
using KyleMauiBlazor.TodoList.DbContext;

namespace KyleMauiBlazor.TodoList.Data
{
    public class CategoryService
    {
        private readonly CategoryDatabase database;

        public CategoryService(CategoryDatabase database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<Category>> GetTest()
        { 
            return new Category[]
            {
                new Category
                {
                    Id = 1,
                    Title = "所有"
                },
                new Category
                {
                    Id = 2,
                    Title = "工作"
                },
                new Category
                {
                    Id = 3,
                    Title = "个人"
                },
                new Category
                {
                    Id = 4,
                    Title = "生日"
                },
                new Category
                {
                    Id = 5,
                    Title = "心愿单"
                },
            }; 
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await database.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await database.Find(x => x.Id == id);
        }

        public async Task<int> Save(Category item)
        {
            return await database.Save(item);
        }

        public async Task<int> Delete(int id)
        {
            var entity = await GetById(id);
            return await database.Delete(entity);
        }


    }
}

