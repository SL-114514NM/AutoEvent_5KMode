using CommandSystem;
using LabApi.Features.Wrappers;
using ProjectMER.Features.Objects;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class GOCDecryptCommand : ICommand
    {
        public string Command => "gocd";

        public string[] Aliases => new string[] {"gcd" };

        public string Description => "GOC奇术队员解密命令";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender playerCommandSender = sender as PlayerCommandSender;
            if(playerCommandSender == null)
            {
                response = "null of Player";
                return false;
            }
            Player player = Player.Get(playerCommandSender.SenderId);
            if(arguments.Count <1)
            {
                response = "using:gcd <password> To Use This Command";
                return false;
            }
            if(!Physics.Raycast(player.Position,player.Camera.forward, out RaycastHit raycast, 3))
            {
                response = "需要对准密钥破解机进行输入";
                return false;
            }
            if(!raycast.collider.gameObject.TryGetComponent<SchematicObject>(out SchematicObject component))
            {
                response = "需要对准密钥破解机进行输入";
                return false;
            }
            if(!GOCDecryptHandler.MiYaoJis.ContainsKey(component))
            {
                response = "需要对准密钥破解机进行输入";
                return false;
            }
            GOCDecryptHandler.MiYaoJis.TryGetValue(component, out int Id);
            if(!GOCDecryptHandler.MiYaos.ContainsKey(Id))
            {
                response = "需要对准密钥破解机进行输入";
                return false;
            }
            GOCDecryptHandler.MiYaos.TryGetValue(Id, out string currpassword);
            string PlayerInPut = arguments.ElementAt(0);
            if(currpassword!=PlayerInPut)
            {
                response = "请输入正确密码";
                return false;
            }
            GOCDecryptHandler.OnHandler(player, Id, PlayerInPut);
            response = "OK";
            return true;
        }
    }
}
