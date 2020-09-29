using System;
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
			foreach (Gizmo gizmo in base.GetGizmos())
			{
				yield return gizmo;
			}
			bool flag = Faction == Faction.OfPlayer;
			if (flag)
			{
				yield return new Command_Target
				{
					defaultLabel = Translator.Translate("RD_Controller_Gizmo_Activate"),
					defaultDesc = Translator.Translate("RD_Controller_Gizmo_Activate_Desc"),
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
					action = new Action<Thing>(ActivateRemote)
				};
			}
			yield break;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002190 File Offset: 0x00000390
		private bool IsManned()
		{
			CompMannable comp = GetComp<CompMannable>();
			bool flag = comp == null;
			return !flag && comp.MannedNow;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
		private bool InRange(Thing thing)
		{
			LocalTargetInfo localTargetInfo = new LocalTargetInfo(thing);
			return (localTargetInfo.Cell - Position).LengthHorizontal < def.specialDisplayRadius;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
		private void ActivateRemote(Thing thing)
		{
			bool flag = thing == null;
			if (!flag)
			{
				bool flag2 = !InRange(thing);
				if (flag2)
				{
					Messages.Message(Translator.Translate("RD_Controller_TargetOutOfRange"), MessageTypeDefOf.RejectInput, true);
				}
				else
				{
					bool flag3 = !IsManned();
					if (flag3)
					{
						Messages.Message(Translator.Translate("RD_Controller_Unmanned"), new LookTargets(this), MessageTypeDefOf.RejectInput, true);
					}
					else
					{
						IRemoteTargetable remoteTargetable = thing as IRemoteTargetable;
						bool flag4 = remoteTargetable != null;
						if (flag4)
						{
							remoteTargetable.Action();
						}
						else
						{
							Messages.Message(Translator.Translate("RD_Controller_TargetNotRemote"), new LookTargets(thing), MessageTypeDefOf.RejectInput, true);
						}
					}
				}
			}
		}
	}
}
