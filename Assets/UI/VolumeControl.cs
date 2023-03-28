using UnityEngine;
using UnityEngine.UIElements;

public class VolumeControl : MonoBehaviour
{
    private VisualElement root;
    [SerializeField] private Sprite muteMusicIcon;
    [SerializeField] private Sprite unMuteMusicIcon;

    [SerializeField] private Sprite muteSFXIcon;
    [SerializeField] private Sprite unMuteSFXIcon;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Button muteMusic = root.Q<Button>("MuteMusic");
        muteMusic.RegisterCallback<ClickEvent>(_ => ToggleMusic());

        Button muteSFX = root.Q<Button>("MuteSFX");
        muteSFX.RegisterCallback<ClickEvent>(_ => ToggleSFX());

        Slider volumeSlider = root.Q<Slider>("VolumeControl");
        volumeSlider.RegisterValueChangedCallback(_ => SoundManager.Instance.SetVolume(_.newValue));
    }

    private void Start()
    {
        UpdateVolumeDisplay();
    }

    private void ToggleMusic()
    {
        SoundManager.Instance.ToggleMusic();
        UpdateVolumeDisplay();
    }

    private void ToggleSFX()
    {
        SoundManager.Instance.ToggleSFX();
        UpdateVolumeDisplay();
    }

    private void UpdateVolumeDisplay()
    {
        root.Q<Button>("MuteMusic").style.backgroundImage = new StyleBackground(SoundManager.Instance.MusicMuted ? muteMusicIcon : unMuteMusicIcon);
        root.Q<Button>("MuteSFX").style.backgroundImage = new StyleBackground(SoundManager.Instance.SFXMuted ? muteSFXIcon : unMuteSFXIcon);
        root.Q<Slider>("VolumeControl").value = AudioListener.volume;
    }
}
