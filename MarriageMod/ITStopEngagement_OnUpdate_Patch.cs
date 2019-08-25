﻿using Harmony12;
using BehaviorDesigner.Runtime.Tasks;
using Pathea.Behavior;
using System;

namespace MarriageMod
{
    public static partial class Main
    {
        [HarmonyPatch(typeof(ITStopEngagement), "OnUpdate", new Type[] { })]
        static class ITStopEngagement_OnUpdate_Patch
        {
            static bool Prefix(ref TaskStatus __result)
            {
                if (!Main.enabled)
                    return true;
                Dbgl("ITStopEngagement_OnUpdate_Patch");
                __result = TaskStatus.Failure;
                return false;
            }
        }
    }
}