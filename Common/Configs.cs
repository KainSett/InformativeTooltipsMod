using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace InformativeTooltips.Common.Configs
{
    public class ArmorDetailedConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("Tooltips")]
        [DefaultValue(false)]
        public bool ArmorCompare;
        public bool ArmorDetailsToggle;
        public bool AccessoryStatsToggle;
        public bool BuffDetailsToggle;
    }
}