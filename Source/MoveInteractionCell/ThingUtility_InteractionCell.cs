using System.Linq;
using HarmonyLib;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(ThingUtility), nameof(ThingUtility.InteractionCell))]
public static class ThingUtility_InteractionCell
{
    public static void Prefix(ref IntVec3 interactionOffset, IntVec3 thingCenter, ref Rot4 rot)
    {
        if (!MoveInteractionCell.Overrides.Any())
        {
            return;
        }

        var vec3 = interactionOffset;
        var rot4 = rot;
        var offsetOverride = MoveInteractionCell.Overrides.Values.FirstOrDefault(overrideInfo =>
            overrideInfo.OriginalOffset == vec3 && overrideInfo.Rotation == rot4 &&
            overrideInfo.ThingCenter == thingCenter);

        if (offsetOverride == null)
        {
            return;
        }

        interactionOffset = offsetOverride.ReplacementOffset;
    }
}