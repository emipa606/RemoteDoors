using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RemoteDoors;

internal class Building_RemoteController : Building
{
    public override IEnumerable<Gizmo> GetGizmos()
    {
        foreach (var gizmo in base.GetGizmos())
        {
            yield return gizmo;
        }

        if (Faction == Faction.OfPlayer)
        {
            yield return new Command_Target
            {
                defaultLabel = "RD_Controller_Gizmo_Activate".Translate(),
                defaultDesc = "RD_Controller_Gizmo_Activate_Desc".Translate(),
                icon = TexCommand.Attack,
                targetingParams = new TargetingParameters
                {
                    canTargetFires = false,
                    canTargetBuildings = true,
                    canTargetItems = false,
                    canTargetLocations = false,
                    canTargetPawns = false,
                    canTargetSelf = false
                },
                action = activateRemote
            };
        }
    }

    private bool isManned()
    {
        var comp = GetComp<CompMannable>();
        return comp is { MannedNow: true };
    }

    private bool inRange(Thing thing)
    {
        var localTargetInfo = new LocalTargetInfo(thing);
        return (localTargetInfo.Cell - Position).LengthHorizontal < def.specialDisplayRadius;
    }

    private void activateRemote(LocalTargetInfo localTargetInfo)
    {
        if (localTargetInfo == null || localTargetInfo.Thing == null)
        {
            return;
        }

        if (!inRange(localTargetInfo.Thing))
        {
            Messages.Message("RD_Controller_TargetOutOfRange".Translate(), MessageTypeDefOf.RejectInput);
            return;
        }

        if (!isManned())
        {
            Messages.Message("RD_Controller_Unmanned".Translate(), new LookTargets(this),
                MessageTypeDefOf.RejectInput);
            return;
        }

        if (localTargetInfo.Thing is IRemoteTargetable remoteTargetable)
        {
            remoteTargetable.Action();
        }
        else
        {
            Messages.Message("RD_Controller_TargetNotRemote".Translate(), new LookTargets(localTargetInfo.Thing),
                MessageTypeDefOf.RejectInput);
        }
    }
}