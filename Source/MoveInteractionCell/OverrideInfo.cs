using Verse;

namespace MoveInteractionCell;

public class OverrideInfo(Thing thing, IntVec3 replacementOffset)
{
    public IntVec3 OriginalOffset = thing.def.interactionCellOffset;
    public IntVec3 ReplacementOffset = replacementOffset;
    public Rot4 Rotation = thing.Rotation;
    public IntVec3 ThingCenter = thing.Position;
}