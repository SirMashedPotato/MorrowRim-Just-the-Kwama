using Verse;

namespace MorrowRim_Kwama
{
    class MorrowRim_ModSettings : ModSettings
    {
        //settings
    
        public bool MorrowRim_SettingForceKwamaNatural = MorrowRim_SettingForceKwamaNatural_def;
        public int MorrowRim_SettingKwamaMinDistance = MorrowRim_SettingKwamaMinDistance_def;
        public int MorrowRim_SettingKwamaMaxNests = MorrowRim_SettingKwamaMaxNests_def;

        public bool MorrowRim_SettingEnableKwamaNestEmerging = MorrowRim_SettingEnableKwamaNestEmerging_def;
        public bool MorrowRim_SettingEnableKwamaTrojanInfestation = MorrowRim_SettingEnableKwamaTrojanInfestation_def;

        //defaults
        private static readonly bool MorrowRim_SettingForceKwamaNatural_def = true;
        private static readonly int MorrowRim_SettingKwamaMinDistance_def = 5;
        private static readonly int MorrowRim_SettingKwamaMaxNests_def = 5;

        private static readonly bool MorrowRim_SettingEnableKwamaNestEmerging_def = true;
        private static readonly bool MorrowRim_SettingEnableKwamaTrojanInfestation_def = true;

        //save settings
        public override void ExposeData()
        {

            Scribe_Values.Look(ref MorrowRim_SettingForceKwamaNatural, "MorrowRim_SettingForceKwamaNatural", MorrowRim_SettingForceKwamaNatural_def);
            Scribe_Values.Look(ref MorrowRim_SettingKwamaMinDistance, "MorrowRim_SettingKwamaMinDistance", MorrowRim_SettingKwamaMinDistance_def);
            Scribe_Values.Look(ref MorrowRim_SettingKwamaMaxNests, "MorrowRim_SettingKwamaMaxNests", MorrowRim_SettingKwamaMaxNests_def);

            Scribe_Values.Look(ref MorrowRim_SettingEnableKwamaNestEmerging, "MorrowRim_SettingEnableKwamaNestEmerging", MorrowRim_SettingEnableKwamaNestEmerging_def);
            Scribe_Values.Look(ref MorrowRim_SettingEnableKwamaTrojanInfestation, "MorrowRim_SettingEnableKwamaTrojanInfestation", MorrowRim_SettingEnableKwamaTrojanInfestation_def);

            base.ExposeData();
        }

        //reset settings

        public static void ResetSettings(MorrowRim_ModSettings settings)
        {
            ResetSettings_Kwama(settings);
            ResetSettings_Incident(settings);
        }
        public static void ResetSettings_Kwama(MorrowRim_ModSettings settings)
        {
            settings.MorrowRim_SettingForceKwamaNatural = MorrowRim_SettingForceKwamaNatural_def;
            settings.MorrowRim_SettingKwamaMinDistance = MorrowRim_SettingKwamaMinDistance_def;
            settings.MorrowRim_SettingKwamaMaxNests = MorrowRim_SettingKwamaMaxNests_def;
        }

        public static void ResetSettings_Incident(MorrowRim_ModSettings settings)
        {
            settings.MorrowRim_SettingEnableKwamaNestEmerging = MorrowRim_SettingEnableKwamaNestEmerging_def;
            settings.MorrowRim_SettingEnableKwamaTrojanInfestation = MorrowRim_SettingEnableKwamaTrojanInfestation_def;
        }
    }
}
