using System;
using System.ComponentModel;
using KyleMauiBlazor.TodoList.Commons;
using SQLite;

namespace KyleMauiBlazor.TodoList.Data
{
    public class TodoItem
    {
        public TodoItem()
        {
            //Deadline = DateTime.Now.Date;
        }



        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Content { get; set; }
        ///// <summary>
        ///// 提醒日期 
        ///// </summary>
        //public DateTime? RemindDate { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public TimeOnly? Time { get; set; }

        /// <summary>
        /// 提醒时间 HH:mm
        /// </summary>
        public TimeOnly? RemindTime { get; set; }
        //public bool ShowBell
        //{
        //    get
        //    {
        //        return !string.IsNullOrWhiteSpace(RemindTime);
        //    }
        //}
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateOnly? Deadline { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeadlineDescript
        {
            get
            {
                var now = DateOnly.FromDateTime(DateTime.Now.Date);
                //var now = DateTime.Now.Date;
                if (!Deadline.HasValue ||
                    (Deadline.HasValue && Deadline.Value > now)) return "未来";


                if (Deadline.Value == now) return "今天";

                return "以前";
            }
        }

        /// <summary>
        /// 开启提醒
        /// </summary>
        public bool EnableRemind { get; set; }
        /// <summary>
        /// 重复 0:不重复 1:小时 2:日 3:周 4:月 5:年
        /// </summary>
        public Repeat Repeat { get; set; }
        /// <summary>
        /// 已完成
        /// </summary>
        public bool Finished { get; set; }

        public IEnumerable<TodoItem> ChildTask { get; set; } = new List<TodoItem>();

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment { get; set; }
        /// <summary>
        /// 是否标记
        /// </summary>
        public bool IsMark { get; set; }


        public void Marked()
        {
            this.IsMark = true;
        }


    }
}

public enum Repeat
{
    [Description("无")]
    None,

    [Description("每日")]
    Hour,

    [Description("每日")]
    Day,

    [Description("每周")]
    Week,

    [Description("月度")]
    Month,

    [Description("年度")]
    Year
}

public class SelectData
{
    public SelectData()
    {
    }

    public SelectData(Repeat value)
    {
        Value = value;

        Label = value.GetDescription();
    }

    public string Label { get; set; } = default;

    public Repeat Value { get; set; } = Repeat.None;
}

