using GameData.ArchiveData;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.Merchant;
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
        private static int BookIndexType;

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(SameCombatBookType));
            AdaptableLog.Info("Initialize  SameCombatBookType");
        }

        public override void OnModSettingUpdate()
        {
            AdaptableLog.Info("SameCombatBookType OnModSettingUpdate");
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookIndexType", ref SameCombatBookType.BookIndexType);
            AdaptableLog.Info(string.Format("SameCombatBookType setting : \n BookIndexType :{0} \n ", BookIndexType));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(MerchantDomain), "ExchangeBook")]
        public unsafe static bool prefixExchangeBook(ref MerchantDomain __instance, DataContext context, int npcId, Inventory getBook, int selfAuthority, int npcAuthority)
        {
            AdaptableLog.Info("ExchangeBook");
            if (getBook == null)
            {
                return true;
            }
            else
            {
                AdaptableLog.Info("prefixExchangeBook : " + Convert.ToString(npcId));
                AdaptableLog.Info("prefixExchangeBook : " + Convert.ToString(selfAuthority));
                AdaptableLog.Info("prefixExchangeBook : " + Convert.ToString(npcAuthority));
                Inventory newGetBook = new Inventory();
                foreach ((ItemKey item, int i) in getBook.Items)
                {
                    if (item.ItemType == 10)
                    {
                        AdaptableLog.Info("   ---   " + Convert.ToString(item.ItemType));
                        AdaptableLog.Info("   ---   " + Convert.ToString(item.TemplateId));
                        AdaptableLog.Info("   ---   " + Convert.ToString(item.Id));
                        SkillBook element_SkillBooks = DomainManager.Item.GetElement_SkillBooks(item.Id);
                        AdaptableLog.Info("   ---   " + Convert.ToString(element_SkillBooks.GetName()));
                        if (element_SkillBooks.IsCombatSkillBook())
                        {
                            // 残缺状态
                            element_SkillBooks.SetPageIncompleteState((ushort)0, context);
                            // 耐久
                            //element_SkillBooks.SetMaxDurability(25, context);
                            //element_SkillBooks.SetCurrDurability(25, context);
                            // 总纲
                            Traverse.Create(element_SkillBooks).Field("_pageTypes").SetValue((byte)BookIndexType);
                            //element_SkillBooks.SetModificationState(())
                            // element_SkillBooks.InvalidateSelfAndInfluencedCache(5, context);
                            //bool isArchive = element_SkillBooks.CollectionHelperData.IsArchive;
                            /*byte b = 1;
                            if (isArchive)
                            {
                                byte* ptr = OperationAdder.DynamicObjectCollection_SetFixedField<int>(element_SkillBooks.CollectionHelperData.DomainId, element_SkillBooks.CollectionHelperData.DataId, item.Id, 11U, 1);
                                *ptr = b;
                                ptr++;
                            }*/
                            Character element_Objects = DomainManager.Character.GetElement_Objects(DomainManager.Taiwu.GetTaiwuCharId());
                            element_Objects.AddInventoryItem(context, item, 1);
                        }
                        AdaptableLog.Info("  ===================  ");
                    }
                    else
                    {
                        newGetBook.OfflineAdd(item, i);
                    }
                }
                getBook = newGetBook;
            }
            return true;
        }

    }
}
