using HarmonyLib;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(Thing), nameof(Thing.Destroy))]
public static class Thing_Destroy
{
    public static void Prefix(Thing __instance)
    {
        MoveInteractionCell.ResetOverrideCell(__instance);
    }
}