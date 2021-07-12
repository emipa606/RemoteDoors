using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RemoteDoors
{
    // Token: 0x02000005 RID: 5
    public class Building_RemoteDoor : Building_Door, IRemoteTargetable
    {
        // Token: 0x06000016 RID: 22 RVA: 0x000023BC File Offset: 0x000005BC
        public void Action()
        {
            ActionDoor();
        }

        // Token: 0x06000012 RID: 18 RVA: 0x000022B9 File Offset: 0x000004B9
        public override IEnumerable<Gizmo> GetGizmos()
        {
            var gizmos = base.GetGizmos().ToList();
            gizmos.RemoveLast();
            foreach (var g in gizmos)
            {
                yield return g;
            }
        }

        // Token: 0x06000013 RID: 19 RVA: 0x000022CC File Offset: 0x000004CC
        public override bool PawnCanOpen(Pawn p)
        {
            return false;
        }

        // Token: 0x06000014 RID: 20 RVA: 0x000022E0 File Offset: 0x000004E0
        public override bool BlocksPawn(Pawn p)
        {
            return !Open;
        }

        // Token: 0x06000015 RID: 21 RVA: 0x000022FC File Offset: 0x000004FC
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
}