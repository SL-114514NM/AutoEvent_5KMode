using Exiled.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public static class EffectExtension
    {

        public static EffectCategory OutCategory(this EffectType effectType)
        {
            switch(effectType)
            {
                case EffectType.BodyshotReduction:
                case EffectType.DamageReduction:
                case EffectType.Invigorated:
                case EffectType.Invisible:
                case EffectType.MovementBoost:
                case EffectType.Scp1853:
                case EffectType.SpawnProtected:
                case EffectType.RainbowTaste:
                case EffectType.Vitality:
                    return EffectCategory.Positive;
                case EffectType.AmnesiaItems:
                case EffectType.AmnesiaVision:
                case EffectType.Asphyxiated:
                case EffectType.Bleeding:
                case EffectType.Blinded:
                case EffectType.Burned:
                case EffectType.CardiacArrest:
                case EffectType.Corroding:
                case EffectType.PocketCorroding:
                case EffectType.Deafened:
                case EffectType.Decontaminating:
                case EffectType.Disabled:
                case EffectType.Ensnared:
                case EffectType.Exhausted:
                case EffectType.Flashed:
                case EffectType.Hemorrhage:
                case EffectType.Hypothermia:
                case EffectType.Poisoned:
                case EffectType.Scanned:
                case EffectType.Slowness:
                case EffectType.SeveredHands:
                case EffectType.SilentWalk:
                case EffectType.SinkHole:
                case EffectType.Stained:
                case EffectType.Strangled:
                case EffectType.Traumatized:
                    return EffectCategory.Negative;
                case EffectType.AntiScp207:
                case EffectType.Scp207:
                    return EffectCategory.Movement;
                case EffectType.InsufficientLighting:
                case EffectType.SoundtrackMute:
                    return EffectCategory.Harmful;
                case EffectType.None:
                default:
                    return EffectCategory.None;
            }
        }
    }
}
