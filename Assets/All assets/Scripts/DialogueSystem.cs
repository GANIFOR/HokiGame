using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // ��������� ���� ��� ����������� ������� (TextMeshProUGUI)
    public float textSpeed = 0.05f; // �������� ��������� ������
    public string[] sentences; // ������ ����� � ���������
    private int currentSentence = 0; // ����� ������� �������

    void Start()
    {
        StartCoroutine(DisplayNextSentence()); // ��������� ����� ������ ������� ��� ������
    }

    IEnumerator DisplayNextSentence()
    {
        if (currentSentence < sentences.Length)
        {
            dialogueText.text = ""; // ������� �����
            foreach (char letter in sentences[currentSentence])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed); // ����� ��� ����������� �� ������
            }
            currentSentence++;
        }
        else
        {
            // ���� ��� ������� ��������, ��������� �������� ��� ������� ������
            StartCoroutine(FadeOutText());
        }
    }

    IEnumerator FadeOutText()
    {
        // ������ ��������� ������������ ������
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= 0.05f;
            dialogueText.alpha = alpha;
            yield return new WaitForSeconds(0.05f);
        }
        // ����� ���������� �������� �������� �����
        dialogueText.text = "";
    }

    void Update()
    {
        // ������� ������� ��� ������� �� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines(); // ��������� ������� ������� ������ �������
            StartCoroutine(DisplayNextSentence()); // ��������� � ��������� �������
        }
    }
}