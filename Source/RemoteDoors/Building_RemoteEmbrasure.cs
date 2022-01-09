using HarmonyLib;
using RimWorld;
using Verse;

namespace RemoteDoors;

internal class Building_RemoteEmbrasure : Building, IRemoteTargetable
{
    private bool openInt;

    public bool OpenInt => openInt;

    private bool IsPowered
    {
        get
        {
            var comp = GetComp<CompPowerTrader>();
            return comp is { PowerOn: true };
        }
    }

    public void Action()
    {
        ChangeState();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref openInt, "open", true);
    }

    public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
    {
        def = ThingDefOf.RemoteEmbrasure_Close;
        base.Destroy(mode);
    }

    private void RefreshGraphic()
    {
        var fieldInfo = AccessTools.Field(typeof(Thing), "graphicInt");
        fieldInfo.SetValue(this, null);
        _ = DefaultGraphic;
    }

    private void CloseEmbrasure()
    {
        openInt = false;
        def = ThingDefOf.RemoteEmbrasure_Close;
    }

    private void OpenEmbrasure()
    {
        openInt = true;
        def = ThingDefOf.RemoteEmbrasure_Open;
    }

    private void ChangeState()
    {
        if (!IsPowered)
        {
            return;
        }

        if (openInt)
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