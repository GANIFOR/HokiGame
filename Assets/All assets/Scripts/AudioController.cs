using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button muteButton;
    [SerializeField] private Image muteButtonImage; // Компонент Image кнопки

    [Header("Sprites")]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private bool isMuted = false;
    private float savedVolumeBeforeMute;

    private void Start()
    {
        // Загружаем настройки
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;

        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        UpdateMuteButtonVisual(); // Обновляем вид кнопки

        // Подписываемся на события
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteButton.onClick.AddListener(ToggleMute);
    }

    private void SetVolume(float volume)
    {
        if (!isMuted)
        {
            AudioListener.volume = volume;
            PlayerPrefs.SetFloat("GameVolume", volume);
        }
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            savedVolumeBeforeMute = AudioListener.volume;
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = savedVolumeBeforeMute;
            volumeSlider.value = savedVolumeBeforeMute;
        }

        UpdateMuteButtonVisual();
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
    }

    // Обновляем спрайт и другие визуальные элементы
    private void UpdateMuteButtonVisual()
    {
        muteButtonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
        muteButton.onClick.RemoveListener(ToggleMute);
    }
}