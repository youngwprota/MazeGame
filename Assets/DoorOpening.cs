using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    private Animator animator;
    public AudioSource doorAudio;
    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        if (KeyCollector.doorUnlocked)
        {
            doorCollider.enabled = true;
            animator.SetBool("isOpen", false);
            KeyCollector.doorUnlocked = false;
        }
    }

    private void Update()
    {
        if (KeyCollector.doorUnlocked && doorCollider != null)
        {
            doorAudio.Play();
            //doorCollider.enabled = false;
            animator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && KeyCollector.doorUnlocked)
        {
            SceneManager.LoadScene("WinMenu");
            Debug.Log("Game Over.");
        }
    }
}
