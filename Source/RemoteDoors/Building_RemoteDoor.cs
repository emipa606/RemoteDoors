using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RemoteDoors;

public class Building_RemoteDoor : Building_Door, IRemoteTargetable
{
    public void Action()
    {
        ActionDoor();
    }

    public override IEnumerable<Gizmo> GetGizmos()
    {
        var gizmos = base.GetGizmos().ToList();
        gizmos.RemoveLast();
        foreach (var g in gizmos)
        {
            yield return g;
        }
    }

    public override bool PawnCanOpen(Pawn p)
    {
        return false;
    }

    public override bool BlocksPawn(Pawn p)
    {
        return !Open;
    }

    private void ActionDoor()
    {
        if (!DoorPowerOn)
        {
            Messages.Message("RD_Door_NotPowered".Translate(), new LookTargets(this), MessageTypeDefOf.RejectInput);
        }
        else
        {
            var fieldInfo = AccessTools.Field(typeof(Building_Door), "holdOpenInt");
            var open = Open;
            if (open)
            {
                fieldInfo.SetValue(this, false);
                if (DoorTryClose())
                {
                    return;
                }

                fieldInfo.SetValue(this, true);
                Messages.Message("RD_Door_Blocked".Translate(), new LookTargets(this),
                    MessageTypeDefOf.RejectInput);
            }
            else
            {
                fieldInfo.SetValue(this, true);
                DoorOpen();
            }
        }
    }
}