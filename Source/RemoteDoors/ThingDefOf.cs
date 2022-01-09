using RimWorld;
using Verse;

namespace RemoteDoors;

[DefOf]
internal class ThingDefOf
{
    public static ThingDef RemoteEmbrasure_Close;

    public static ThingDef RemoteEmbrasure_Open;

    static ThingDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
    }
}