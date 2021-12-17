using Verse;

namespace MorrowRim
{
    class ModSettings_Utility
    {
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

        public static bool MorrowRim_SettingKwamaEnableGen()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingKwamaEnableGen;
        }

        public static bool MorrowRim_SettingEnablePredatorAvoidKwama()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingEnablePredatorAvoidKwama;
        }

        public static bool MorrowRim_SettingKwamaEnableTrojanHostile()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingKwamaEnableTrojanHostile;
        }

        public static bool MorrowRim_SettingKwamaNestReproducing()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingKwamaNestReproducing;
        }

        public static bool MorrowRim_SettingKwamaMining()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingKwamaMining;
        }

        /* */

        public static bool MorrowRim_SettingEnableKwamaNestEmerging()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingEnableKwamaNestEmerging;
        }

        public static bool MorrowRim_SettingEnableKwamaTrojanInfestation()
        {
            return LoadedModManager.GetMod<MorrowRim_Mod>().GetSettings<MorrowRim_ModSettings>().MorrowRim_SettingEnableKwamaTrojanInfestation;
        }
    }
}
