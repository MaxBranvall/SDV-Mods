using System.Collections.Generic;
using CJBCheatsMenu.Framework.Components;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace CJBCheatsMenu.Framework.Cheats.FarmAndFishing
{
    /// <summary>A cheat which enables instant catches when fishing.</summary>
    internal class InstantFishCatchCheat : BaseCheat
    {
        /*********
        ** Public methods
        *********/
        /// <summary>Get the config UI fields to show in the cheats menu.</summary>
        /// <param name="context">The cheat context.</param>
        public override IEnumerable<OptionsElement> GetFields(CheatContext context)
        {
            yield return new CheatsOptionsCheckbox(
                label: I18n.Fishing_InstantCatch(),
                value: context.Config.InstantCatch,
                setValue: value => context.Config.InstantCatch = value
            );
        }

        /// <summary>Handle the cheat options being loaded or changed.</summary>
        /// <param name="context">The cheat context.</param>
        /// <param name="needsUpdate">Whether the cheat should be notified of game updates.</param>
        /// <param name="needsInput">Whether the cheat should be notified of button presses.</param>
        /// <param name="needsRendering">Whether the cheat should be notified of render ticks.</param>
        public override void OnConfig(CheatContext context, out bool needsInput, out bool needsUpdate, out bool needsRendering, out bool needsInventoryChanged)
        {
            needsInput = false;
            needsUpdate = context.Config.InstantCatch;
            needsRendering = false;
            needsInventoryChanged = false;
        }

        /// <summary>Handle a game update if <see cref="ICheat.OnSaveLoaded"/> indicated updates were needed.</summary>
        /// <param name="context">The cheat context.</param>
        /// <param name="e">The update event arguments.</param>
        public override void OnUpdated(CheatContext context, UpdateTickedEventArgs e)
        {
            if (Game1.player?.CurrentTool is FishingRod && Game1.activeClickableMenu is BobberBar bobberMenu)
            {
                context.Reflection.GetField<float>(bobberMenu, "distanceFromCatching").SetValue(1);
                if (context.Reflection.GetField<bool>(bobberMenu, "treasure").GetValue())
                    context.Reflection.GetField<bool>(bobberMenu, "treasureCaught").SetValue(true);
            }
        }
    }
}
