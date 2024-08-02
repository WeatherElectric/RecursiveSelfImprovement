# Recursive Self Improvement

## Introduction
A collection of minor quality of life/utility mods for BONELAB.

This was originally MarrowUtils, but I honestly could NOT be bothered to fix that csproj. So I made a new one entirely.

## Utilities
- **Emergency Reload**: Teleports you back to the map's spawn.
- **Level Reloader**: Reloads the current level when you hit Control + R.
- **The Janitor**: Garry's Mod style cleanup tool.
- **Unredacted**: - Unhides any redacted crates.

## Contribution
* Utility creation is very simple.
* Make your class derive from Utility, and that's it.
* You get a couple base methods that get called.
* Feel free to add more hooks for base methods as needed. Originally I didn't have OnWarehouseInit until I needed it for Unredacted.

* Base Methods
> **Start()** - Equivalent to a Monobehaviour's Start. Gets called once the Utility is loaded, which is shortly after the mod itself gets loaded.

> **Update()** - Equivalent to a Monobehaviour's Update. Called every frame.

> **FixedUpdate()** - Same as a Monobehaviour's FixedUpdate.

> **LateUpdate()** - Same as a Monobehaviour's LateUpdate.

> **CreateMenu()** - Also like Start. Used for Bonemenu setup.

> **OnLevelLoad(LevelInfo levelInfo)** - Called when a level is loaded. Provides Bonelib's LevelInfo class as a variable so you can access information about the loaded level easily.

> **OnLevelUnload()** - Called when a level is unloaded.

> **OnWarehouseInit(List<Pallet> loadedPalletList)** - Called when Warehouse is finished loading. Provides a list of every pallet the user has installed, this includes the basegame pallets.

> **OnPalletAdded(Barcode barcode)** - Called when a pallet is added. Provides the barcode of the pallet that was added.

> **OnPlayerReady()** - Called when the player is properly ready. OnLevelLoaded isn't called when the player can see, this is.

### Example Script
```cs
using Utils = UnityEngine.Diagnostics.Utils;

namespace WeatherElectric.RecursiveSelfImprovement.Utilities.ExampleClass;

internal class ExampleClass : Utility
{
    // Called shortly after OnInitializeMelon
    protected override void Start()
    {
        ModConsole.Msg("Loading ExampleClass...", 1);
    }

    // Called once Warehouse is ready
    protected override void OnWarehouseInit(PalletList loadedPalletList)
    {
        foreach (var pallet in loadedPalletList)
        {
            if (pallet.Barcode == (barcode)"NotEnoughPhotons.Paranoia")
            {
                // stuff happens if person has Paranoia's pallet
            }
        }
    }

    // Called when a level is loaded
    protected override void OnLevelLoad(LevelInfo levelInfo)
    {
        ModConsole.Msg($"Level loaded: {levelInfo.title});
    }

    // Called when a level is unloaded
    protected override void OnLevelUnload()
    {
        ModConsole.Msg("Level unloaded");
    }

    // Called at the same time as Start, seperated for cleanliness.
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreatePage("Example Class", Color.magenta);
        // dont actually do this lol
        menu.CreateBoolPreference("Crash Game", Color.white, CrashGame);
    }

    // Example Class' own original method, not provided by Utility
    private static void CrashGame()
    {
        // please don't actually do this
        Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
    }
}
```