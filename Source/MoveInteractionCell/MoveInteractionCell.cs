using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoveInteractionCell;

[StaticConstructorOnStartup]
public static class MoveInteractionCell
{
    public static readonly List<ThingDef> BuildingsWithInteractionCell;
    public static readonly Dictionary<Thing, OverrideInfo> Overrides = [];
    public static bool InterceptThingList;
    public static bool InterceptThingListFast;
    public static readonly Thing BlueprintDummy;

    static MoveInteractionCell()
    {
        BuildingsWithInteractionCell =
            DefDatabase<ThingDef>.AllDefsListForReading.Where(def =>
                def.hasInteractionCell && def.multipleInteractionCellOffsets.NullOrEmpty() &&
                def.interactionCellOffset != IntVec3.Zero && def.thingClass != typeof(Building_VoidMonolith)).ToList();
        BlueprintDummy = new Thing();
        new Harmony("Mlie.MoveInteractionCell").PatchAll(Assembly.GetExecutingAssembly());
    }

    public static bool SetOverride(Thing building, bool useDummyThing = false)
    {
        var firstThing = building.GetInnerIfMinified();

        if (!BuildingsWithInteractionCell.Contains(firstThing.def))
        {
            return false;
        }

        var cellTracker = Current.Game.GetComponent<GameComponent_InteractionCellTracker>();
        if (cellTracker == null)
        {
            return false;
        }

        if (!cellTracker.CustomInteractionCells.TryGetValue(firstThing, out var cell))
        {
            return false;
        }

        if (useDummyThing)
        {
            Overrides[BlueprintDummy] = new OverrideInfo(building, cell);
        }
        else
        {
            Overrides[building] = new OverrideInfo(building, cell);
        }

        return true;
    }

    public static IntVec3 ActualPlaceFromOffset(IntVec3 offset, Building building)
    {
        return offset.RotatedBy(building.Rotation) + building.Position;
    }


    public static IntVec3 OffsetFromActualPlace(IntVec3 actualPlace, Building building)
    {
        if (building.Rotation == Rot4.East || building.Rotation == Rot4.West)
        {
            return (actualPlace - building.Position).RotatedBy(building.Rotation.Opposite);
        }

        return (actualPlace - building.Position).RotatedBy(building.Rotation);
    }


    public static bool ValidateNewSpot(IntVec3 position, Map map)
    {
        if (!position.InBounds(map))
        {
            return false;
        }

        var list = map.thingGrid.ThingsListAtFast(position);
        foreach (var thing in list)
        {
            if (thing.def.passability != 0)
            {
                return false;
            }

            var entityDefToBuild = thing.def.entityDefToBuild;
            if (entityDefToBuild != null && entityDefToBuild.passability != 0)
            {
                return false;
            }
        }

        return true;
    }


    public static void RotateSpot(Building building)
    {
        var cellTracker = Current.Game.GetComponent<GameComponent_InteractionCellTracker>();
        if (cellTracker == null)
        {
            return;
        }

        if (cellTracker.CustomInteractionCells == null)
        {
            cellTracker.CustomInteractionCells = [];
        }

        var currentOffset =
            cellTracker.CustomInteractionCells.GetValueOrDefault(building, building.def.interactionCellOffset);
        if (currentOffset == default)
        {
            currentOffset = building.def.interactionCellOffset;
        }

        var currentCell = ActualPlaceFromOffset(currentOffset, building);

        var validCells = building.OccupiedRect().AdjacentCells.Where(vec3 => ValidateNewSpot(vec3, building.Map))
            .ToList();
        var center = building.Position;

        validCells.Sort((a, b) =>
        {
            var aAngle = Mathf.Atan2(a.z - center.z, a.x - center.x);
            var bAngle = Mathf.Atan2(b.z - center.z, b.x - center.x);

            return aAngle.CompareTo(bAngle);
        });

        var currentIndex = 0;
        if (!validCells.Contains(currentCell))
        {
            if (cellTracker.CustomInteractionCells.ContainsKey(building))
            {
                cellTracker.CustomInteractionCells.Remove(building);
            }
        }
        else
        {
            currentIndex = validCells.IndexOf(currentCell);
        }

        if (Event.current.button == 1)
        {
            var originalCell = ActualPlaceFromOffset(building.def.interactionCellOffset, building);
            if (validCells.Contains(originalCell))
            {
                ResetOverrideCell(building);
                return;
            }

            Messages.Message("MIC.ResetCellFail".Translate(building.LabelCap), building, MessageTypeDefOf.RejectInput);
            return;
        }

        if (Event.current.shift)
        {
            currentIndex++;
            if (currentIndex + 1 > validCells.Count)
            {
                currentIndex = 0;
            }
        }
        else
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = validCells.Count - 1;
            }
        }

        currentCell = validCells[currentIndex];

        var newOffset = OffsetFromActualPlace(currentCell, building);

        if (newOffset == building.def.interactionCellOffset)
        {
            if (!cellTracker.CustomInteractionCells.ContainsKey(building))
            {
                return;
            }

            cellTracker.CustomInteractionCells.Remove(building);
            if (Overrides.ContainsKey(building))
            {
                Overrides.Remove(building);
            }

            return;
        }

        cellTracker.CustomInteractionCells[building] = newOffset;
    }

    public static void ResetOverrideCell(Thing building)
    {
        var cellTracker = Current.Game.GetComponent<GameComponent_InteractionCellTracker>();
        if (Overrides.ContainsKey(building))
        {
            Overrides.Remove(building);
        }

        if (cellTracker.CustomInteractionCells.ContainsKey(building))
        {
            cellTracker.CustomInteractionCells.Remove(building);
        }

        if (building is not Blueprint && building is not Frame)
        {
            return;
        }

        if (Overrides.ContainsKey(BlueprintDummy))
        {
            Overrides.Remove(BlueprintDummy);
        }

        if (cellTracker.CustomInteractionCells.ContainsKey(BlueprintDummy))
        {
            cellTracker.CustomInteractionCells.Remove(BlueprintDummy);
        }
    }

    public static Thing GetSelectedItem()
    {
        var singleSelectedThing = Find.Selector.SingleSelectedThing;
        if (singleSelectedThing is MinifiedThing)
        {
            return singleSelectedThing.GetInnerIfMinified();
        }

        if (singleSelectedThing is Building building && building.def.Minifiable)
        {
            return singleSelectedThing;
        }

        return null;
    }
}