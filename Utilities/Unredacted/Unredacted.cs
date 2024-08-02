using Il2CppSLZ.Marrow.Warehouse;

namespace WeatherElectric.RecursiveSelfImprovement.Utilities.Unredacted;

internal class Unredacted : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading Unredacted...", 1);
    }
    
    protected override void OnWarehouseInit(PalletList loadedPalletList)
    {
        if (!Preferences.UnredactCrates.Value) return;
        foreach (var pallet in loadedPalletList)
        {
            var crates = pallet.Crates;
            foreach (var crate in crates)
            {
                if (!crate.Redacted) continue;
                crate.Redacted = false;
                crate._redacted = false;
                ModConsole.Msg($"[Unredacted] Unredacted {crate.name}!", 1);
            }
        }
    }

    protected override void OnPalletAdded(Barcode barcode)
    {
        if (!Preferences.UnredactCrates.Value) return;
        var success = AssetWarehouse.Instance.TryGetPallet(barcode, out var pallet);
        if (!success) return;
        var crates = pallet.Crates;
        foreach (var crate in crates)
        {
            if (!crate.Redacted) continue;
            crate.Redacted = false;
            crate._redacted = false;
            ModConsole.Msg($"[Unredacted] Unredacted {crate.name}!", 1);
        }
    }
    
    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreatePage("Unredacted", Color.magenta);
        menu.CreateBoolPreference("Toggle Unredaction", Color.white, Preferences.UnredactCrates, Preferences.UnredactedCategory);
    }
}