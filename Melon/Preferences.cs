// ReSharper disable MemberCanBePrivate.Global, these categories may be used outside of this namespace to create bonemenu options.

using MelonLoader.Utils;

namespace WeatherElectric.RecursiveSelfImprovement.Melon;

internal static class Preferences
{
    public static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");

    public static readonly MelonPreferences_Category JanitorCategory = MelonPreferences.CreateCategory("TheJanitor");
    public static readonly MelonPreferences_Category ReloaderCategory = MelonPreferences.CreateCategory("LevelReloader");
    public static readonly MelonPreferences_Category UnredactedCategory = MelonPreferences.CreateCategory("Unredacted");
    public static readonly MelonPreferences_Category QualityOfLifeAssuranceCategory = MelonPreferences.CreateCategory("QualityOfLifeAssurance");
    
    public static MelonPreferences_Entry<bool> OverrideFusionCheck { get; set; }
    public static MelonPreferences_Entry<bool> EnableReloader { get; set; }
    public static MelonPreferences_Entry<bool> UnredactCrates { get; set; }
    public static MelonPreferences_Entry<bool> EnableItemPatches { get; set; }
    
    public static MelonPreferences_Entry<int> LoggingMode { get; set; }

    public static void Setup()
    {
        LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0,
            "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
        GlobalCategory.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        GlobalCategory.SaveToFile(false);
        
        OverrideFusionCheck = JanitorCategory.GetEntry<bool>("OverrideFusionCheck") ?? JanitorCategory.CreateEntry("OverrideFusionCheck", false, "Override Fusion Check", "Whether or not to override the Fusion check. This is not recommended, as it causes desync.");
        JanitorCategory.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        JanitorCategory.SaveToFile(false);
        EnableReloader = ReloaderCategory.GetEntry<bool>("EnabledReloader") ?? ReloaderCategory.CreateEntry("EnabledReloader", true, "Enabled", "Whether or not to check for Control + R."); 
        UnredactCrates = UnredactedCategory.GetEntry<bool>("UnredactCrates") ?? UnredactedCategory.CreateEntry("UnredactCrates", true, "Unredact Crates", "Whether or not to unredact crates.");
        UnredactedCategory.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        UnredactedCategory.SaveToFile(false);
        EnableItemPatches = QualityOfLifeAssuranceCategory.GetEntry<bool>("EnableItemPatches") ?? QualityOfLifeAssuranceCategory.CreateEntry("EnableItemPatches", true, "Enable Item Patches", "Whether or not to enable small item fixes.");
        QualityOfLifeAssuranceCategory.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        QualityOfLifeAssuranceCategory.SaveToFile(false);
        
        ModConsole.Msg("Finished preferences setup for RecursiveSelfImprovement", 1);
    }
}