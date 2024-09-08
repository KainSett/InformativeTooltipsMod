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
using InformativeTooltips.Content.BetterTooltips;
using InformativeTooltips.Content.GlobalTooltips;

namespace InformativeTooltips.Content.SetBonuses
{
    public class VanillaSetBonusTooltipBase : GlobalTooltipsBase
    {
        public VanillaSetBonusTooltipBase() : base(4) { }
        public string moveinfo = Language.GetTextValue("Mods.InformativeTooltips.Special.moveinfo");
        public override bool InstancePerEntity => true;
        public virtual string SetText { get; set; }
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return SetText != null;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.social) { return; }
            if (Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift))
            {
                ScrollTooltip.max = 2;
                var neutral = ModContent.GetInstance<ArmorDetailedConfig>().NeutralColor;
                var blue = ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor;
                var moveinf = new TooltipLine(Mod, "MoveWithArrows", moveinfo) { OverrideColor = blue };
                var stats = new TooltipLine(Mod, "StatsHidden", "Stats...") { OverrideColor = neutral };
                var set = new TooltipLine(Mod, "SetBonusHidden", "Set bonus...") { OverrideColor = neutral };
                var savedTooltips = new List<TooltipLine>();
                foreach (var line in tooltips)
                {
                    if (line.Name.Contains("Tooltip"))
                    {
                        savedTooltips.Add(line);
                    }
                    line.Hide();
                }
                    tooltips.Add(moveinf);
                if (ScrollTooltip.Current == 1)
                {
                    foreach (var line in savedTooltips) { tooltips.Add(line); }
                    tooltips.Add(set);
                }
                else
                {
                    tooltips.Add(stats);
                    var SetBonus = new TooltipLine(Mod, "SetBonus", SetText);
                    tooltips.Add(SetBonus);
                }

            }
        }
    }
    public class CrimsonSet : VanillaSetBonusTooltipBase
    {

        public override string SetText { get; set; } = string.Format(Language.GetTextValue("Mods.InformativeTooltips.Items.SetBonus"), ModContent.GetInstance<ArmorDetailedConfig>().HeaderColor.Hex3()) + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenRate"), 100) + "\n" + string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenMult"), 1.5);
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.CrimsonHelmet || entity.type == ItemID.CrimsonScalemail || entity.type == ItemID.CrimsonGreaves;
        }
    }
}