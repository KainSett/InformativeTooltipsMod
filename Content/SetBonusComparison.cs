using InformativeTooltips.GlobalTooltips;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace InformativeTooltips.Content.SetBonusComparison
{
    public class SetBonusTooltips : GlobalTooltipsBase
    {
        public SetBonusTooltips() : base(4) { }
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return !item.social && !item.accessory && !item.vanity;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            List<TooltipLine> modified = !item.IsAir ? GetTooltipLines(item) : null;
            foreach (var line in modified)
            {
                 Main.NewText($"{line.Text}");
            }
        }
        public static List<TooltipLine> GetTooltipLines(Item item)
        {
            // Pulled from ItemFilters.FitsFilter

            // Have to reinitialize because ItemLoader.ModifyTooltips resizes the arrays
            string[] toolTipNames = new string[30];
            string[] toolTipLines = new string[30];
            bool[] unusedPrefixLine = new bool[30];
            bool[] unusedBadPrefixLines = new bool[30];
            int unusedYoyoLogo = 0;
            int unusedResearchLine = 0;

            int numLines = 1;
            float knockBack = item.knockBack;
            Main.MouseText_DrawItemTooltip_GetLinesInfo(item, ref unusedYoyoLogo, ref unusedResearchLine, knockBack, ref numLines, toolTipLines, unusedPrefixLine, unusedBadPrefixLines, toolTipNames, out _);
            var modifiedTooltipLines = ItemLoader.ModifyTooltips(item, ref numLines, toolTipNames, ref toolTipLines, ref unusedPrefixLine, ref unusedBadPrefixLines, ref unusedYoyoLogo, out _, -1);
            return modifiedTooltipLines;
        }
    }
}