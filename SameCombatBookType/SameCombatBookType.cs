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

        [HarmonyPrefix, HarmonyPatch(typeof(ItemDomain), "AddElement_SkillBooks", new Type[] { typeof(int) ,typeof(SkillBook) }, new ArgumentType[] { ArgumentType.Normal , ArgumentType.Normal })]
        public static bool prefixAddElementSkillBooks(ref ItemDomain __instance, int objectId, SkillBook instance)
        {
            if (instance.IsCombatSkillBook())
            {
                AdaptableLog.Info("patch pre AddElement_SkillBooks : " + Convert.ToString(objectId));
                AdaptableLog.Info("patch pre AddElement_SkillBooks :" + Convert.ToString(instance.GetCombatSkillType()));
            }
            return true;
        }
    }
}
