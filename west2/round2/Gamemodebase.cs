using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    /// <summary>
    /// 游戏模式类
    /// </summary>
    class Gamemodebase
    {
        /// <summary>
        /// 玩家
        /// </summary>
        public Player player;
        /// <summary>
        /// 游戏开始
        /// </summary>
        public void Beginplay()
        {
            string msg="";
            string op;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------");
                Console.WriteLine("|               |");
                Console.WriteLine("|               |");
                Console.WriteLine("|   1.开始游戏  |");
                Console.WriteLine("|   2.退出      |");
                Console.WriteLine("|               |");
                Console.WriteLine("|               |");
                Console.WriteLine("-----------------");
                Console.WriteLine(msg);
                Console.WriteLine("请输入操作:");
                msg = "";
                op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Newgame();
                        break;
                    case "2":
                        return;
                        break;
                    default:
                        msg = "无效操作（1-2）";
                        break;
                }

            }
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        public void Newgame()
        {
            Console.Clear();
            Console.WriteLine("憨憨，你叫什么:");
            string name = Console.ReadLine();
            player = new Player(name);
            player.AddSkill(1);
            player.AddSkill(0);
            Console.WriteLine("勇者"+ player.Name+",大Boss库巴把你绿，你现在要去杀了它，但你现在的能力还不够，试着捏些软柿子吧！(回车继续)");
            Console.ReadLine();
            Maingame();
        }
        /// <summary>
        /// 游戏主界面
        /// </summary>
        public void Maingame()
        {
            string msg="";
            string op;
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("姓名: " + player.Name);
                Console.WriteLine("-----------------");
                Console.WriteLine("|               |");
                Console.WriteLine("|     主界面    |");
                Console.WriteLine("|   1.探险模式  |");
                Console.WriteLine("|   2.装备栏    |");
                Console.WriteLine("|   3.技能栏    |");
                Console.WriteLine("|   4.角色信息  |");
                Console.WriteLine("|   5.BOSS战    |");
                Console.WriteLine("|   6.返回主菜单|");
                Console.WriteLine("-----------------");
                Console.WriteLine(msg);
                Console.WriteLine("请输入操作:");
                msg = "";
                op = Console.ReadLine();
                MonsterItem enemy;
                Battle Abattle;
                switch (op)
                {
                    case "1":
                        Random rnd=new Random();
                        int x = rnd.Next(0, 3);
                        enemy = new MonsterItem(x);
                        Abattle = new Battle(player, enemy);
                        if (Abattle.BeginBattle())
                        {
                            Console.WriteLine("获得了" + enemy.exp + "点经验值");
                            player.AddExp(enemy.exp);
                            for(int i = 0; i < enemy.drop.Count; i++)
                            {
                                if (enemy.drop[i].type == "skill")
                                {
                                    player.AddSkill(enemy.drop[i].id);
                                }
                                else
                                {
                                    player.AddEquipment(enemy.drop[i].id);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("垃圾，你死了，重新来吧");
                            Console.ReadLine();
                            return;
                        }
                   
                        
                        break;
                    case "2":
              
                        EquipmentMenu();
                        break;
                    case "3":
                       
                        SkillMenu();
                        break;
                    case "4":
                        PersonMenu();
                        break;
                    case "5":
                        enemy = new MonsterItem(3);
                        Abattle = new Battle(player, enemy);
                        if (Abattle.BeginBattle())
                        {
                            Console.WriteLine("获得了" + enemy.exp + "点经验值");
                            player.AddExp(enemy.exp);
                            for (int i = 0; i < enemy.drop.Count; i++)
                            {
                                if (enemy.drop[i].type == "skill")
                                {
                                    player.AddSkill(enemy.drop[i].id);
                                }
                                else
                                {
                                    player.AddEquipment(enemy.drop[i].id);
                                }
                            }
                            Console.WriteLine("库巴被打败了，但你杀了它之后才发现它是被冤枉的，绿你的是隔壁老王，因此你要去杀了他（下一作）");
                            Console.ReadLine();

                        }
                        else
                        {
                            Console.WriteLine("垃圾，你死了，重新来吧");
                            Console.ReadLine();
                            return;
                        }
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        msg = "无效操作（1-6）";
                        break;
                }
                Console.WriteLine("按回车继续");
                Console.ReadLine();

            }
        }
        /// <summary>
        /// 装备栏
        /// </summary>
        public void EquipmentMenu()
        {

            string msg="";
            string op;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("姓名: " + player.Name);
                Console.WriteLine("----------------------------------");
                Console.WriteLine("|                                 ");
                Console.WriteLine("|           角色装备页面          ");
                if(player.Equipments[0]!=null)
                Console.WriteLine("|      1.头饰:" + player.Equipments[0].name);
                else
                {
                    Console.WriteLine("|      1.头饰:无");
                }
                if (player.Equipments[1] != null)
                    Console.WriteLine("|      2.衣服:" + player.Equipments[1].name);
                else
                {
                    Console.WriteLine("|      2.衣服:无");
                }
                if (player.Equipments[2] != null)
                    Console.WriteLine("|      3.武器:" + player.Equipments[2].name);
                else
                {
                    Console.WriteLine("|      3.武器:无");
                }
                if (player.Equipments[3] != null)
                    Console.WriteLine("|      4.鞋子:" + player.Equipments[3].name);
                else
                {
                    Console.WriteLine("|      4.鞋子:无");
                }
                Console.Write("|   仓库:");
                for (int i = 0; i < player.Equipmentstore.Count; i++)
                {
                    Console.Write((i + 1) + "." + player.Equipmentstore[i].name + " ");
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine(msg);
                msg = "";
                Console.WriteLine("请输入操作(要更改的部位-仓库序号，0-0表示退出到主界面,0-仓库序号表示丢弃):");
                op = Console.ReadLine();

                if (op.Length != 3)
                {
                    msg = "换装指令出错";
                }
                else if (op.Equals( "0-0"))
                {
                    break;
                }
                else
                {
                   if( player.EquipmentChange((int)op[0]-49, (int)op[2]-49))
                    {
                      
                    }
                    else
                    {

                        msg = "换装失败";
                    }
                }
            }

        }
        /// <summary>
        /// 技能栏
        /// </summary>
        public void SkillMenu()
        {

            string msg="";
            string op;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("姓名: " + player.Name);
                Console.WriteLine("----------------------------------");
                Console.WriteLine("|                                 ");
                Console.WriteLine("|           角色技能页面          ");
                if (player.Skills[0] != null)
                    Console.WriteLine("|      1.:" + player.Skills[0].name);
                else
                {
                    Console.WriteLine("|      1.:无");
                }
                if (player.Skills[1] != null)
                    Console.WriteLine("|      2.:" + player.Skills[1].name);
                else
                {
                    Console.WriteLine("|      2.:无");
                }
                if (player.Skills[2] != null)
                    Console.WriteLine("|      3.:" + player.Skills[2].name);
                else
                {
                    Console.WriteLine("|      3.:无");
                }
                if (player.Skills[3] != null)
                    Console.WriteLine("|      4.:" + player.Skills[3].name);
                else
                {
                    Console.WriteLine("|      4.:无");
                }
                Console.Write("|   仓库:");
                for (int i = 0; i < player.Skillstore.Count; i++)
                {
                    Console.Write((i + 1) + "." + player.Skillstore[i].name + " ");
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine(msg);
                msg = "";
                Console.WriteLine("请输入操作(要更改的技能-仓库序号，0-0表示退出到主界面,0-仓库序号表示丢弃):");
                op = Console.ReadLine();

                if (op.Length != 3)
                {
                    msg = "换技能指令出错";
                }
                else if (op.Equals("0-0"))
                {
                    break;
                }
                else
                {
                    if (player.SkillChange((int)op[0] - 49, (int)op[2] - 49))
                    {
                        
                    }
                    else
                    {

                        msg = "换技能失败";

                    }
                }
            }

        }
        /// <summary>
        /// 个人信息页面
        /// </summary>
        public void PersonMenu()
        {
      


                Console.Clear();

                Console.WriteLine("----------------------------------");
                Console.WriteLine("|                                 ");
                Console.WriteLine("|           角色信息页面          ");
                Console.WriteLine("|    姓名: " + player.Name);
                Console.WriteLine("|    等级: " + player.Level);
                Console.WriteLine("|    经验: " + player.Exp+"/"+player.Exptolevelup);
                Console.WriteLine("|    HP: " + player.Hp);
                Console.WriteLine("|    MP: " + player.Mp);
                Console.WriteLine("|    Atk: " + player.GetAtk());
                Console.WriteLine("|    Def: " + player.GetDef());
                Console.WriteLine();
     
                Console.WriteLine("----------------------------------");
                Console.WriteLine("输入任意返回主界面:");
                 Console.ReadLine();


                
            
        }
    }
}
