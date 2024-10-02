using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    private Vector2 moveVector;
    private bool isColliding = false;  
    private Vector2 collisionNormal; 
    bool isDead = false;
    [Header("Player Animation Settings")]
    public Animator animator;
    private Vector3 originalScale;
    private Vector2 moveDirection; 
    public GameObject bulletPrefab; 
    public Transform firePoint;     

    [Header("Shooting Settings")]
    public float fireCooldown = 5f; 
    private float nextFireTime = 0f; 

    [Header("Sounds Settings")]
<<<<<<< HEAD
    public AudioSource shootAudio;
=======
    public AudioSource shootAudio, stepAudio, deathAudio;
    private bool isStepAudioPlaying = false;
>>>>>>> 0d61615851b1f717fd77bffd8b597f5115de5b99
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; 
        stepAudio.loop = true;
    }

    void Shoot()
    {   
        if (moveVector.magnitude == 0)
        {
            return;
        }
        shootAudio.Play();
        firePoint.position = new Vector3(rb.position.x, rb.position.y);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetMoveDirection(moveDirection);
    }

    void Update()
    {
        if(isDead){
            return;
        }
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");

        if (isColliding)
        {
            if (Vector2.Dot(moveVector, collisionNormal) < 0)
            {
                moveVector = Vector2.zero;
            }
        }

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
         
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireCooldown; 
        }

        rb.MovePosition(rb.position + moveVector * speed * Time.deltaTime);

        if (moveVector.magnitude > 0.1f) 
        {
            animator.SetBool("isWalking", true);
            if(!isStepAudioPlaying){
                stepAudio.Play();
                isStepAudioPlaying = true;
            }
        }
        else 
        {
            animator.SetBool("isWalking", false);
            if(isStepAudioPlaying){
                stepAudio.Stop();
                isStepAudioPlaying = false;
            }
        }

        if (moveVector.x < 0) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveVector.x > 0) 
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

<<<<<<< HEAD
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key")) 
        {
            Debug.Log("Sound");
        }
    }

=======
>>>>>>> 0d61615851b1f717fd77bffd8b597f5115de5b99
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall")) 
        {
            isColliding = true;
            collisionNormal = (rb.position - (Vector2)other.ClosestPoint(rb.position)).normalized;
        }
        if (other.CompareTag("Enemy"))
        {   
            deathAudio.Play();
            isDead = true;
            Collider2D plCol = GetComponent<Collider2D>();
            animator.SetBool("isDead", true);

            Debug.Log(animator.GetBool("isDead"));
            plCol.enabled = false;
            StartCoroutine(DestroyCharacterAnimation());
           
        }
    }
    private IEnumerator DestroyCharacterAnimation()
    {

        Debug.Log("AnimatorPl");
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        SceneManager.LoadScene("LoseMenu");


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isColliding = false;
        }
    }
}
