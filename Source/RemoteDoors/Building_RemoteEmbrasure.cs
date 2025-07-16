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
        changeState();
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

    private void refreshGraphic()
    {
        var fieldInfo = AccessTools.Field(typeof(Thing), "graphicInt");
        fieldInfo.SetValue(this, null);
        _ = DefaultGraphic;
    }

    private void closeEmbrasure()
    {
        openInt = false;
        def = ThingDefOf.RemoteEmbrasure_Close;
    }

    private void openEmbrasure()
    {
        openInt = true;
        def = ThingDefOf.RemoteEmbrasure_Open;
    }

    private void changeState()
    {
        if (!IsPowered)
        {
            return;
        }

        if (openInt)
        {
            closeEmbrasure();
        }
        else
        {
            openEmbrasure();
        }

        refreshGraphic();
        DirtyMapMesh(Map);
    }
}