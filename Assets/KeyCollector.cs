using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCollector : MonoBehaviour
{
    public static int keyCount = 0;
    public static bool doorUnlocked = false;
    public TextMeshProUGUI text;
    private TextMeshProUGUI text2;

    [Header("Sounds Settings")]
    public AudioSource keyAudio;

    public delegate void KeyCollectedHandler();
    public static event KeyCollectedHandler OnKeyCollected;

    private GameObject keyText;
    

    private void Start()
    {
        keyCount = 0;
        doorUnlocked = false;
        keyText = GameObject.FindGameObjectWithTag("Keytext");
        text2 = keyText.GetComponent<TextMeshProUGUI>();
        UpdateKeyUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnKeyCollected != null)
            {
                OnKeyCollected.Invoke();
            }

            keyCount++;
            UpdateKeyUI();

            if (keyCount >= 3)
            {
                UnlockDoor(); 
            }

            // Запускаем корутину для воспроизведения звука и удаления объекта
            StartCoroutine(PlayKeySoundAndDestroy());
        }
    }

    private void CreateNew()
    {

    }

    // Корутина для воспроизведения звука и удаления объекта
    private IEnumerator PlayKeySoundAndDestroy()
    {
        if (keyAudio != null && keyAudio.clip != null)
        {
            keyAudio.Play(); // Проигрываем звук ключа
            yield return new WaitForSeconds(keyAudio.clip.length); // Ждем завершения звука
        }
        else
        {
            Debug.LogError("AudioSource или AudioClip не настроены!");
        }

        Destroy(gameObject); // Уничтожаем объект
    }

    private void UpdateKeyUI()
    {
        Debug.Log(keyCount);

        // if (text != null)
        //{
        //    text.text = "Keys: " + keyCount + " / 3";
        //}
        text2.text = "Keys: " + keyCount + " / 3";
    }

    private void UnlockDoor()
    {
        Debug.Log("Door unlocked");
        doorUnlocked = true; 
    }
}
