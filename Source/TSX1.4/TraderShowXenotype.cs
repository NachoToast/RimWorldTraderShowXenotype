using HarmonyLib;
using RimWorld;
using Verse;

namespace TraderShowXenotype
{
    [StaticConstructorOnStartup]
    public class TraderShowXenotype
    {
        static TraderShowXenotype()
        {
            Harmony harmony = new Harmony("NachoToast.TraderShowXenotype");

            harmony.Patch(AccessTools.PropertyGetter(typeof(Tradeable_Pawn), nameof(Tradeable_Pawn.Label)),
                    postfix: new HarmonyMethod(typeof(TraderShowXenotype), nameof(TradeablePawnLabel_Postfix)));

        }
        private static void TradeablePawnLabel_Postfix(Tradeable_Pawn __instance, ref string __result)
        {
            Pawn pawn = (Pawn)__instance.AnyThing;
            if (pawn?.genes?.Xenotype?.label != null && pawn.genes.Xenotype.label != XenotypeDefOf.Baseliner.label)
            {
                string[] list = __result.Split(',');
                list[1] += " " + pawn.genes.Xenotype.label;
                __result = string.Join(",", list);
            }
        }
    }
}
