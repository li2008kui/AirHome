using System;

namespace AirHome
{
    public class House
    {
        /// <summary>
        /// 住所名称
        /// </summary>
        private string Name { get; private set; }
        /// <summary>
        /// 是否是主要住所
        /// </summary>
        private bool IsPrimary { get; private set; }

        /// <summary>
        /// 默认构造方法
        ///     <para>住所名称为NULL</para>
        ///     <para>默认为主要住所</para>
        /// </summary>
        public House()
        {
            IsPrimary = true;
        }

        /// <summary>
        /// 初始化住所
        /// </summary>
        /// <param name="name">住所名称</param>
        ///     <para>默认为主要住所</para>
        /// <param name="isPrimary">是否是主要住所</param>
        public House(string name, bool isPrimary = true)
        {
            Name = name;
            IsPrimary = isPrimary;
        }

        /// <summary>
        /// 处理更新住所名称的事件
        /// </summary>
        private event EventHandler UpdateNameCompleted;
        /// <summary>
        /// 处理使住所成为主要住所的事件
        /// </summary>
        private event EventHandler BecomePrimaryCompleted;

        /// <summary>
        /// 引发处理更新住所名称的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnUpdateName(UpdateEventArgs e)
        {
            if (this.UpdateNameCompleted != null)
            {
                this.UpdateNameCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理使住所成为主要住所的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnBecomePrimary(UpdateEventArgs e)
        {
            if (this.BecomePrimaryCompleted != null)
            {
                this.BecomePrimaryCompleted(this, e);
            }
        }

        /// <summary>
        /// 更新住所名称
        /// </summary>
        /// <param name="name">住所名称</param>
        private void UpdateName(string name)
        {
            Exception ex = null;

            if (string.IsNullOrEmpty(name))
            {
                ex = new Exception("住所名称不能为空。");
            }
            else if (Name == name.Trim())
            {
                ex = new Exception("新住所名称与旧住所名称没有变化。");
            }
            else
            {
                Name = name;
                ex = new Exception("住所名称更新成功。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnUpdateName(arg);
        }

        /// <summary>
        /// 使住所成为主要住所
        /// </summary>
        private void BecomePrimary()
        {
            Exception ex = null;
            string name = "[" + (Name ?? "null") + "]";

            if (IsPrimary)
            {
                ex = new Exception(name + "已经是主要住所。");
            }
            else
            {
                IsPrimary = true;
                ex = new Exception("设置" + name + "为主要住所成功。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnBecomePrimary(arg);
        }
    }
}