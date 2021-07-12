using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RemoteDoors
{
    // Token: 0x02000004 RID: 4
    internal class Building_RemoteController : Building
    {
        // Token: 0x0600000C RID: 12 RVA: 0x0000217D File Offset: 0x0000037D
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
                    action = ActivateRemote
                };
            }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002190 File Offset: 0x00000390
        private bool IsManned()
        {
            var comp = GetComp<CompMannable>();
            return comp != null && comp.MannedNow;
        }

        // Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
        private bool InRange(Thing thing)
        {
            var localTargetInfo = new LocalTargetInfo(thing);
            return (localTargetInfo.Cell - Position).LengthHorizontal < def.specialDisplayRadius;
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
        private void ActivateRemote(LocalTargetInfo localTargetInfo)
        {
            if (localTargetInfo == null || localTargetInfo.Thing == null)
            {
                return;
            }

            if (!InRange(localTargetInfo.Thing))
            {
                Messages.Message("RD_Controller_TargetOutOfRange".Translate(), MessageTypeDefOf.RejectInput);
                return;
            }

            if (!IsManned())
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
}