using Exiled.API.Enums;
using Exiled.API.Features;
using MapGeneration;
using MEC;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AutoEvent_5KMode.API.MapCustEditer
{
    public class MapEditer
    {
        public static Dictionary<Player, SchematicObject> PlayerSchs = new Dictionary<Player, SchematicObject>();
        public static bool AddSchFollowPlayer(Player player, string name, Vector3 offect)
        {
            if (player == null) { return false; }
            Vector3 pos = player.Position + offect;
            SchematicObject schematicObject = ObjectSpawner.SpawnSchematic(name, pos);
            if (schematicObject == null) { return false; }
            if (PlayerSchs.ContainsKey(player))
            {
                PlayerSchs[player] = schematicObject;
                return true;
            }

            PlayerSchs.Add(player, schematicObject);
            Timing.RunCoroutine(Follow(player, offect, schematicObject));
            return true;
        }
        public static bool PlayAnimation(GameObject target, string animationName)
        {
            if (target == null) return false;
            Animation animation = target.GetComponent<Animation>();
            if (animation == null) return false;
            return animation.Play(animationName);
        }
        public static GameObject SpawnCubeAtRoom(RoomType roomName, Vector3 Scale, Quaternion quaternion)
        {
            Vector3 Pos = Room.Get(roomName).Position;
            GameObject gameObject = SpawnCube(Pos, Scale, quaternion);
            return gameObject;
        }
        public static GameObject SpawnCube(Vector3 vector3, Vector3 Scale, Quaternion quaternion)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.position = vector3;
            gameObject.transform.localScale = Scale;
            gameObject.transform.transform.rotation = quaternion;
            return gameObject;
        }
        public static GameObject SpawnCube(Vector3 vector3, Vector3 Scale, Quaternion quaternion, Color color, bool IsHide = false)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.position = vector3;
            gameObject.transform.localScale = Scale;
            gameObject.transform.transform.rotation = quaternion;
            gameObject.GetComponent<Renderer>().material.color = color;
            if (IsHide == true)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            return gameObject;
        }
        public static GameObject SpawnCube(Vector3 vector, Vector3 Scale)
        {
            return SpawnCube(vector, Scale, Quaternion.identity);
        }
        public static GameObject SpawnCube(Vector3 vector3, Quaternion quaternion)
        {
            return SpawnCube(vector3, new Vector3(1, 1, 1), quaternion);
        }
        public static IEnumerator<float> Follow(Player player, Vector3 offect, SchematicObject schematicObject)
        {
            while(true)
            {
                if (player.IsAlive)
                {
                    if (!PlayerSchs.ContainsKey(player)) { yield break; }
                    Vector3 targetEuler = player.Transform.eulerAngles;
                    Vector3 currentEuler = schematicObject.transform.eulerAngles;
                    schematicObject.transform.rotation = Quaternion.Euler(
                currentEuler.x,
                targetEuler.y,
                currentEuler.z
            );
                    Vector3 pos = player.Position + offect;
                    schematicObject.Position = pos;
                }
                else
                {
                    schematicObject.Destroy();
                    yield break;
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
