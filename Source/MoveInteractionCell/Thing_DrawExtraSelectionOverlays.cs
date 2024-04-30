using HarmonyLib;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(Thing), nameof(Thing.DrawExtraSelectionOverlays))]
public static class Thing_DrawExtraSelectionOverlays
{
    public static void Prefix(Thing __instance)
    {
        MoveInteractionCell.SetOverride(__instance);
    }
}