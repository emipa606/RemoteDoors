using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RemoteDoors
{
    // Token: 0x02000002 RID: 2
    internal class Building_RemoteEmbrasure : Building, IRemoteTargetable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public bool OpenInt
		{
			get
			{
				return openInt;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		private bool IsPowered
		{
			get
			{
				CompPowerTrader comp = GetComp<CompPowerTrader>();
				bool flag = comp == null;
				return !flag && comp.PowerOn;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00000284
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref openInt, "open", true, false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A1 File Offset: 0x000002A1
		public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
		{
			def = ThingDefOf.RemoteEmbrasure_Close;
			base.Destroy(mode);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B7 File Offset: 0x000002B7
		public void Action()
		{
			ChangeState();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C4 File Offset: 0x000002C4
		private void RefreshGraphic()
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(Thing), "graphicInt");
			fieldInfo.SetValue(this, null);
            _ = DefaultGraphic;
        }

		// Token: 0x06000007 RID: 7 RVA: 0x000020F7 File Offset: 0x000002F7
		private void CloseEmbrasure()
		{
			openInt = false;
			def = ThingDefOf.RemoteEmbrasure_Close;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		private void OpenEmbrasure()
		{
			openInt = true;
			def = ThingDefOf.RemoteEmbrasure_Open;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002124 File Offset: 0x00000324
		private void ChangeState()
		{
			bool flag = !IsPowered;
			if (!flag)
			{
				bool flag2 = openInt;
				if (flag2)
				{
					CloseEmbrasure();
				}
				else
				{
					OpenEmbrasure();
				}
				RefreshGraphic();
                DirtyMapMesh(Map);
			}
		}

		// Token: 0x04000001 RID: 1
		private bool openInt;
	}
}
