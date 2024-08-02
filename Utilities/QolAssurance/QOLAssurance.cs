using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow.Pool;
using Il2CppTMPro;
using WeatherElectric.RecursiveSelfImprovement.Utilities.QolAssurance.Behaviours;

namespace WeatherElectric.RecursiveSelfImprovement.Utilities.QolAssurance;

internal class QolAssurance : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading QualityOfLifeAssurance...", 1);
        Assets.Load();
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreatePage("Quality of Life Assurance", Color.green);
        menu.CreateBoolPreference("Enabled", Color.white, Preferences.EnableItemPatches, Preferences.QualityOfLifeAssuranceCategory);
    }
    
    protected override void OnSpawnablePlaced(GameObject obj, Poolee poolee)
    {
        if (!Preferences.EnableItemPatches.Value) return;
        if (poolee.SpawnableCrate.Barcode.ID == "SLZ.BONELAB.Content.Spawnable.AlarmClock") FixAlarmClock(obj);
    }

    private static void FixAlarmClock(GameObject obj)
    {
        var tmp = obj.GetComponentInChildren<TextMeshPro>();
        var uiClock = obj.AddComponent<UIClock>();
        var alarmClock = obj.AddComponent<AlarmClock>();
        if (tmp == null || uiClock == null) return;
        uiClock.txt_clock = tmp;
        uiClock.OnEnable();
        var audioSource = obj.AddComponent<AudioSource>();
        SetupAudioSource();
        alarmClock.AudioSource = audioSource;
        return;

        void SetupAudioSource()
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 1f;
            audioSource.maxDistance = 10f;
            audioSource.minDistance = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.volume = 0.5f;
            audioSource.clip = Assets.AlarmSound;
        }
    }
    
    private static class Assets
    {
        private static AssetBundle _bundle;
        public static AudioClip AlarmSound { get; private set; }

        public static void Load()
        {
            _bundle = HelperMethods.LoadEmbeddedAssetBundle(Main.CurrAsm, HelperMethods.IsAndroid() ? "WeatherElectric.RecursiveSelfImprovement.Utilities.QolAssurance.Resources.Android.bundle" : "WeatherElectric.RecursiveSelfImprovement.Utilities.QolAssurance.Resources.Windows.bundle");
            AlarmSound = _bundle.LoadPersistentAsset<AudioClip>("Assets/MarrowUtils/QOLAssurance/BoneTheme.wav");
        }
    }
}