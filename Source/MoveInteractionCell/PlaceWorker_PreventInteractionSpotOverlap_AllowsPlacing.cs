using HarmonyLib;
using RimWorld;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(PlaceWorker_PreventInteractionSpotOverlap),
    nameof(PlaceWorker_PreventInteractionSpotOverlap.AllowsPlacing))]
public static class PlaceWorker_PreventInteractionSpotOverlap_AllowsPlacing
{
    public static void Prefix()
    {
        MoveInteractionCell.InterceptThingListFast = true;
    }

    public static void Postfix()
    {
        MoveInteractionCell.InterceptThingListFast = false;
    }
}