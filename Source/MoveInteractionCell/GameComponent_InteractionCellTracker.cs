using System.Collections.Generic;
using Verse;

namespace MoveInteractionCell;

public class GameComponent_InteractionCellTracker : GameComponent
{
    public Dictionary<Thing, IntVec3> CustomInteractionCells = new Dictionary<Thing, IntVec3>();
    private List<Thing> customInteractionCellsKeys;
    private List<IntVec3> customInteractionCellsValues;

    public GameComponent_InteractionCellTracker(Game game)
    {
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref CustomInteractionCells, "CustomInteractionCells", LookMode.Reference,
            LookMode.Value, ref customInteractionCellsKeys, ref customInteractionCellsValues);
    }
}