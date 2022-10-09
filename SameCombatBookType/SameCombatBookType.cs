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
        private static int BookOneType;
        private static int BookTwoType;
        private static int BookThreeType;
        private static int BookFourType;
        private static int BookFiveType;
        private static int BookGetType;
        private static ushort[] BookTypeArray = new ushort[5];

        public override void Initialize()
        {
            harmony = Harmony.CreateAndPatchAll(typeof(SameCombatBookType));
            AdaptableLog.Info("Initialize  SameCombatBookType");
        }

        public override void OnModSettingUpdate()
        {
            AdaptableLog.Info("SameCombatBookType OnModSettingUpdate");
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookIndexType", ref SameCombatBookType.BookIndexType);
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookOneType", ref SameCombatBookType.BookOneType);
            BookTypeArray[0] = (ushort)BookOneType;
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookTwoType", ref SameCombatBookType.BookTwoType);
            BookTypeArray[1] = ((ushort)BookTwoType);
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookThreeType", ref SameCombatBookType.BookThreeType);
            BookTypeArray[2]=((ushort)BookThreeType);
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookFourType", ref SameCombatBookType.BookFourType);
            BookTypeArray[3] = ((ushort)BookFourType);
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookFiveType", ref SameCombatBookType.BookFiveType);
            BookTypeArray[4] = ((ushort)BookFiveType);
            DomainManager.Mod.GetSetting(base.ModIdStr, "BookGetType", ref SameCombatBookType.BookGetType);
            AdaptableLog.Info(string.Format("SameCombatBookType setting : \n BookIndexType :{0} \n BookOneType : {1} \n ", BookIndexType, BookOneType));
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
                Inventory newGetBook = new Inventory();
                foreach ((ItemKey item, int i) in getBook.Items)
                {
                    if (item.ItemType == 10)
                    {
                        AdaptableLog.Info("   ---   " + Convert.ToString(item.TemplateId));
                        AdaptableLog.Info("   ---   " + Convert.ToString(item.Id));
                        SkillBook element_SkillBooks = DomainManager.Item.GetElement_SkillBooks(item.Id);
                        AdaptableLog.Info("   ---   " + Convert.ToString(element_SkillBooks.GetName()));
                        if (element_SkillBooks.IsCombatSkillBook())
                        {
                            /*
                            // 残缺状态
                            ushort num = 0;
                            ushort num2 = 4;
                            for (int j = 0; j < 5; j++)
                            {
                                num |= ((ushort)(BookTypeArray[j] * num2));
                                num2 *= 4;
                            }
                            AdaptableLog.Info("   ---  num  " + Convert.ToString(num));
                           
                            element_SkillBooks.SetPageIncompleteState(num, context);
                            */
                            element_SkillBooks.SetPageIncompleteState(0, context);
                            // 耐久
                            //element_SkillBooks.SetMaxDurability(30, context);
                            //element_SkillBooks.SetCurrDurability(30, context);
                            byte b = (byte)BookIndexType;
                            byte b2 = 4;
                            for (int k = 0; k < 5; k++)
                            {
                                b2 *= 2;
                                bool flag = BookTypeArray[k] == 1;
                                if (flag)
                                {
                                    b |= b2;
                                }
                            }
                            // 总纲
                            Traverse.Create(element_SkillBooks).Field("_pageTypes").SetValue((byte)b);
                            Character element_Objects = DomainManager.Character.GetElement_Objects(DomainManager.Taiwu.GetTaiwuCharId());
                            element_Objects.AddInventoryItem(context, item, 1);
                        }
                        AdaptableLog.Info("  ===================  ");
                    }
                    else
                    {
                        // 将非功法书单独存一份
                        newGetBook.OfflineAdd(item, i);
                    }
                }
                // 1为替换原本
                if (BookGetType == 1)
                {
                    getBook = newGetBook;
                }
            }
            return true;
        }

    }
}
