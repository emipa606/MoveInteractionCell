using HarmonyLib;
using RimWorld;

namespace MoveInteractionCell;

//[HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.CanPlaceBlueprintAt))]
[HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.CanPlaceBlueprintAt_NewTemp))]
public static class GenConstruct_CanPlaceBlueprintAt_NewTemp
{
    public static void Prefix()
    {
        MoveInteractionCell.InterceptThingList = true;
    }

    public static void Postfix()
    {
        MoveInteractionCell.InterceptThingList = false;
    }
}