using Il2CppSLZ.Marrow.SceneStreaming;

namespace WeatherElectric.RecursiveSelfImprovement.Utilities.EmergencyRelocation;

internal class Teleporter : Utility
{
    protected override void Start()
    {
        ModConsole.Msg("Loading EmergencyRelocation...");
    }

    protected override void CreateMenu()
    {
        var menu = Main.MenuCat.CreatePage("Emergency Relocation", Color.green);
        menu.CreateFunction("Relocate", Color.white, Relocate);
    }
    
    private static Vector3 _levelStart;
    
    protected override void OnLevelLoad(LevelInfo levelInfo)
    {
        var playerMarker = Object.FindObjectOfType<PlayerMarker>();
        if (!playerMarker)
        {
            var rigmanager = GameObject.Find("[RigManager (Blank)]");
            if (rigmanager == null) return;
            _levelStart = rigmanager.transform.position;
        }
        else
        {
            _levelStart = playerMarker.transform.position;
        }
    }
    
    private static void Relocate()
    {
        // fun fact: Teleport() on the rigmanager is just a base game method, i dont even have to do anything to teleport the player, the game just has this
        Player.RigManager.Teleport(_levelStart);
    }
}