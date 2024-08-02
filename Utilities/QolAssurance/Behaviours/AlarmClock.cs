namespace WeatherElectric.RecursiveSelfImprovement.Utilities.QolAssurance.Behaviours;

[RegisterTypeInIl2Cpp]
public class AlarmClock : MonoBehaviour
{
    [NonSerialized]
    // ReSharper disable once UnassignedField.Global, ITS ASSIGNED WHEN I CREATE THE OBJECT YOU DINGUS
    public AudioSource AudioSource;

    private readonly int[] _playTimes = [0, 12];

    private bool _hasPlayedToday;

    private void Update()
    {
        if (!_hasPlayedToday && Array.IndexOf(_playTimes, DateTime.Now.Hour) != -1 && DateTime.Now.Minute == 0)
        {
            AudioSource.Play();
            _hasPlayedToday = true;
        }
        
        if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
        {
            _hasPlayedToday = false;
        }
    }
    
    // ReSharper disable once ConvertToPrimaryConstructor, this cannot be fucking touched i hate you rider
    public AlarmClock(IntPtr ptr) : base(ptr) { }
}