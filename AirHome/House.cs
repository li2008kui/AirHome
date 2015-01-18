using System;
using System.Collections.Generic;

namespace AirHome
{
    /// <summary>
    /// 用户住所主类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 住所名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 是否是主要住所
        /// </summary>
        public bool IsPrimary { get; private set; }

        /// <summary>
        /// 默认构造方法
        ///     <para>住所名称为NULL</para>
        ///     <para>默认为主要住所</para>
        /// </summary>
        public House()
        {
            IsPrimary = true;
            devices = new List<Device>();
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
            devices = new List<Device>();
        }

        /// <summary>
        /// 处理更新住所名称的事件
        /// </summary>
        public event EventHandler UpdateNameCompleted;
        /// <summary>
        /// 处理使住所成为主要住所的事件
        /// </summary>
        public event EventHandler BecomePrimaryCompleted;

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
        public void UpdateName(string name)
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
        public void BecomePrimary()
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

    /// <summary>
    /// 用户住所设备分部类
    /// </summary>
    public partial class House
    {
        /// <summary>
        /// 设备对象列表
        /// </summary>
        public List<Device> devices { get; private set; }

        /// <summary>
        /// 处理添加设备的事件
        /// </summary>
        public event EventHandler AddDeviceCompleted;

        /// <summary>
        /// 处理移除设备的事件
        /// </summary>
        public event EventHandler RemoveDeviceCompleted;

        /// <summary>
        /// 引发处理添加设备的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnAddDevice(UpdateEventArgs e)
        {
            if (this.AddDeviceCompleted != null)
            {
                this.AddDeviceCompleted(this, e);
            }
        }

        /// <summary>
        /// 引发处理移除设备的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        private void OnRemoveDevice(UpdateEventArgs e)
        {
            if (this.RemoveDeviceCompleted != null)
            {
                this.RemoveDeviceCompleted(this, e);
            }
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="device">设备对象</param>
        public void AddDevice(Device device)
        {
            Exception ex = null;

            if (device != null)
            {
                if (devices == null)
                {
                    ex = new Exception("设备列表重新初始化，添加设备成功。");
                    devices = new List<Device>();
                    devices.Add(device);
                }
                else
                {
                    if (devices.IndexOf(device) >= 0)
                    {
                        ex = new Exception("设备已经存在于设备列表中。");
                    }
                    else
                    {
                        ex = new Exception("添加设备成功。");
                        devices.Add(device);
                    }
                }
            }
            else
            {
                ex = new Exception("设备对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnAddDevice(arg);
        }

        /// <summary>
        /// 移除设备
        /// </summary>
        /// <param name="device">设备对象</param>
        public void RemoveDevice(Device device)
        {
            Exception ex = null;

            if (device != null)
            {
                if (devices == null)
                {
                    ex = new Exception("设备列表不存在，已将其重新初始化。");
                    devices = new List<Device>();
                }
                else
                {
                    if (devices.Remove(device))
                    {
                        ex = new Exception("从设备列表中移除设备成功。");
                    }
                    else
                    {
                        ex = new Exception("设备在设备列表中不存在。");
                    }
                }
            }
            else
            {
                ex = new Exception("设备对象不能为空。");
            }

            UpdateEventArgs arg = new UpdateEventArgs(ex);
            OnRemoveDevice(arg);
        }
    }
}