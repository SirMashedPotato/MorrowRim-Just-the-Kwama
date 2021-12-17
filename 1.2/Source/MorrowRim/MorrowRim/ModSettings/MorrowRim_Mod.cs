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

        public override string SettingsCategory() => "MorrowRim - Just The Kwama";

        private int page = 0;

        private static Vector2 scrollPosition = Vector2.zero;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            Rect rect2 = new Rect(0f, 0f, inRect.width - 30, inRect.height * 2);
            Widgets.BeginScrollView(rect,ref scrollPosition, rect2);
            listing_Standard.Begin(rect2);

            //get page
            switch (page)
            {
                case 1:
                    listing_Standard = SettingsPage_Kwama(listing_Standard);
                    break;

                case 2:
                    listing_Standard = SettingsPage_Incident(listing_Standard);
                    break;

                default:
                    listing_Standard = SettingsPage_Default(listing_Standard);
                    break;
            }

            listing_Standard = SettingsButtons(listing_Standard);

            listing_Standard.End();
            Widgets.EndScrollView();
            base.DoSettingsWindowContents(inRect);
        }

        private Listing_Standard SettingsButtons(Listing_Standard listing_Standard)
        {
            listing_Standard.Gap();
            listing_Standard.GapLine();
            //pages

            if (page != 0)
            {
                Rect rectPageDefault = listing_Standard.GetRect(30f);
                TooltipHandler.TipRegion(rectPageDefault, "MorrowRim_PageDefault".Translate());
                if (Widgets.ButtonText(rectPageDefault, "MorrowRim_PageDefault".Translate(), true, true, true))
                {
                    page = 0;
                }
                listing_Standard.Gap();
            }

            if (page != 1)
            {

                Rect rectPageKwama = listing_Standard.GetRect(30f);
                TooltipHandler.TipRegion(rectPageKwama, "MorrowRim_PageKwama".Translate());
                if (Widgets.ButtonText(rectPageKwama, "MorrowRim_PageKwama".Translate(), true, true, true))
                {
                    page = 1;
                }
                listing_Standard.Gap();
            }
            if(page != 2) {

                Rect rectPageIncident = listing_Standard.GetRect(30f);
                TooltipHandler.TipRegion(rectPageIncident, "MorrowRim_PageIncident".Translate());
                if (Widgets.ButtonText(rectPageIncident, "MorrowRim_PageIncident".Translate(), true, true, true))
                {
                    page = 2;
                }
                listing_Standard.Gap();
            }

            listing_Standard.GapLine();

            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "MorrowRim_ResetDefault".Translate());
            if (Widgets.ButtonText(rectDefault, "MorrowRim_ResetDefault".Translate(), true, true, true))
            {
                MorrowRim_ModSettings.ResetSettings(settings);
            }

            return listing_Standard;
        }

        //Specific pages

        private Listing_Standard SettingsPage_Default(Listing_Standard listing_Standard)
        {
            listing_Standard.Label("MorrowRim_PageDefault".Translate());
            listing_Standard.GapLine();
            listing_Standard.Gap();


            return listing_Standard;
        }

        private Listing_Standard SettingsPage_Incident(Listing_Standard listing_Standard)
        {
            listing_Standard.Label("MorrowRim_PageIncident".Translate());
            listing_Standard.GapLine();
            listing_Standard.Gap();

            //settings

            listing_Standard.CheckboxLabeled("MorrowRim_SettingEnableKwamaNestEmerging".Translate(), ref settings.MorrowRim_SettingEnableKwamaNestEmerging);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("MorrowRim_SettingEnableKwamaTrojanInfestation".Translate(), ref settings.MorrowRim_SettingEnableKwamaTrojanInfestation);
            listing_Standard.Gap();


            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "MorrowRim_ResetIncident".Translate());
            if (Widgets.ButtonText(rectDefault, "MorrowRim_ResetIncident".Translate(), true, true, true))
            {
                MorrowRim_ModSettings.ResetSettings_Incident(settings);
            }

            return listing_Standard;
        }

        private Listing_Standard SettingsPage_Kwama(Listing_Standard listing_Standard)
        {
            listing_Standard.Label("MorrowRim_PageKwama".Translate());
            listing_Standard.GapLine();
            listing_Standard.Gap();

            //settings
            listing_Standard.CheckboxLabeled("MorrowRim_SettingForceKwamaNatural".Translate(), ref settings.MorrowRim_SettingForceKwamaNatural);
            listing_Standard.Gap();

            listing_Standard.Label("MorrowRim_SettingKwamaMinDistance".Translate() + " (" + settings.MorrowRim_SettingKwamaMinDistance + " tiles)");
            settings.MorrowRim_SettingKwamaMinDistance = (int)(listing_Standard.Slider(settings.MorrowRim_SettingKwamaMinDistance, 0, 100));
            listing_Standard.Gap();

            listing_Standard.Label("MorrowRim_SettingKwamaMaxNests".Translate() + " (" + settings.MorrowRim_SettingKwamaMaxNests + " nests)");
            settings.MorrowRim_SettingKwamaMaxNests = (int)(listing_Standard.Slider(settings.MorrowRim_SettingKwamaMaxNests, 1, 20));
            listing_Standard.Gap();

            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "MorrowRim_ResetKwama".Translate());
            if (Widgets.ButtonText(rectDefault, "MorrowRim_ResetKwama".Translate(), true, true, true))
            {
                MorrowRim_ModSettings.ResetSettings_Kwama(settings);
            }

            return listing_Standard;
        }
    }
}
