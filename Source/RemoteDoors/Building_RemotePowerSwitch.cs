using RimWorld;

namespace RemoteDoors;

public class Building_RemotePowerSwitch : Building_PowerSwitch, IRemoteTargetable
{
    public void Action()
    {
        var comp = GetComp<CompFlickable>();
        comp.DoFlick();
    }
}