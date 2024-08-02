using Il2CppSLZ.Marrow.Pool;
using WeatherElectric.RecursiveSelfImprovement.Utilities;
using HarmonyLib;
using Il2CppSLZ.Marrow.Warehouse;

namespace WeatherElectric.RecursiveSelfImprovement;

public class Main : MelonMod
{
    internal const string Name = "RecursiveSelfImprovement";
    internal const string Description = "A general quality of life/tools mod.";
    internal const string Author = "FragileDeviations";
    internal const string Company = "Weather Electric";
    internal const string Version = "1.0.0";
    internal const string DownloadLink = "https://thunderstore.io/c/bonelab/p/SoulWithMae/RecursiveSelfImprovement/";
    
    internal static Page MenuCat { get; private set; }
    internal static Assembly CurrAsm { get; } = Assembly.GetExecutingAssembly();

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        CreateMenu();
        SetupHooking();
        
        Utility.Initialize();
    }
    
    public override void OnUpdate()
    {
        Utility.OnUpdate();
    }

    public override void OnLateUpdate()
    {
        Utility.OnLateUpdate();
    }
    
    public override void OnFixedUpdate()
    {
        Utility.OnFixedUpdate();
    }

    private static void SetupHooking()
    {
        Hooking.OnWarehouseReady += OnWarehouseLoaded;
        Hooking.OnLevelLoaded += OnLevelLoad;
        Hooking.OnLevelUnloaded += OnlevelUnload;
        Hooking.OnUIRigCreated += OnPlayerReady;
    }
    
    private static void OnWarehouseLoaded()
    {
        var pallets = AssetWarehouse.Instance.GetPallets();
        var crates = AssetWarehouse.Instance.GetCrates();
        Utility.WarehouseLoaded(pallets, crates);
        AssetWarehouse.Instance.OnPalletAdded += (Il2CppSystem.Action<Barcode>)OnPalletAdded;
    }

    private static void OnPalletAdded(Barcode barcode)
    {
        Utility.PalletAdded(barcode);
    }

    private static void OnLevelLoad(LevelInfo levelInfo)
    {
        Utility.LevelLoad(levelInfo);
    }

    private static void OnlevelUnload()
    {
        Utility.LevelUnload();
    }

    private static void OnPlayerReady()
    {
        Utility.PlayerReady();
    }
    
    private static void CreateMenu()
    {
        var mainCat = Page.Root.CreatePage("<color=#6FBDFF>Weather Electric</color>", Color.white);
        MenuCat = mainCat.CreatePage("<color=#009dff>Marrow Utils</color>", Color.white);
    }
    
    [HarmonyPatch(typeof(Poolee))]
    private static class PooleeStart
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Poolee.OnInitialize))]
        private static void Postfix(Poolee __instance, IPoolable poolable)
        {
            Utility.SpawnablePlaced(__instance, __instance.gameObject);
        }
    }
}