using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(ThingGrid), nameof(ThingGrid.ThingsListAtFast), typeof(IntVec3))]
public static class ThingGrid_ThingsListAtFast
{
    public static void Postfix(List<Thing> __result)
    {
        if (!MoveInteractionCell.InterceptThingListFast || __result == null || !__result.Any())
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