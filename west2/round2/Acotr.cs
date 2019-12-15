using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    /// <summary>
    /// 对象类
    /// </summary>
    class Actor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 血量
        /// </summary>
        public int Hp;
        /// <summary>
        /// 魔法值
        /// </summary>
        public int Mp;

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <param name="name">对象名</param>
        public Actor(string name)
        {
            this.Name = name;
        }
       

    }
}
