using AutoEvent_5KMode.Loader;
using LabApi.Features.Console;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues
{
    public class ServerTranslate
    {
        public static List<TranslateInstance> Translates = new List<TranslateInstance>();
        public static void RegisterAllTranslate()
        {
            if(File.Exists(CustomPaths.GetTranslateFile(ServerLanguage.zhCN, TranslateType.CustomRoleName)))
            {
                if(File.ReadAllText(CustomPaths.GetTranslateFile(ServerLanguage.zhCN, TranslateType.CustomRoleName))=="[]")
                {
                    List<TranslateInstance> RoleNameTranslates = new List<TranslateInstance>()
            {
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-血契者","深红教会血楔打击组-血契者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-引导者","深红教会血楔打击组-引导者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "深红教会血楔打击组-异教徒","深红教会血楔打击组-异教徒"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-铭文师","深红教会血楔打击组-铭文师"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "深红教会血楔打击组-狂信者","深红教会血楔打击组-狂信者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "GOC奇术打击二组-指挥官","GOC奇术打击二组-指挥官"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"GOC奇术打击二组-奇术师","GOC奇术打击二组-奇术师"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"GOC奇术打击二组-维稳者","GOC奇术打击二组-维稳者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"GOC奇术打击二组-观察员","GOC奇术打击二组-观察员"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "蛇之手折跃小组-折跃引导者","蛇之手折跃小组-折跃引导者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "蛇之手折跃小组-解放者","蛇之手折跃小组-解放者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "蛇之手折跃小组-书页守卫","蛇之手折跃小组-书页守卫"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"蛇之手折跃小组-流浪者","蛇之手折跃小组-流浪者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "落锤-队长","落锤-队长"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"落锤-重火力手","落锤-重火力手"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"落锤-爆破手","落锤-爆破"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "落锤-步枪手","落锤-步枪手"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"火箭侠-空中指挥官","火箭侠-空中指挥官"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"火箭侠-空降手","火箭侠-空降手"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "火箭侠-火力手","火箭侠-火力手"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"火箭侠-工兵","火箭侠-工兵"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"最后的希望-队长(SCP105)","最后的希望-队长(SCP105)"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"最后的希望-Cain(SCP-073)","最后的希望-Cain(SCP-073)"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"最后的希望-前线突击手","最后的希望-前线突击手"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"最后的希望-侦察兵(SCP2913)","最后的希望-侦察兵(SCP2913)"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName, "最后的希望-安保负责人","最后的希望-安保负责人"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-引导者","深红教会血楔打击组-引导者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-血契者","深红教会血楔打击组-血契者"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-铭文师","深红教会血楔打击组-铭文师"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomRoleName,"深红教会血楔打击组-狂信者","深红教会血楔打击组-狂信者"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-血契者","Crimson Church Blood Wedge Strike Team - Blood-Bound"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-引导者","Crimson Church Blood Wedge Strike Team - Conduit"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "深红教会血楔打击组-异教徒","Crimson Church Blood Wedge Strike Team - Heretic"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-铭文师","Crimson Church Blood Wedge Strike Team - Scribe"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "深红教会血楔打击组-狂信者","Crimson Church Blood Wedge Strike Team - Zealot"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "GOC奇术打击二组-指挥官","GOC Occult Strike Team 2 - Commander"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"GOC奇术打击二组-奇术师","GOC Occult Strike Team 2 - Thaumaturge"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"GOC奇术打击二组-维稳者","GOC Occult Strike Team 2 - Stabilizer"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"GOC奇术打击二组-观察员","GOC Occult Strike Team 2 - Observer"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "蛇之手折跃小组-折跃引导者","Serpent's Hand Warp Team - Warp Guide"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "蛇之手折跃小组-解放者","Serpent's Hand Warp Team - Liberator"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "蛇之手折跃小组-书页守卫","Serpent's Hand Warp Team - Page Keeper"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"蛇之手折跃小组-流浪者","Serpent's Hand Warp Team - Wanderer"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "落锤-队长","Hammer Down - Captain"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"落锤-重火力手","Hammer Down - Heavy Gunner"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"落锤-爆破手","Hammer Down - Breacher"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "落锤-步枪手","Hammer Down - Rifleman"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"火箭侠-空中指挥官","Rocket Man - Flight Lead"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"火箭侠-空降手","Rocket Man - Airborne"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "火箭侠-火力手","Rocket Man - Gunner"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"火箭侠-工兵","Rocket Man - Engineer"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"最后的希望-队长(SCP105)","Last Hope - Captain (SCP-105)"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"最后的希望-Cain(SCP-073)","Last Hope - Cain (SCP-073)"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"最后的希望-前线突击手","Last Hope - Frontline Assault"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"最后的希望-侦察兵(SCP2913)","Last Hope - Scout (SCP-2913)"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName, "最后的希望-安保负责人","Last Hope - Security Lead"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-引导者","Crimson Church Blood Wedge Strike Team - Conduit"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-血契者","Crimson Church Blood Wedge Strike Team - Blood-Bound"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-铭文师","Crimson Church Blood Wedge Strike Team - Scribe"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomRoleName,"深红教会血楔打击组-狂信者","Crimson Church Blood Wedge Strike Team - Zealot"),
            };
                    string jsontext = JsonConvert.SerializeObject(RoleNameTranslates, Formatting.Indented);
                    File.WriteAllText(CustomPaths.GetTranslateFile(ServerLanguage.zhCN, TranslateType.CustomRoleName),jsontext);
                    Translates.AddRange(RoleNameTranslates);
                }
                else
                {
                    try
                    {
                        List<TranslateInstance> RoleNameTranslates = (List<TranslateInstance>)JsonConvert.DeserializeObject(File.ReadAllText(CustomPaths.GetTranslateFile(ServerLanguage.zhCN, TranslateType.CustomRoleName)));
                        Translates.AddRange(RoleNameTranslates);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{CustomPaths.GetTranslateFile(ServerLanguage.zhCN, TranslateType.CustomRoleName)}无法读取特殊角色名称翻译");
                    }
                }
            }
            
            List<TranslateInstance> CommandTranslates = new List<TranslateInstance>()
            {
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomCommand, "helptest","可以使用以下命令:\ntest5k role <特殊角色ID> <玩家ID> - 刷新角色\ntest5k item <特殊物品ID> <玩家ID> - 给物品\ntest 5k loadani <特殊阵营ID> - 测试加载特殊动画"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomCommand, "helptest","You can use the following commands:\ntest5k role <role id> <player ID> - refresh character\ntest5k item <Specifi item Id> <player ID> - give item\ntest5k loadani <special faction ID> - test loading special animation"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomCommand,"rolelist","特殊角色列表\n使用test5k role <特殊角色ID> <玩家ID>刷新\n"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomCommand, "rolelist","Special character list\nUse test5k role <special character ID> <player ID> to refresh\n"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomCommand, "itemlist","特殊物品列表\n使用test5k item <特殊物品ID> <玩家ID>给物品\n"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomCommand, "itemlist","Special Items List\nUse test5k item <special item ID> <player ID> to give the item\n"),
                new TranslateInstance(ServerLanguage.zhCN, TranslateType.CustomCommand, "loadani","使用test5k loadani <特殊阵营id> 测试阵营刷新动画"),
                new TranslateInstance(ServerLanguage.enUS, TranslateType.CustomCommand, "loadani","Use text5k loadani <TeamId> To Test Specifiec Team Load Aniation\n"),
            };
            Translates.AddRange(CommandTranslates);
        }
        public static string GetCustomTranslate(ServerLanguage serverLanguage, TranslateType translateType, string Name)
        {
            if (!Translates.Any(x => x.Language == serverLanguage&&x.TranslateType==translateType)) return "";
            return Translates.FirstOrDefault(x => x.Language == serverLanguage&&x.TranslateType==translateType).TranslateText;
        }
        public static ServerLanguage GetConfigLanguage()
        {
            return GetLanguage(Plugin.Instance.Config.PluginLanguage);
        }
        public static ServerLanguage GetLanguage(string language)
        {
            switch(language)
            {
                case "cn":
                case "zh-CN":
                    return ServerLanguage.zhCN;
                case "en":
                case "en-us":
                    return ServerLanguage.enUS;
                default:
                    return ServerLanguage.zhCN;
            }
        }
        public enum TranslateType
        {
            CustomRoleName=0,
            CustomRoleDescription=1,
            CustomAbility=2,
            CustomItemName=3,
            CustomItemDescription=4,
            CustomCommand=5
        }
        public enum ServerLanguage
        {
            zhCN=0,
            enUS=1
        }
        public class TranslateInstance
        {
            public ServerLanguage Language;
            public TranslateType TranslateType;
            public string Name;
            public string TranslateText;
            public TranslateInstance(ServerLanguage serverLanguage, TranslateType translateType, string name, string translateText)
            {
                Language = serverLanguage;
                TranslateType = translateType;
                Name = name;
                TranslateText = translateText;
            }
        }
    }
    
}
