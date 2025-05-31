using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private Button switchButton;

    private int currentClipIndex = 0;

    private void Start()
    {
        // Проверяем и находим компоненты, если они не установлены в инспекторе
        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
            if (audioSource == null)
            {
                GameObject audioObject = new GameObject("AudioSource");
                audioSource = audioObject.AddComponent<AudioSource>();
            }
        }

        if (switchButton == null)
        {
            switchButton = GetComponent<Button>();
        }

        // Назначаем обработчик нажатия кнопки
        switchButton.onClick.AddListener(SwitchMusic);

        // Запускаем первую музыку, если клипы есть
        if (musicClips.Length > 0)
        {
            audioSource.clip = musicClips[0];
            audioSource.Play();
        }
    }

    public void SwitchMusic()
    {
        if (musicClips.Length == 0) return;

        // Переключаемся на следующий клип
        currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }

    private void OnDestroy()
    {
        // Удаляем обработчик при уничтожении объекта
        if (switchButton != null)
        {
            switchButton.onClick.RemoveListener(SwitchMusic);
        }
    }
}