using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(GridsUtility), nameof(GridsUtility.GetThingList))]
public static class GridsUtility_GetThingList
{
    public static void Postfix(List<Thing> __result)
    {
        if (!MoveInteractionCell.InterceptThingList || __result == null || !__result.Any())
        {
            return;
        }

        foreach (var thing in __result)
        {
            if (MoveInteractionCell.SetOverride(thing))
            {
                break;
            }
        }
    }
}