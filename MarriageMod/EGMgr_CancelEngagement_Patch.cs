﻿using Harmony12;
using Pathea.EG;
using System;

namespace MarriageMod
{
    public static partial class Main
    {
        [HarmonyPatch(typeof(EGMgr), "CancelEngagement", new Type[] { typeof(EGStopType) })]
        static class EGMgr_CancelEngagement_Patch
        {
            static bool Prefix(EGStopType type)
            {
                if (!Main.enabled)
                    return true;
                Dbgl("EGMgr_CancelEngagement_Patch "+type.ToString());
                if (type == EGStopType.Jealous)
                {
                    return false;
                }
                return true;
            }
        }
    }
}