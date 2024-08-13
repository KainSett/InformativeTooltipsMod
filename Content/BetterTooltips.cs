using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using System;
using Terraria.GameInput;
using System.Linq;
using InformativeTooltips.Common.Configs;
namespace InformativeTooltips.Content.BetterTooltips
{
    public class Food : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && (entity.buffType == BuffID.WellFed || entity.buffType == BuffID.WellFed2 || entity.buffType == BuffID.WellFed3);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            tooltips.Insert(2, SHIFTINFO);
            if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
            {
                string tip = "";
                if (item.buffType == BuffID.WellFed) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 1, 2, 5, 5, 0.5, 20, 5); }
                if (item.buffType == BuffID.WellFed2) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 2, 3, 7.5, 7.5, 0.75, 30, 10); }
                if (item.buffType == BuffID.WellFed3) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 3, 4, 10, 10, 1, 40, 15); }
                tooltips.RemoveAt(2);
                var Prov = new TooltipLine(Mod, "FoodBuff", Language.GetTextValue("Mods.InformativeTooltips.Special.provides"));
                Prov.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Insert(2, Prov);
                tooltips.Insert(3, new(Mod, "WellFedTooltip", tip));
                if (Main.expertMode)
                {
                    tooltips.Insert(tooltips.Count - 1, new TooltipLine(Mod, "FoodExpert+", Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Expert")));
                }
                if (Main.dontStarveWorld) { tooltips.Insert(tooltips.Count - 1, new TooltipLine(Mod, "FoodHunger", Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Hunger"))); }
                tooltips.RemoveAt(tooltips.Count - 3);
                tooltips.RemoveAt(tooltips.Count - 4);
                tooltips.RemoveAt(tooltips.Count - 1);
                tooltips.RemoveAt(tooltips.Count - 2);
                tooltips.RemoveAt(1);
                tooltips.RemoveAt(0);
            }
        }
    }
    public class WellFed : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && (type == BuffID.WellFed || type == BuffID.WellFed2 || type == BuffID.WellFed3))
            {
                var SHIFTINFO = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
                tip = $"{tip}\n{SHIFTINFO}";
                if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    if (type == BuffID.WellFed) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 1, 2, 5, 5, 0.5, 20, 5); }
                    if (type == BuffID.WellFed2) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 2, 3, 7.5, 7.5, 0.75, 30, 10); }
                    if (type == BuffID.WellFed3) { tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.WellFed.Tooltip"), 3, 4, 10, 10, 1, 40, 15); }
                }
            }
        }
    }
    public class CampFire : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && item.createTile == TileID.Campfire;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.RemoveAt(2);
            tooltips.Add(new(Mod, "CampfireTooltip", Language.GetTextValue("Mods.InformativeTooltips.Items.PlacedNearby.Tooltip")));
            tooltips.Add(new(Mod, "CampfireTooltip2", Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip")));
            tooltips.Add(new(Mod, "CampfireTooltip3", string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenMult.Tooltip"), 1.1)));
        }
    }
    public class CampFireBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && type == BuffID.Campfire) { tip = Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip") + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenMult.Tooltip"), 1.1); }
        }
    }
    public class CatBast : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.CatBast;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //Main.NewText($"{item.}");
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true)
            {
                int index = tooltips.FindIndex(tip => tip.Name == "Tooltip0");
                tooltips[index].Text = Language.GetTextValue("Mods.InformativeTooltips.Special.grantsbuff");
                var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
                SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
                {
                    tooltips.RemoveAt(--index);
                    tooltips.RemoveAt(index);
                    var PlacedTooltip = new TooltipLine(Mod, "PlacedNearbyTooltip", Language.GetTextValue("Mods.InformativeTooltips.Items.PlacedNearby.Tooltip"));
                    PlacedTooltip.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                    tooltips[tooltips.FindIndex(line => line.Name == "ItemName")].OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                    tooltips.Insert(index, PlacedTooltip);
                    tooltips.Insert(++index, new(Mod, "BastDef", string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.DefenseInc.Tooltip"), 5)));
                }
                else { tooltips.Insert(++index, SHIFTINFO); }
            }
        }
    }
    public class CatBastBuff : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            if (ModContent.GetInstance<ArmorDetailedConfig>().BuffDetailsToggle == true && type == BuffID.CatBast)
            {
                tip = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.DefenseInc.Tooltip"), 5);
            }
        }
    }
    public class BoC : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.BrainOfConfusion;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.RemoveAt(2);
            tooltips.RemoveAt(2);
            tooltips.RemoveAt(2);
            tooltips.Insert(2, new TooltipLine(Mod, "BOC1", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.basetooltip1")));
            tooltips.Insert(3, new TooltipLine(Mod, "BOC2", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.basetooltip2")));
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", Language.GetTextValue("Mods.InformativeTooltips.Special.shift"));
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }

            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Prov = new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip2"));
                Prov.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                var dodgechance = new TooltipLine(Mod, "BOC3", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip1"));
                dodgechance.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                tooltips.Add(dodgechance);
                tooltips.Add(Prov);
                tooltips.Add(new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip3")));
                tooltips.Add(new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip4")));
                tooltips.Add(new TooltipLine(Mod, "BOC4", Language.GetTextValue("Mods.InformativeTooltips.Individual.BoC.Tooltip5")));
            }
        }
    }
    public class AegisFruit : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.AegisFruit;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips[2].Text = $"{tooltips[2].Text} {Language.GetTextValue("Mods.InformativeTooltips.Items.AegisVanilla")}";
        }
    }
    public class AegisCrystal : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.AegisCrystal;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips[2].Text = $"{tooltips[2].Text.Remove(tooltips[2].Text.IndexOf(' '))} {string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenRampRate.Tooltip").ToLowerInvariant(), 20)}";
        }
    }
    public class AnkhS : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return (entity.type == ItemID.AnkhShield || entity.type == ItemID.AnkhCharm);
        }
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }
            tooltips.Insert(++index, new TooltipLine(Mod, "Immunities", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.basetooltip")));
            tooltips.RemoveAt(++index);
            if (item.type == ItemID.AnkhShield) { tooltips.RemoveAt(index); }
            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Provides = new TooltipLine(Mod, "ProvidesTo", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.providesto"));
                Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Add(Provides);
                if (item.type == ItemID.AnkhShield) { tooltips.Add(new(Mod, "AnkhList", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.shieldonly"))); }
                tooltips.Add(new(Mod, "AnkhList", Language.GetTextValue("Mods.InformativeTooltips.Individual.Ankh.list")));
            }
        }
    }
    public class MoonCharm : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.MoonCharm;
        }
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }
            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Provides = new TooltipLine(Mod, "NightTimeOnly", Language.GetTextValue("Mods.InformativeTooltips.Special.night"));
                Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Add(Provides);
                tooltips.Add(new(Mod, "MoonCharm", string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.Werewolf.Tooltip"), 3, 2, 5.1, 5.1, 5)));
                tooltips.Add(new(Mod, "RegenSlight", Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip")));
            }
        }
    }
    public class Rakushka : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.NeptunesShell;
        }
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
            SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
            int index = tooltips.FindIndex(line => line.Name == "Material");
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
            if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
            if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Insert(++index, SHIFTINFO);
            }
            tooltips.Insert(++index, new TooltipLine(Mod, "MerefolkWater", Language.GetTextValue("Mods.InformativeTooltips.Special.merefolk")));
            tooltips.RemoveAt(++index);
            if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
            {
                tooltips.Clear();
                var Provides = new TooltipLine(Mod, "Underwater", Language.GetTextValue("Mods.InformativeTooltips.Special.underwater"));
                Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                tooltips.Add(Provides);
                tooltips.Add(new(Mod, "rakushka", Language.GetTextValue("Mods.InformativeTooltips.Buffs.Merefolk.Tooltip")));
            }
        }
    }
    public class CShellTooltipPlayer : ModPlayer
    {
        public int Current = Math.Clamp(1, 1, 3);
        public bool PrevState = false;
        public int scrolled = Math.Clamp(0, -5, 5);
        public override void ResetEffects()
        {
            if (!Main.keyState.IsKeyDown(Keys.LeftShift) && !Main.keyState.IsKeyDown(Keys.RightShift))
            {
                Current = 1;
                PrevState = false;
                scrolled = 0;
            }
        }
    }
    public class CShell : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return (item.type == ItemID.CelestialShell || item.type == ItemID.MoonStone || item.type == ItemID.SunStone || item.type == ItemID.CelestialStone || item.type == ItemID.MoonShell);
        }
        public string incstats = Language.GetTextValue("Mods.InformativeTooltips.Special.incstats");
        public string shift = Language.GetTextValue("Mods.InformativeTooltips.Special.shift");
        public string night = Language.GetTextValue("Mods.InformativeTooltips.Special.night");
        public string day = Language.GetTextValue("Mods.InformativeTooltips.Special.day");
        public string provides = Language.GetTextValue("Mods.InformativeTooltips.Special.provides");
        public string regen = Language.GetTextValue("Mods.InformativeTooltips.Buffs.Regen1.Tooltip");
        public string slightregen = Language.GetTextValue("Mods.InformativeTooltips.Buffs.RegenSlight.Tooltip");
        public string moveinfo = Language.GetTextValue("Mods.InformativeTooltips.Special.moveinfo");
        public string werewolf = Language.GetTextValue("Mods.InformativeTooltips.Special.werewolf");
        public string merefolk = Language.GetTextValue("Mods.InformativeTooltips.Special.merefolk");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!item.social)
            {
                if ((Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift)) && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
                {
                    tooltips.Clear();
                    if (item.type != ItemID.CelestialShell && item.type != ItemID.MoonShell)
                    {
                        if (item.type == ItemID.MoonStone)
                        {
                            var Provides = new TooltipLine(Mod, "NightTimeOnly", night);
                            Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                            tooltips.Add(Provides);
                        }
                        if (item.type == ItemID.SunStone)
                        {
                            var Provides = new TooltipLine(Mod, "DayTimeOnly", day);
                            Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                            tooltips.Add(Provides);
                        }
                        if (item.type == ItemID.CelestialStone)
                        {
                            var Provides = new TooltipLine(Mod, "ItemEffects", provides);
                            Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                            tooltips.Add(Provides);
                        }
                        tooltips.Add(new(Mod, "MoonStats", string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.MoonStone.Tooltip"), 4, 2, 10, 10, 0.5, 15)));
                        tooltips.Add(new(Mod, "IncreasedRegen", regen));
                    }
                    else
                    {
                        tooltips.Clear();
                        if (PlayerInput.ScrollWheelDelta > 0) { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().scrolled++; }
                        else if (PlayerInput.ScrollWheelDelta < 0) { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().scrolled--; }


                        int y = 1;
                        if (item.type == ItemID.MoonShell) 
                        { 
                            y = 2; 
                            if (Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current == 1) { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current = 2; }
                        }
                        if (Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().scrolled >= 5 && !Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().PrevState)
                        {
                            Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().scrolled = 0;
                            if (Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current != y) { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current -= 1; }
                            else { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current = 3; }
                            Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().PrevState = true;
                        }
                        else if (Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().scrolled <= -5 && !Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().PrevState)
                        {
                            Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().scrolled = 0;
                            if (Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current != 3) { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current += 1; }
                            else { Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current = y; }
                            Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().PrevState = true;
                        }
                        if (PlayerInput.ScrollWheelDelta == 0)
                        {
                            Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().PrevState = false;
                        }
                        int CurrentLine = Main.LocalPlayer.GetModPlayer<CShellTooltipPlayer>().Current;
                        var moveinf = new TooltipLine(Mod, "MoveWithArrows", moveinfo);
                        moveinf.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                        tooltips.Add(moveinf);
                        var Provides = new TooltipLine(Mod, "ProvidesCShell", provides);
                        Provides.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().PositiveColor;
                        tooltips.Add(Provides);
                        var WerewolfLine = new TooltipLine(Mod, "WerewolfLine", werewolf);
                        WerewolfLine.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                        var MerefolkLine = new TooltipLine(Mod, "MerefolkLine", merefolk);
                        MerefolkLine.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                        var MoonStoneLine = new TooltipLine(Mod, "MoonStoneLine", incstats);
                        MoonStoneLine.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;

                        if (CurrentLine == 2)
                        {
                            string Werewolf = "During nighttime:" + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.Werewolf.Tooltip"), 3, 2, 5.1, 5.1, 5);
                            var WerewolfTooltip = new TooltipLine(Mod, "WerewolfStats", Werewolf);
                            if (item.type == ItemID.CelestialShell) { tooltips.Add(MoonStoneLine); }
                            tooltips.Add(WerewolfTooltip);
                            tooltips.Add(new(Mod, "SlightRegen", slightregen));
                            tooltips.Add(MerefolkLine);
                        }
                        if (CurrentLine == 3)
                        {
                            string Merefolk = "While submerged in liquid:" + "\n" + Language.GetTextValue("Mods.InformativeTooltips.Buffs.Merefolk.Tooltip");
                            var MerefolkTooltip = new TooltipLine(Mod, "MerefolkStats", Merefolk);
                            if (item.type == ItemID.CelestialShell) { tooltips.Add(MoonStoneLine); }
                            tooltips.Add(WerewolfLine);
                            tooltips.Add(MerefolkTooltip);
                        }
                        if (CurrentLine == 1)
                        {
                            string MoonStone = "Always:" + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.MoonStone.Tooltip"), 4, 2, 10, 10, 0.5, 15);
                            var MoonStoneTooltip = new TooltipLine(Mod, "MoonStoneStats", MoonStone);
                            if (item.type == ItemID.CelestialShell)
                            {
                                tooltips.Add(MoonStoneTooltip);
                                tooltips.Add(new(Mod, "IncreasedRegen", regen));
                            }
                            tooltips.Add(WerewolfLine);
                            tooltips.Add(MerefolkLine);
                        }
                    }
                }
                else
                {
                    var SHIFTINFO = new TooltipLine(Mod, "HideDescription", shift);
                    SHIFTINFO.OverrideColor = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                    int index = tooltips.FindIndex(line => line.Name == "Material");
                    if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Equipable"); }
                    if (index == -1) { index = tooltips.FindIndex(line => line.Name == "Defense") - 2; }
                    if (!item.social && index != -1 && ModContent.GetInstance<ArmorDetailedConfig>().AccessoryStatsToggle == true)
                    {
                        tooltips.Insert(++index, SHIFTINFO);
                    }

                    if (item.type == ItemID.CelestialShell || item.type == ItemID.MoonShell)
                    {
                        var MoonStoneLine = new TooltipLine(Mod, "MoonStoneLine", incstats);
                        var WerewolfLine = new TooltipLine(Mod, "WerewolfLine", werewolf);
                        var MerefolkLine = new TooltipLine(Mod, "MerefolkLine", merefolk);


                        if (item.type == ItemID.CelestialShell) { tooltips.Insert(++index, MoonStoneLine); }
                        tooltips.Insert(++index, WerewolfLine);
                        tooltips.Insert(++index, MerefolkLine);
                        if (item.type == ItemID.CelestialShell)
                        {
                            tooltips.RemoveAt(++index);
                            tooltips.RemoveAt(++index);
                            tooltips.RemoveAt(++index);
                            tooltips.RemoveAt(--index);
                            tooltips.RemoveAt(--index);
                        }
                        else
                        {
                            tooltips.RemoveAt(++index);
                        }
                    }
                    else
                    {
                        if (item.type == ItemID.MoonStone)
                        {
                            var MoonStoneLine = new TooltipLine(Mod, "MoonStoneLine", $"{night.Replace(':', ',')} {incstats.ToLower()}");
                            tooltips.Insert(++index, MoonStoneLine);
                        }
                        else if (item.type == ItemID.SunStone)
                        {
                            var SunStoneLine = new TooltipLine(Mod, "SunStoneLine", $"{day.Replace(':', ',')} {incstats.ToLower()}");
                            tooltips.Insert(++index, SunStoneLine);
                        }
                        else
                        {
                            var CStoneLine = new TooltipLine(Mod, "CStoneLine", incstats);
                            tooltips.Insert(++index, CStoneLine);
                        }
                        tooltips.RemoveAt(++index);
                        tooltips.RemoveAt(++index);
                        tooltips.RemoveAt(--index);
                    }
                } 
            }
        }
    }
}