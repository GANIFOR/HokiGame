using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class CutsceneManager : MonoBehaviour
{
    [Header("Настройки катсцены")]
    [SerializeField] private PlayableDirector timeline; // Ссылка на Timeline
    [SerializeField] private GameObject player; // Персонаж игрока
    [SerializeField] private MonoBehaviour playerController; // Скрипт управления персонажем
    [SerializeField] private CinemachineVirtualCamera playerVCam; 
    [SerializeField] private CinemachineVirtualCamera cutsceneVCam;

    private void Start()
    {
        timeline.stopped += OnCutsceneFinished; // Событие окончания
        timeline.Play();
        if (timeline == null)
            timeline = GetComponent<PlayableDirector>();

        StartCutscene();
    }

    public void StartCutscene()
    {
        cutsceneVCam.Priority = 100;
        playerVCam.Priority = 0;
        if (playerController != null)
            playerController.enabled = false;

        // Запускаем катсцену
        if (timeline != null)
        {
            timeline.Play();
            Invoke("EndCutscene", (float)timeline.duration); // Автозавершение
        }
        else
        {
            Debug.LogError("Timeline не назначен!");
            Invoke("EndCutscene", 5f); // Запасной вариант
        }
    }

    public void EndCutscene()
    {
        cutsceneVCam.Priority = 0;
        playerVCam.Priority = 100;
        if (playerController != null)
            playerController.enabled = true;

        Debug.Log("Катсцена завершена");
    }
    private void OnCutsceneFinished(PlayableDirector director)
    {
        playerVCam.Priority = 10; // Возвращаем камеру игроку
    }
}