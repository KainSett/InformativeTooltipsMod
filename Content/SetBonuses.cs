using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using InformativeTooltips.Content.GlobalTooltips;

namespace InformativeTooltips.Content.SetBonuses
{
    public class GlobalSetBonus : GlobalTooltipsBase
    {
        public GlobalSetBonus() : base(5) { }
        public virtual List<string> Lines { get; set; } = new List<string> { string.Empty };
        public virtual bool IsThis { get; set; } = false;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return !item.accessory && !item.social && !item.vanity && IsThis;
        }
        public override bool InstancePerEntity => true;
        public override void UpdateArmorSet(Player player, string set)
        {
            Main.NewText($"{player.setBonus}");
            Main.NewText($"{set}");
            //player.setBonus = string.Empty;
            //foreach (var line in Lines)
            //{
            //    player.setBonus += "\n" + line;
            //}
        }
    }
    public class CrimsonSet : GlobalSetBonus
    {
        public override List<string> Lines { get; set; } = new List<string> { string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenRampRate.Tooltip"), 100), string.Format(Language.GetTextValue("Mods.InformativeTooltips.Buffs.NatRegenMult.Tooltip"), 1.5) };
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.CrimsonHelmet;
        }
        public override bool IsThis { get; set; } = true;
    }
}