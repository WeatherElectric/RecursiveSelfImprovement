using Il2CppSLZ.Marrow.Pool;
using Il2CppSLZ.Marrow.Warehouse;

namespace WeatherElectric.RecursiveSelfImprovement.Utilities;

public abstract class Utility
{
    private static readonly List<Utility> Instances = [];

    protected internal Utility()
    {
        Instances.Add(this);
    }

    ~Utility()
    {
        Instances.Remove(this);
    }
    
    protected virtual void OnLevelLoad(LevelInfo levelInfo){}
    protected virtual void OnLevelUnload(){}
    protected virtual void Update(){}
    protected virtual void CreateMenu(){}
    protected virtual void Start(){}
    protected virtual void OnWarehouseInit(){}
    protected virtual void OnWarehouseInit(PalletList loadedPalletList){}
    protected virtual void OnWarehouseInit(PalletList loadedPalletList, CrateList crateList){}
    protected virtual void OnWarehouseInit(CrateList crateList){}
    protected virtual void OnPalletAdded(Barcode barcode){}
    protected virtual void LateUpdate(){}
    protected virtual void FixedUpdate(){}
    protected virtual void OnSpawnablePlaced(GameObject obj){}
    protected virtual void OnSpawnablePlaced(Poolee poolee){}
    protected virtual void OnSpawnablePlaced(GameObject obj, Poolee poolee){}
    protected virtual void OnSpawnablePlaced(){}
    protected virtual void OnPlayerReady(){}
    
    public static void Initialize()
    {
        // automatically create instances of all utilities
        foreach (var type in Main.CurrAsm.GetTypes())
        {
            if (type.IsAbstract || !type.IsSubclassOf(typeof(Utility))) continue;
            Activator.CreateInstance(type);
        }
        
        foreach (var instance in Instances)
        {
            instance.Start();
            instance.CreateMenu();
        }
    }

    public static void SpawnablePlaced(Poolee poolee, GameObject obj)
    {
        foreach (var instance in Instances)
        {
            instance.OnSpawnablePlaced();
            instance.OnSpawnablePlaced(poolee);
            instance.OnSpawnablePlaced(obj);
            instance.OnSpawnablePlaced(obj, poolee);
        }
    }
    
    public static void LevelLoad(LevelInfo levelInfo)
    {
        foreach (var instance in Instances)
        {
            instance.OnLevelLoad(levelInfo);
        }
    }
    
    public static void LevelUnload()
    {
        foreach (var instance in Instances)
        {
            instance.OnLevelUnload();
        }
    }
    
    public static void PlayerReady()
    {
        foreach (var instance in Instances)
        {
            instance.OnPlayerReady();
        }
    }

    public static void OnUpdate()
    {
        foreach (var instance in Instances)
        {
            instance.Update();
        }
    }

    public static void OnLateUpdate()
    {
        foreach (var instance in Instances)
        {
            instance.LateUpdate();
        }
    }

    public static void OnFixedUpdate()
    {
        foreach (var instance in Instances)
        {
            instance.FixedUpdate();
        }
    }

    public static void WarehouseLoaded(PalletList loadedPalletList, CrateList crateList)
    {
        foreach (var instance in Instances)
        {
            instance.OnWarehouseInit();
            instance.OnWarehouseInit(loadedPalletList);
            instance.OnWarehouseInit(crateList);
            instance.OnWarehouseInit(loadedPalletList, crateList);
        }
    }
    
    public static void PalletAdded(Barcode barcode)
    {
        foreach (var instance in Instances)
        {
            instance.OnPalletAdded(barcode);
        }
    }
}