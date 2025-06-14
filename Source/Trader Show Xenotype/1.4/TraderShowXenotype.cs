using HarmonyLib;
using RimWorld;
using Verse;

namespace TraderShowXenotype;

[StaticConstructorOnStartup]
public static class TraderShowXenotype
{
    static TraderShowXenotype()
    {
        Harmony harmony = new("NachoToast.TraderShowXenotype");

        harmony.Patch(
            original: AccessTools.PropertyGetter(
                type: typeof(Tradeable_Pawn),
                name: nameof(Tradeable_Pawn.Label)),
            postfix: new HarmonyMethod(
                methodType: typeof(TraderShowXenotype),
                methodName: nameof(TradeablePawnLabel_Postfix)));
    }

    private static void TradeablePawnLabel_Postfix(Tradeable_Pawn __instance, ref string __result)
    {
        if (__instance?.AnyThing is not Pawn pawn)
        {
            return;
        }

        string label = pawn.genes?.Xenotype?.label;

        if (label.NullOrEmpty())
        {
            return;
        }

        if (label == XenotypeDefOf.Baseliner.label)
        {
            return;
        }

        // e.g. "male, 41" -> ["male", " 41"]
        string[] list = __result.Split(',');

        // e.g. becomes ["male yttakin", " 41"]
        list[1] += " " + label;

        // and joined back together, e.g. "male yttakin, 41"
        __result = string.Join(",", list);
    }
}
