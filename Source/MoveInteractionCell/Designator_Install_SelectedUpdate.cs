using HarmonyLib;
using RimWorld;
using Verse;

namespace MoveInteractionCell;

[HarmonyPatch(typeof(Designator_Install), nameof(Designator_Install.SelectedUpdate))]
public static class Designator_Install_SelectedUpdate
{
    public static void Prefix(Rot4 ___placingRot)
    {
        var selectedItem = MoveInteractionCell.GetSelectedItem();

        if (selectedItem == null)
        {
            return;
        }

        if (!MoveInteractionCell.SetOverride(selectedItem, true))
        {
            return;
        }

        MoveInteractionCell.Overrides[MoveInteractionCell.BlueprintDummy].ThingCenter = UI.MouseCell();
        MoveInteractionCell.Overrides[MoveInteractionCell.BlueprintDummy].Rotation = ___placingRot;
    }
}