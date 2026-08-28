using AutoEvent_5KMode.API.Featrues.CustomItem;
using CommandSystem;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class TestPluginCommand : ICommand
    {
        public string Command =>"test5kplugin";

        public string[] Aliases => new string[] {"test5k" };

        public string Description => "测试5k插件内容";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;
            if(!Player.TryGet(playerCommandSender.SenderId, out Player player))
            {
                response = "null of player";
                return false;
            }
            if(arguments.Count <0)
            {
                response = "";
                return false;
            }
            response = "";
            return true;
        }
        public void TestRoleSpawn(Player player, int id)
        {
            if(!CustomRole.CustomRoleManager.CustomRoleList.Any(x=> x.Id == id))
            {
                return;
            }
            CustomRole.CustomRoleManager.CustomRoleList.FirstOrDefault(x=>x.Id == id).Spawn(player);
        }
        public void TestItemGive(Player player, int id)
        {
            if(!CustomItem.CustomItem.CustomItems.Any(x=>x.ID == id))
            {
                return;
            }
            CustomItem.CustomItem.CustomItems.FirstOrDefault(x => x.ID == id).Give(player);
        }
        public void TestTeamLoadAnis(int id)
        {
            ///调试刷新动画，还没写，后面再写qwq
        }
    }
}
