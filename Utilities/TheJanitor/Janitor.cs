using BoneLib.Notifications;
using Il2CppSLZ.Marrow.Pool;
using Il2CppSLZ.Marrow.Warehouse;

namespace WeatherElectric.RecursiveSelfImprovement.Utilities.TheJanitor;

internal class Janitor : Utility
{
    #region Notifications
    
    private static Notification EverythingCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared every spawnable!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification NpcsCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared all NPCs!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification GunsCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared all guns!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification MeleeCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared all melee!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification PropsCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared all props!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification DecalsCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared all decals!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification InFusionServer { get; } = new()
    {
        Title = "The Janitor",
        Message = "You are in a fusion server! This would cause desync. Not doing it.",
        Type = NotificationType.Error,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification ResetMap { get; } = new()
    {
        Title = "The Janitor",
        Message = "Reset the map!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    private static Notification UtilityGunsCleared { get; } = new()
    {
        Title = "The Janitor",
        Message = "Cleared all utility guns!",
        Type = NotificationType.Success,
        PopupLength = 1f,
        ShowTitleOnPopup = true
    };
    
    #endregion
    
    #region Variables
    
    private static bool _fusionInstalled;
    
    private static ClearType _clearType = ClearType.Everything;
    
    private static readonly List<CrateSpawner> SpawnableCratePlacers = new();
    
    #endregion
    
    #region Derived Class Overrides
    
    protected override void Start()
    {
        ModConsole.Msg("Loading The Janitor...", 1);
        _fusionInstalled = HelperMethods.CheckIfAssemblyLoaded("labfusion");
    }

    protected override void CreateMenu()
    {
        var janitorCat = Main.MenuCat.CreatePage("<color=#ecff6d>The Janitor</color>", Color.white);
        janitorCat.CreateEnum("Clear Type", Color.white, ClearType.Everything, UpdateType);
        janitorCat.CreateFunction("Clear", Color.white, Clear);
        janitorCat.CreateFunction("Reset Map", Color.white, Reset);
        janitorCat.CreateBoolPreference("Override Fusion Check", Color.red, Preferences.OverrideFusionCheck, Preferences.JanitorCategory);
    }
    
    protected override void OnLevelUnload()
    {
        SpawnableCratePlacers.Clear();
    }
    
    #endregion

    private static void UpdateType(Enum @enum)
    {
        _clearType = (ClearType)@enum;
    }

    private static void Clear()
    {
        // if (_fusionInstalled && !Preferences.OverrideFusionCheck.Value && (LabFusion.Network.NetworkInfo.IsClient || LabFusion.Network.NetworkInfo.IsServer) && _clearType != ClearType.Decals)
        // {
        //     Notifier.Send(InFusionServer);
        //     return;
        // }
        switch (_clearType)
        {
            case ClearType.Everything:
                ClearEverything();
                break;
            case ClearType.UtilityGuns:
                ClearUtilGuns();
                break;
            case ClearType.Npcs:
                ClearNPCs();
                break;
            case ClearType.Guns:
                ClearGuns();
                break;
            case ClearType.Melee:
                ClearMelee();
                break;
            case ClearType.Props:
                ClearProps();
                break;
            case ClearType.Decals:
                ClearDecals();
                break;
            default:
                ModConsole.Error("[TheJanitor] Invalid clear type!");
                break;
        }
    }
    
    
    #region Clearing
    
    private static void Reset()
    {
        // if (_fusionInstalled && !Preferences.OverrideFusionCheck.Value && (LabFusion.Network.NetworkInfo.IsClient || LabFusion.Network.NetworkInfo.IsServer) && _clearType != ClearType.Decals)
        // {
        //     Notifier.Send(InFusionServer);
        //     return;
        // }
        ClearEverything(true);
        // instead of making a new new list every time, just clear it and add to the same one. check if 0 to see if it is empty, and if it is, fill it.
        if (SpawnableCratePlacers.Count == 0)
        {
            var placers = Object.FindObjectsOfType<CrateSpawner>();
            if (placers.Length == 0) return;
            SpawnableCratePlacers.AddRange(placers);
        }
        foreach (var placer in SpawnableCratePlacers)
        {
            placer.ReSpawnSpawnable();
        }
        Notifier.Send(ResetMap);
    }
    
    private static void ClearEverything(bool nonotif = false)
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Barcode == (Barcode)"SLZ.BONELAB.Core.DefaultPlayerRig") return;
            poolee.Despawn();
        }
        if (!nonotif) Notifier.Send(EverythingCleared);
    }

    private static void ClearUtilGuns()
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Barcode == (Barcode)CommonBarcodes.Misc.NimbusGun || poolee.SpawnableCrate.Barcode == (Barcode)CommonBarcodes.Misc.SpawnGun || poolee.SpawnableCrate.Barcode == (Barcode)CommonBarcodes.Misc.Constrainer) return;
            {
                poolee.Despawn();
            }
        }
        Notifier.Send(UtilityGunsCleared);
    }
    
    private static void ClearNPCs()
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Tags.Contains("NPC"))
            {
                poolee.Despawn();
            }
        }
        Notifier.Send(NpcsCleared);
    }
    
    private static void ClearGuns()
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Tags.Contains("Gun"))
            {
                poolee.Despawn();
            }
        }
        Notifier.Send(GunsCleared);
    }
    
    private static void ClearMelee()
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Tags.Contains("Melee"))
            {
                poolee.Despawn();
            }
        }
        Notifier.Send(MeleeCleared);
    }
    
    private static void ClearProps()
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Tags.Contains("Prop"))
            {
                poolee.Despawn();
            }
        }
        Notifier.Send(PropsCleared);
    }
    
    private static void ClearDecals()
    {
        var poolees = Object.FindObjectsOfType<Poolee>();
        if (poolees.Length == 0) return;
        foreach (var poolee in poolees)
        {
            if (poolee.SpawnableCrate.Tags.Contains("Decal"))
            {
                poolee.Despawn();
            }
        }
        Notifier.Send(DecalsCleared);
    }
    
    #endregion

    private enum ClearType
    {
        Everything,
        UtilityGuns,
        Npcs,
        Guns,
        Melee,
        Props,
        Decals
    }
}