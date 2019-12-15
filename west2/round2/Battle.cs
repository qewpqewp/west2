using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    /// <summary>
    /// 战斗界面类
    /// </summary>
    class Battle
    {
        /// <summary>
        /// 本方信息
        /// </summary>
        public BattlePlayer me;
        /// <summary>
        /// 地方信息
        /// </summary>
        public BattlePlayer oppo;
        string msg = "";
        /// <summary>
        /// 开始战斗
        /// </summary>
        /// <returns>胜利失败结果</returns>
        public bool BeginBattle()
        {
            Console.Clear();
            Console.WriteLine("-----------------");
            Console.WriteLine("| " + me.name + "遭遇了" + oppo.name);
            Console.WriteLine("-----------------");
            Console.WriteLine("回车继续");
            Console.ReadLine();
            return preparestate();
        }
        /// <summary>
        /// 初始化双方
        /// </summary>
        /// <param name="player">本方</param>
        /// <param name="monster">敌方</param>
        public Battle(Player player,MonsterItem monster)
        {
            me = new BattlePlayer(player);
            oppo = new BattlePlayer(monster);
        }
        /// <summary>
        /// 技能列表
        /// </summary>
        public void ShowSkillList()
        {
            for (int i = 0; i < me.Skills.Length; i++)
            {

                if (me.Skills[i] != null)
                    Console.Write("{0,10}", (i + 1) + ":" + me.Skills[i].name + "|");
            }
            Console.WriteLine();
            for (int i = 0; i < me.Skills.Length; i++)
            {
                if (me.Skills[i] != null)
                    Console.Write("{0,9}", "伤害:" + me.Skills[i].damage + "|");
            }
            Console.WriteLine();
            for (int i = 0; i < me.Skills.Length; i++)
            {
                if (me.Skills[i] != null)
                    Console.Write("{0,9}", "耗魔:" + me.Skills[i].mana + "|");
            }
        }
        /// <summary>
        /// 战斗信息框架
        /// </summary>
        public void mainframe()
        {
            
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("|                                              ");
            Console.WriteLine("| HP:" + me.HP + "                          " + "HP:" + oppo.HP);
            Console.WriteLine("| MP:" + me.MP + "                          ");
            Console.WriteLine("|                                              ");
            Console.WriteLine("|                                              ");
            Console.WriteLine("|                                              ");
            Console.WriteLine("| " + me.name + "                             " + oppo.name);
            Console.WriteLine("|                                              ");
            Console.WriteLine("|  描述:" + me.description + "             描述:" + oppo.description);
            Console.WriteLine("|                                              ");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            ShowSkillList();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(msg);
            msg = "";
        }
        /// <summary>
        /// 准备阶段
        /// </summary>
        /// <returns></returns>
        public bool preparestate()
        {
            while(me.HP>0 && oppo.HP > 0) {



                mainframe();
            fightstate(mychoose(), enemychoose());
            }

            mainframe();
            if (me.HP <=0)
            {
                Console.WriteLine("失败");
                Console.ReadLine();
                return false;
            }
                Console.WriteLine("胜利");
                Console.ReadLine();
                return true;

            



        }
        /// <summary>
        /// 本方选择技能
        /// </summary>
        /// <returns>技能序号</returns>
        public int mychoose()
        {
            while (true)
            {
                Console.WriteLine("请输入要释放的技能序号,不输入则跳过当前回合");
                string str = Console.ReadLine();

                if (str == "")
                {
                    return -1;
                }
                int x = str[0] - '0';
                
           
                if (x > me.skillcount ||  x<=0)
                {
                    Console.WriteLine("技能序号错误，请重输");
                }
                else if (me.Skills[x - 1].mana > me.MP)
                {
                    Console.WriteLine("蓝量不足，请重输");
                }
                else
                {
                    return x-1;
                }

              
            }

        }
        /// <summary>
        /// 敌方选择技能
        /// </summary>
        /// <returns>技能序号</returns>
        public int enemychoose()
        {
            Random rnd = new Random();
            int skill = rnd.Next(0, oppo.skillcount );
            return skill;

        }
        /// <summary>
        /// 进行一回合
        /// </summary>
        /// <param name="myskill">本方技能序号</param>
        /// <param name="enemyskill">敌方技能序号</param>
        public void fightstate(int myskill,int enemyskill)
        {
            Console.Clear();
            if (myskill != -1) { 
            int mydmg = me.Atk * 3 + me.Skills[myskill].damage-oppo.Def;
            
            Console.WriteLine(me.name + "释放了" + me.Skills[myskill].name + "，对" + oppo.name + "造成了" + mydmg + "点伤害");
            oppo.HP -= mydmg;
            me.MP -= me.Skills[myskill].mana;
            }




            if (oppo.HP < 0)
            {
                
                return;
            }
            else
            {
                int opdmg = oppo.Atk * 3 + oppo.Skills[enemyskill].damage - me.Def;
                Console.WriteLine(oppo.name + "释放了" + oppo.Skills[enemyskill].name + "，对" + me.name + "造成了" + opdmg + "点伤害");
                me.HP -= opdmg;
      
            }
        }
    }
    /// <summary>
    /// 对战角色类
    /// </summary>
    class BattlePlayer
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name;
        /// <summary>
        /// 描述
        /// </summary>
        public string description;
        /// <summary>
        /// 角色血量
        /// </summary>
        public int HP;
        /// <summary>
        /// 角色蓝量
        /// </summary>
        public int MP;
        /// <summary>
        /// 角色攻击力
        /// </summary>
        public int Atk;
        /// <summary>
        /// 角色防御力
        /// </summary>
        public int Def;
        /// <summary>
        /// 角色技能
        /// </summary>
        public SkillItem[] Skills = new SkillItem[4];
        /// <summary>
        /// 创捷本方
        /// </summary>
        /// <param name="player"></param>
        public int skillcount = 0;
        /// <summary>
        /// 创建玩家角色
        /// </summary>
        /// <param name="player"></param>
        public BattlePlayer(Player player)
        {
            this.name = player.Name;
            this.description = "未知";
            this.HP = player.Hp;
            this.MP = player.Mp;
            this.Atk = player.GetAtk();
            this.Def = player.GetDef();
            this.Skills = player.Skills;
            for(int i = 0; i < 4; i++)
            {
                if (Skills[i] != null)
                {
                    skillcount++;
                }
            }
        }
        /// <summary>
        /// 创建敌方
        /// </summary>
        /// <param name="monster"></param>
        public BattlePlayer(MonsterItem monster)
        {
            this.name = monster.name;
            this.description = monster.description;
            this.HP = monster.hp;
            this.MP = monster.mana;
            this.Atk = 5;
            this.Def = 5;
            for (int i = 0; i < monster.skill.Count; i++)
            {
                this.Skills[i] = monster.skill[i];
                
            }
            skillcount = monster.skill.Count;
        }

    }
}
