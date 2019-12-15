using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    /// <summary>
    /// 玩家类
    /// </summary>
    class Player:Actor
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level;
        /// <summary>
        /// 升级所需经验值
        /// </summary>
        public int Exptolevelup;
        /// <summary>
        /// 当前经验值
        /// </summary>
        public int Exp;

        

       /// <summary>
       /// 装备仓库
       /// </summary>
        public List<EquipmentItem> Equipmentstore = new List<EquipmentItem>();
        /// <summary>
        /// 技能仓库
        /// </summary>
        public List<SkillItem> Skillstore = new List<SkillItem>();
        /// <summary>
        /// 身上装备   0:head 1:body 2:weapon 3:shoes
        /// </summary>
        public EquipmentItem []Equipments=new EquipmentItem[4];
        /// <summary>
        /// 身上技能
        /// </summary>
        public SkillItem []Skills = new SkillItem[4];
        /// <summary>
        /// 玩家初始化
        /// </summary>
        /// <param name="name">玩家名</param>
        public Player(string name):base(name)
        {
            this.SetLevel(1);
        }
        /// <summary>
        /// 经验值增加
        /// </summary>
        /// <param name="value">增加值</param>
        public void AddExp(int value)
        {
            Exp +=value;
            while(Exp>= Exptolevelup)
            {
                levelup();
            }
        }
        /// <summary>
        /// 设置玩家等级
        /// </summary>
        /// <param name="level">玩家等级</param>
        private void SetLevel(int level)
        {
            Level = level;
            Hp = Level * 200;
            Mp = Level * 75;
            Exp -= Exptolevelup;
            if (level <= 25)
            {
                Exptolevelup = 10 + 10 * level;
            }else
            {
                Exptolevelup = 999999;
            }
        }
        /// <summary>
        /// 玩家升级
        /// </summary>
        private void levelup()
        {
            Console.WriteLine("升到了" + (Level + 1) + "级");
            SetLevel(Level + 1);
        }
        /// <summary>
        /// 换装
        /// </summary>
        /// <param name="from">要换下的装备栏位置</param>
        /// <param name="to">要换上的装备序号</param>
        /// <returns></returns>
        public bool EquipmentChange(int from,int to)
        {
            try
            {
                if (from == -1)
                {
                    Equipmentstore.Remove(Equipmentstore[to]);
                    return true;
                }


                if (Equipmentstore[to].type == from)
                {
                    if (Equipments[from] != null) { 
                    Equipmentstore.Add(Equipments[from]);
                    }
                    Equipments[from] = Equipmentstore[to];
                    Equipmentstore.Remove(Equipmentstore[to]);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 换技能
        /// </summary>
        /// <param name="from">要换下的技能位置</param>
        /// <param name="to">要换上的技能序号</param>
        /// <returns></returns>
        public bool SkillChange(int from, int to)
        {
            try
            {
                if (from == -1)
                {
                    Skillstore.Remove(Skillstore[to]);
                    return true;
                }
                if (Skills[from] != null) {
                    Console.WriteLine("error");
                Skillstore.Add(Skills[from]);
                }
                Skills[from] = Skillstore[to];
                Skillstore.Remove(Skillstore[to]);
            }
            catch
            {
                return false;
            }
        
            return true;
        }
        /// <summary>
        /// 获得技能
        /// </summary>
        /// <param name="id">技能id</param>
        public void AddSkill(int id)
        {
            SkillItem skill = new SkillItem(id);
            if (Skillstore.Count <= 8) { 
            
            Skillstore.Add(skill);
            Console.WriteLine("获得了技能:" + skill.name);
            }
            else
            {
               
                Console.WriteLine("本来可以获得技能" + skill.name+ "但是技能背包满了");
            }
        }
        /// <summary>
        /// 获得装备
        /// </summary>
        /// <param name="id">装备id</param>
        public void AddEquipment(int id)
        {
            EquipmentItem equipment = new EquipmentItem(id);
            if (Equipmentstore.Count <= 8)
            {
                

                Equipmentstore.Add(equipment);
                Console.WriteLine("获得了物品:" + equipment.name);
            }
            else
            {
                
                Console.WriteLine("本来可以获得装备" + equipment.name + "但是装备背包满了");
            }
        }
        /// <summary>
        /// 获取攻击力
        /// </summary>
        /// <returns></returns>
         public int GetAtk()
        {
            int total=0;
            for(int i = 0; i < 4; i++)
            {
                if (Equipments[i] != null)
                {
                    total += Equipments[i].atk;
                }
            }
            return total;
        }
        /// <summary>
        /// 获取防御力
        /// </summary>
        /// <returns></returns>
        public int GetDef()
        {
            int total = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Equipments[i] != null)
                {
                    total += Equipments[i].def;
                }
            }
            return total;
        }
    }
}
