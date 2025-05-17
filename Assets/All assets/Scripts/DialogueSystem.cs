using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Текстовое поле для отображения диалога (TextMeshProUGUI)
    public float textSpeed = 0.05f; // Скорость появления текста
    public string[] sentences; // Массив строк с репликами
    private int currentSentence = 0; // Номер текущей реплики

    void Start()
    {
        StartCoroutine(DisplayNextSentence()); // Запускаем показ первой реплики при старте
    }

    IEnumerator DisplayNextSentence()
    {
        if (currentSentence < sentences.Length)
        {
            dialogueText.text = ""; // Очищаем текст
            foreach (char letter in sentences[currentSentence])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed); // Пауза для отображения по буквам
            }
            currentSentence++;
        }
        else
        {
            // Если все реплики показаны, запускаем корутину для скрытия текста
            StartCoroutine(FadeOutText());
        }
    }

    IEnumerator FadeOutText()
    {
        // Плавно уменьшаем прозрачность текста
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= 0.05f;
            dialogueText.alpha = alpha;
            yield return new WaitForSeconds(0.05f);
        }
        // После завершения анимации скрываем текст
        dialogueText.text = "";
    }

    void Update()
    {
        // Пропуск реплики при нажатии на кнопку мыши
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines(); // Прерываем текущий процесс показа реплики
            StartCoroutine(DisplayNextSentence()); // Переходим к следующей реплике
        }
    }
}