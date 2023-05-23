using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJBCheatsMenu.Framework.Components;
using CJBCheatsMenu.Framework.Models;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace CJBCheatsMenu.Framework.Cheats.PlayerAndTools
{
    internal class DuplicateItemsCheat : BaseCheat
    {

        public override void OnConfig(CheatContext context, out bool needsInput, out bool needsUpdate, out bool needsRendering, out bool needsInventoryChanged)
        {
            needsInventoryChanged = context.Config.DuplicateItems;
            needsInput = context.Config.DuplicateItemsKey.IsBound;
            needsUpdate = false;
            needsRendering = false;
        }

        /// <summary>Get the config UI fields to show in the cheats menu.</summary>
        /// <param name="context">The cheat context.</param>
        public override IEnumerable<OptionsElement> GetFields(CheatContext context)
        {
            yield return new CheatsOptionsCheckbox(
                label: "Duplicate Items",
                value: context.Config.DuplicateItems,
                setValue: value => context.Config.DuplicateItems = value
            );
        }

        public override void OnButtonsChanged(CheatContext context, ButtonsChangedEventArgs e)
        {
            ModConfig config = context.Config;

            if (config.DuplicateItemsKey.JustPressed())
            {
                config.DuplicateItems = !config.DuplicateItems;

                Game1.playSound(config.DuplicateItems ? "dog_bark" : "cat");
            }
        }

        public override void OnInventoryChanged(CheatContext context, InventoryChangedEventArgs e)
        {
            var removedItem = e.Removed.FirstOrDefault();
            e.Player.addItemToInventory(removedItem);
        }
    }
}
