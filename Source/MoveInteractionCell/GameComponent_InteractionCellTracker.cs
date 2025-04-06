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
        if (Scribe.mode == LoadSaveMode.Saving || Scribe.mode == LoadSaveMode.PostLoadInit)
        {
            var keysToRemove = new List<Thing>();
            foreach (var pair in CustomInteractionCells)
            {
                if (pair.Key == null || pair.Key.Destroyed)
                {
                    keysToRemove.Add(pair.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                CustomInteractionCells.Remove(key);
            }
        }

        Scribe_Collections.Look(ref CustomInteractionCells, "CustomInteractionCells", LookMode.Reference,
            LookMode.Value, ref customInteractionCellsKeys, ref customInteractionCellsValues);
    }
}