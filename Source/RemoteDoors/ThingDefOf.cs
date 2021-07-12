using RimWorld;
using Verse;

namespace RemoteDoors
{
    // Token: 0x02000006 RID: 6
    [DefOf]
    internal class ThingDefOf
    {
        // Token: 0x04000002 RID: 2
        public static ThingDef RemoteEmbrasure_Close;

        // Token: 0x04000003 RID: 3
        public static ThingDef RemoteEmbrasure_Open;

        // Token: 0x06000019 RID: 25 RVA: 0x000023D7 File Offset: 0x000005D7
        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }
    }
}