using GameData.Domains;
using GameData.Domains.Item;
using GameData.Utilities;
using HarmonyLib;
using System;
using TaiwuModdingLib.Core.Plugin;

namespace SameCombatBookType
{
    [PluginConfig("SameCombatBookType", "Naced", "1.0.0")]
    public class SameCombatBookType : TaiwuRemakePlugin
    {
        Harmony harmony;

        public override void Dispose()
        {
            if (harmony != null)
            {
                harmony.UnpatchSelf();
            }

        }

        // 总纲
        private static int IndexType;

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(SameCombatBookType));
            AdaptableLog.Info("Initialize  SameCombatBookType");
        }

        public override void OnModSettingUpdate()
        {
            AdaptableLog.Info("SameCombatBookType OnModSettingUpdate");
            DomainManager.Mod.GetSetting(base.ModIdStr, "SameMorality", ref SameCombatBookType.IndexType);
            AdaptableLog.Info(string.Format("SameCombatBookType setting : \n IndexType :{0} \n ", IndexType));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ItemDomain), "GetElement_SkillBooks", new Type[] { typeof(int) }, new ArgumentType[] { ArgumentType.Normal })]
        public static void post(ref ItemDomain __instance, int objectId)
        {
            AdaptableLog.Info("patch GetElement_SkillBooks " + Convert.ToString(objectId));
        }
    }
}
