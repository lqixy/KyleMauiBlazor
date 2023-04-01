using System;
using System.Text;
using KyleMauiBlazor.TodoList.DbContext;
using Newtonsoft.Json;

namespace KyleMauiBlazor.TodoList.Data
{
    public class TodoItemService
    {
        private readonly TodoItemDatabase database;

        private List<TodoItem> Datas;

        public TodoItemService(TodoItemDatabase database)
        {
            this.database = database;

            Datas = TodoItemData.Get();
        }

        #region TestData
        public Random random;

        public async Task<int> Count(Func<TodoItem, bool> pre = null)
        {
            return Datas.Count(pre);
        }

        public async Task<TodoItem[]> GetTestAll(Func<TodoItem, bool> pre = null)
        {
            var list = new List<TodoItem>();
            if (pre is not null)
                Datas.Where(pre).ToList();
            else
                list = Datas;
            return Datas.OrderBy(x => x.Deadline).ToArray();
        }
        public async Task<TodoItem[]> GetTest(int skipCount = 0, int takeCount = 5, Func<TodoItem, bool> pre = null)
        {
            var list = new List<TodoItem>();
            if (pre is not null)
                list = Datas.Where(pre).ToList();
            else
            {
                list = Datas;
            }

            return await Task.FromResult<TodoItem[]>(list.OrderBy(x => x.Deadline).Skip(skipCount).Take(takeCount).ToArray());
        }

        public async Task<TodoItem> GetTestById(int id)
        {
            var entity = Datas.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult<TodoItem>(entity);
        }

        #endregion

        public async Task<IEnumerable<TodoItem>> Get()
        {
            return await database.GetAll();
        }

        public async Task<TodoItem> GetById(int id)
        {
            return await database.Find(x => x.Id == id);
        }

        public async Task<int> Save(TodoItem item)
        {
            return await database.Save(item);
        }

        public async Task<int> Delete(TodoItem item)
        {
            return await database.Delete(item);
        }


        public List<SelectData> GetRepeatList()
        {
            return new List<SelectData>
            {
                new SelectData( Repeat.None),
                new SelectData(Repeat.Hour),
                new SelectData(Repeat.Day),
                new SelectData( Repeat.Week),
                new SelectData( Repeat.Month),
                new SelectData( Repeat.Year)
            };
        }

    }


    public static class TodoItemData
    {
        public static List<TodoItem> Get()
        {
            var Datas = Enumerable.Range(1, 2)
            .Select(x =>
            {
                var random = new Random();

                var repeats = Enum.GetValues(typeof(Repeat)) as Repeat[];

                return new TodoItem()
                {
                    Id = x,
                    Content = Helper.RandChina(random.Next(5, 20)),// $"列表可以包含一组项目。",
                    Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(-2, 30))),
                    Time = TimeOnly.FromDateTime(DateTime.Now.AddHours(random.Next(-4, 12))),
                    RemindTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(random.Next(1, 12))),
                    EnableRemind = true,
                    Repeat = repeats[random.Next(0, repeats.Length)]
                };

            }).ToList();


            return Datas;
        }


    }

    public class Helper
    {
        /// <summary>
        /// 此函数为生成指定数目的汉字
        /// </summary>
        /// <param name="charLen">汉字数目</param>
        /// <returns>所有汉字</returns>
        public static string RandChina(int charLen)
        {
            int area, code;//汉字由区位和码位组成(都为0-94,其中区位16-55为一级汉字区,56-87为二级汉字区,1-9为特殊字符区)
            StringBuilder strtem = new StringBuilder();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Random rand = new Random();
            for (int i = 0; i < charLen; i++)
            {
                area = rand.Next(16, 88);
                if (area == 55)//第55区只有89个字符
                {
                    code = rand.Next(1, 90);
                }
                else
                {
                    code = rand.Next(1, 94);
                }
                strtem.Append(Encoding.GetEncoding("GB2312").GetString(new byte[] { Convert.ToByte(area + 160), Convert.ToByte(code + 160) }));
            }
            return strtem.ToString();
        }
    }


}

