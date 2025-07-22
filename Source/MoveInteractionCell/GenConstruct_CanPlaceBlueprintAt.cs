using HarmonyLib;
using RimWorld;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.CanPlaceBlueprintAt))]
public static class GenConstruct_CanPlaceBlueprintAt
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