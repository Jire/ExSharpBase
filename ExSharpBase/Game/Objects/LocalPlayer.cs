using System.Collections.Generic;
using System.Linq;
using ExSharpBase.API;
using ExSharpBase.Game.Spells;
using ExSharpBase.Modules;
using ExSharpBase.Overlay.Drawing;
using Newtonsoft.Json.Linq;
using SharpDX;

namespace ExSharpBase.Game.Objects
{
    class LocalPlayer
    {
        public static JObject UnitRadiusData;

        private static List<string> RangeSlotList = new List<string> {"Q", "W", "E", "R"};
        private static List<float> UsedRangeSlotsList = new List<float>();

        public static string GetSummonerName()
        {
            return ActivePlayerData.GetSummonerName();
        }

        public static int GetLevel()
        {
            return ActivePlayerData.GetLevel();
        }

        public static float GetCurrentGold()
        {
            return ActivePlayerData.GetCurrentGold();
        }

        public static void DrawAttackRange(Color Colour, float Thickness)
        {
            if (IsVisible() && GetCurrentHealth() > 1.0f)
            {
                DrawFactory.DrawCircleRange(GetPosition(), GetBoundingRadius() + GetAttackRange(),
                    Colour, Thickness);
            }
        }

        public static void DrawSpellRange(SpellBook.SpellSlot Slot, Color Colour, float Thickness)
        {
            if (IsVisible() && GetCurrentHealth() > 1.0f)
            {
                DrawFactory.DrawCircleRange(GetPosition(), SpellBook.GetSpellRadius(Slot),
                    Colour, Thickness);
            }
        }

        public static void DrawAllSpellRange(Color RGB)
        {
            foreach (string RangeSlot in RangeSlotList)
            {
                float SpellRange = SpellBook.SpellDB[RangeSlot].ToObject<JObject>()["Range"][0]
                    .ToObject<float>();

                if (UsedRangeSlotsList.Count != 0)
                {
                    if (!UsedRangeSlotsList.Contains(SpellRange))
                    {
                        UsedRangeSlotsList.Add(SpellRange);
                    }
                }
                else
                {
                    UsedRangeSlotsList.Add(SpellRange);
                }
            }

            foreach (float Range in UsedRangeSlotsList)
            {
                DrawFactory.DrawCircleRange(GetPosition(), Range, RGB, 2.5f);
            }
        }

        public static bool IsVisible()
        {
            return Memory.Read<bool>(Engine.GetLocalPlayer + OffsetManager.Object.VISIBILITY);
        }

        public static Vector3 GetPosition()
        {
            var posX = Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.POS);
            var posY = Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.POS + 0x4);
            var posZ = Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.POS + 0x8);

            return new Vector3 {X = posX, Y = posY, Z = posZ};
        }

        public static string GetChampionName()
        {
            return AllPlayerData.AllPlayers.First(x => x.SummonerName == GetSummonerName()).ChampionName;
        }

        public static float GetAttackRange()
        {
            return Memory.Read<float>(Engine.GetLocalPlayer + OffsetManager.Object.ATTACK_RANGE);
        }

        public static int GetBoundingRadius()
        {
            return int.Parse(UnitRadiusData[GetChampionName()]?["Gameplay radius"]?.ToString() ?? string.Empty);
        }

        public static float GetCurrentHealth()
        {
            return ActivePlayerData.ChampionStats.GetCurrentHealth();
        }

        public static float GetMaxHealth()
        {
            return ActivePlayerData.ChampionStats.GetMaxHealth();
        }

        public static float GetHealthRegenRate()
        {
            return ActivePlayerData.ChampionStats.GetHealthRegenRate();
        }

        public string GetResourceType()
        {
            return ActivePlayerData.ChampionStats.GetResourceType();
        }

        public static float GetCurrentMana()
        {
            return ActivePlayerData.ChampionStats.GetResourceValue();
        }

        public static float GetCurrentManaMax()
        {
            return ActivePlayerData.ChampionStats.GetResourceMax();
        }

        public static float GetAbilityPower()
        {
            return ActivePlayerData.ChampionStats.GetAbilityPower();
        }

        public static float GetArmor()
        {
            return ActivePlayerData.ChampionStats.GetArmor();
        }

        public static float GetArmorPenetrationFlat()
        {
            return ActivePlayerData.ChampionStats.GetArmorPenetrationFlat();
        }

        public static float GetArmorPenetrationPercent()
        {
            return ActivePlayerData.ChampionStats.GetArmorPenetrationPercent();
        }

        public static float GetAttackSpeed()
        {
            return ActivePlayerData.ChampionStats.GetAttackSpeed();
        }

        public static float GetBonusArmorPenetrationPercent()
        {
            return ActivePlayerData.ChampionStats.GetBonusArmorPenetrationPercent();
        }

        public static float GetBonusMagicPenetrationPercent()
        {
            return ActivePlayerData.ChampionStats.GetBonusMagicPenetrationPercent();
        }

        public static float GetCooldownReduction()
        {
            return ActivePlayerData.ChampionStats.GetCooldownReduction();
        }

        public static float GetCritChance()
        {
            return ActivePlayerData.ChampionStats.GetCritChance();
        }

        public static float GetCritDamage()
        {
            return ActivePlayerData.ChampionStats.GetCritDamage();
        }

        public static float GetLifeSteal()
        {
            return ActivePlayerData.ChampionStats.GetLifeSteal();
        }

        public static float GetMagicLethality()
        {
            return ActivePlayerData.ChampionStats.GetMagicLethality();
        }

        public static float GetMagicPenetrationFlat()
        {
            return ActivePlayerData.ChampionStats.GetMagicPenetrationFlat();
        }

        public static float GetMagicPenetrationPercent()
        {
            return ActivePlayerData.ChampionStats.GetMagicPenetrationPercent();
        }

        public static float GetMagicResist()
        {
            return ActivePlayerData.ChampionStats.GetMagicResist();
        }

        public static float GetMoveSpeed()
        {
            return ActivePlayerData.ChampionStats.GetMoveSpeed();
        }

        public static float GetPhysicalLethality()
        {
            return ActivePlayerData.ChampionStats.GetPhysicalLethality();
        }

        public static float GetSpellVamp()
        {
            return ActivePlayerData.ChampionStats.GetSpellVamp();
        }

        public static float GetTenacity()
        {
            return ActivePlayerData.ChampionStats.GetTenacity();
        }
    }
}