using HarmonyLib;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(Thing), nameof(Thing.InteractionCell), MethodType.Getter)]
public static class Thing_InteractionCell
{
    public static void Prefix(Thing __instance)
    {
        MoveInteractionCell.SetOverride(__instance);
    }
}