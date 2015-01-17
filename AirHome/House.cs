using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// 更新住所名称
        /// </summary>
        /// <param name="name">住所名称</param>
        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}