using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
namespace RPG
{
    /// <summary>
    /// Item总类
    /// </summary>
    public class Item {
        protected string itemtype;
        /// <summary>
        /// Item数据
        /// </summary>
        protected static JsonData data;
        /// <summary>
        /// 加载Item数据
        /// </summary>
        static public void LoadJson()

        {

           using (StreamReader r = new StreamReader("data.json"))

            {
               
                string JsonData = r.ReadToEnd();
                data = JsonMapper.ToObject(JsonData);
           


            }

        }
        /// <summary>
        /// 获取item信息(string)
        /// </summary>
        /// <param name="id">itemid</param>
        /// <param name="itemtype">item类型</param>
        /// <param name="inform">信息类别</param>
        /// <returns></returns>
        static public int GetIntByID(int id,string itemtype,string inform)
        {
            return (int)data[itemtype][id][inform];
        }
        /// <summary>
        /// 获取item信息(int)
        /// </summary>
        /// <param name="id">itemid</param>
        /// <param name="itemtype">item类型</param>
        /// <param name="inform">信息类别</param>
        static public string GetStringByID(int id, string itemtype, string inform)
        {
      
            return (string)data[itemtype][id][inform];
        }


    }
    /// <summary>
    /// 装备类
    /// </summary>
    public class EquipmentItem:Item
    {
        
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 描述
        /// </summary>
        public string description;
        /// <summary>
        /// 类型 0:head 1:body 2:weapon 3:shoes
        /// </summary>
        public int type;
        /// <summary>
        /// 攻击力
        /// </summary>
        public int atk;
        /// <summary>
        /// 防御力
        /// </summary>
        public int def;
        /// <summary>
        /// 装备初始化
        /// </summary>
        /// <param name="id">装备id</param>
        public EquipmentItem(int id)
        {
            itemtype = "Equipment";
            this.name = GetStringByID(id, itemtype, "name");
            this.description = GetStringByID(id, itemtype, "description");
            this.type = GetIntByID(id, itemtype, "type");
            this.atk = GetIntByID(id, itemtype, "atk");
            this.def = GetIntByID(id, itemtype, "def");
        }
    }

    /// <summary>
    /// 技能类
    /// </summary>
    public class SkillItem : Item
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 伤害
        /// </summary>
        public int damage;
        /// <summary>
        /// 耗魔
        /// </summary>
        public int mana;
        /// <summary>
        /// 技能初始化
        /// </summary>
        /// <param name="id">技能id</param>
        public SkillItem(int id)
        {
            itemtype = "Skill";
            this.name = GetStringByID(id, itemtype, "name");
            this.damage = GetIntByID(id, itemtype, "damage");
            this.mana = GetIntByID(id, itemtype, "mana");
        }
    }
    /// <summary>
    /// 怪物类
    /// </summary>
    public class MonsterItem : Item
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 描述
        /// </summary>
        public string description;
        /// <summary>
        /// 血量
        /// </summary>
        public int hp;
        /// <summary>
        /// 魔法值
        /// </summary>
        public int mana;
        /// <summary>
        /// 技能表
        /// </summary>
        public List<SkillItem> skill =new List<SkillItem>();
        /// <summary>
        /// 获取经验
        /// </summary>
        public int exp;
        /// <summary>
        /// 掉落表
        /// </summary>
        public List<DropItem> drop=new List<DropItem>();
        /// <summary>
        /// 怪物初始化
        /// </summary>
        /// <param name="id">怪物id</param>
        public MonsterItem(int id)
        {
            itemtype = "Monster";
            this.name = GetStringByID(id, itemtype, "name");
            this.description = GetStringByID(id, itemtype, "description");
            this.hp = GetIntByID(id, itemtype, "hp");
            this.mana = GetIntByID(id, itemtype, "mana");
            foreach (JsonData x in data["Monster"][id]["skill"])
            {
                skill.Add(new SkillItem((int)x));
            }
            this.exp = (int)data["Monster"][id]["exp"];
            foreach (JsonData x in data["Monster"][id]["drop"])
            {
                drop.Add(new DropItem((string)x["type"], (int)x["id"]));
            }
        }

    }
    /// <summary>
    /// 掉落类
    /// </summary>
    public class DropItem
    {
        /// <summary>
        /// 类型 skill equipment
        /// </summary>
        public string type;
        /// <summary>
        /// id
        /// </summary>
        public int id;
        /// <summary>
        /// 掉落初始化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">id</param>
        public DropItem(string type,int id)
        {
            this.type = type;
            this.id = id;
        }
    }


}
