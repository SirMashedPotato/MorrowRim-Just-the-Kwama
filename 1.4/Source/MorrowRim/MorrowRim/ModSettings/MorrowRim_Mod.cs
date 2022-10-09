using UnityEngine;
using Verse;
using System;

namespace MorrowRim_Kwama
{
    class MorrowRim_Mod : Mod
    {
        MorrowRim_ModSettings settings;
        public MorrowRim_Mod(ModContentPack contentPack) : base(contentPack)
        {
            this.settings = GetSettings<MorrowRim_ModSettings>();
        }

        public override string SettingsCategory() => "MorrowRim - Just the Kwama";

        private static Vector2 scrollPosition = Vector2.zero;

        public override void DoSettingsWindowContents(Rect inRect)
        {

            var firstColumnWidth = (inRect.width - Listing.ColumnSpacing) * 3.5f / 5f;
            var secondColumnWidth = inRect.width - Listing.ColumnSpacing - firstColumnWidth;

            var outerRect = new Rect(inRect.x, inRect.y, firstColumnWidth, inRect.height);
            var innerRect = new Rect(0f, 0f, firstColumnWidth - 24f, inRect.height * 2);
            Widgets.BeginScrollView(outerRect, ref scrollPosition, innerRect, true);

            var listing_Standard = new Listing_Standard();
            listing_Standard.Begin(innerRect);
            listing_Standard = SettingsPage_Kwama(listing_Standard);

            listing_Standard.End();
            Widgets.EndScrollView();
            base.DoSettingsWindowContents(inRect);

            //buttons pane
            outerRect.x += firstColumnWidth + Listing.ColumnSpacing;
            outerRect.width = secondColumnWidth;

            listing_Standard = new Listing_Standard();
            listing_Standard.Begin(outerRect);
            SettingsButtons(listing_Standard);
            listing_Standard.End();
        }

        private Listing_Standard SettingsButtons(Listing_Standard listing_Standard)
        {
            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "MorrowRim_ResetKwama".Translate());
            if (Widgets.ButtonText(rectDefault, "MorrowRim_ResetKwama".Translate(), true, true, true))
            {
                MorrowRim_ModSettings.ResetSettings(settings);
            }

            return listing_Standard;
        }

        //Specific pages

        private Listing_Standard SettingsPage_Kwama(Listing_Standard listing_Standard)
        {
            listing_Standard.Label("MorrowRim_PageKwama".Translate());
            listing_Standard.GapLine();
            listing_Standard.Gap();

            //settings
            listing_Standard.CheckboxLabeled("MorrowRim_SettingKwamaEnableGen".Translate(), ref settings.MorrowRim_SettingKwamaEnableGen);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingKwamaNestReproducing".Translate(), ref settings.MorrowRim_SettingKwamaNestReproducing);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingKwamaMining".Translate(), ref settings.MorrowRim_SettingKwamaMining);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingEnablePredatorAvoidKwama".Translate(), ref settings.MorrowRim_SettingEnablePredatorAvoidKwama);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingForceKwamaNatural".Translate(), ref settings.MorrowRim_SettingForceKwamaNatural);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingKwamaEnableTrojanHostile".Translate(), ref settings.MorrowRim_SettingKwamaEnableTrojanHostile);
            listing_Standard.Gap();

            listing_Standard.Label("MorrowRim_SettingKwamaMinDistance".Translate() + " (" + settings.MorrowRim_SettingKwamaMinDistance + " tiles)");
            settings.MorrowRim_SettingKwamaMinDistance = (int)(listing_Standard.Slider(settings.MorrowRim_SettingKwamaMinDistance, 0, 100));
            listing_Standard.Gap();

            listing_Standard.Label("MorrowRim_SettingKwamaMaxNests".Translate() + " (" + settings.MorrowRim_SettingKwamaMaxNests + " nests)");
            settings.MorrowRim_SettingKwamaMaxNests = (int)(listing_Standard.Slider(settings.MorrowRim_SettingKwamaMaxNests, 1, 20));
            listing_Standard.Gap();

            listing_Standard.GapLine();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingEnableKwamaNestEmerging".Translate(), ref settings.MorrowRim_SettingEnableKwamaNestEmerging);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingEnableKwamaTrojanInfestation".Translate(), ref settings.MorrowRim_SettingEnableKwamaTrojanInfestation);
            listing_Standard.Gap();

            return listing_Standard;
        }
    }
}
