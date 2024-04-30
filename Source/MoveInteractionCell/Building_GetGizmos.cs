using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(Building), nameof(Building.GetGizmos))]
public static class Building_GetGizmos
{
    public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> values, Building __instance)
    {
        foreach (var value in values)
        {
            yield return value;
        }

        if (__instance is not { Spawned: true } ||
            !MoveInteractionCell.BuildingsWithInteractionCell.Contains(__instance.def))
        {
            yield break;
        }

        var currentCell = MoveInteractionCell.ActualPlaceFromOffset(__instance.def.interactionCellOffset, __instance);
        if (__instance.OccupiedRect().Contains(currentCell))
        {
            MoveInteractionCell.BuildingsWithInteractionCell.Add(__instance.def);
            yield break;
        }

        yield return new Command_Action
        {
            defaultLabel = "MIC.RotateCell.label".Translate(),
            defaultDesc = "MIC.RotateCell.desc".Translate(),
            action = delegate { MoveInteractionCell.RotateSpot(__instance); },
            icon = Event.current.shift ? TexUI.RotLeftTex : TexUI.RotRightTex
        };
    }
}