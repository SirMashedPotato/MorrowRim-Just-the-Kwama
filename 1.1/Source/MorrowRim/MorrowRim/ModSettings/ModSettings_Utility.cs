using Verse;

namespace MorrowRim_Kwama
{
    class ModSettings_Utility
    {

        public static float SettingToFloat(int setting)
        {
            float f = setting;
            return f / 100;
        }

        /* incidents */

        public static bool MorrowRim_SettingEnableKwamaNestEmerging()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingEnableKwamaNestEmerging;
        }

        public static bool MorrowRim_SettingEnableKwamaTrojanInfestation()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingEnableKwamaTrojanInfestation;
        }

        /* kwama */

        public static bool MorrowRim_SettingForceKwamaNatural()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingForceKwamaNatural;
        }

        public static int MorrowRim_SettingKwamaMinDistance()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingKwamaMinDistance;
        }

        public static int MorrowRim_SettingKwamaMaxNests()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingKwamaMaxNests;

        }
    }
}
