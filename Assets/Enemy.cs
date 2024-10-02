
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float chaseSpeed = 40f;
    public float defaultSpeed = 20f;
    private Vector3 originalScale;

    private float speed;
    public float detectionDistance = 25f;  

    public Animator animator;
    public Animator animatorPlayer;

    public float detectionDistancePl = 35f;
    private Vector2 moveDirection = Vector2.right;  
    public GameObject player;

    public bool isRaycast = true;
    public Sprite burnedSprite;  

    public AudioSource fireSound;

    private SpriteRenderer spriteRenderer; 

    void Awake()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();  
        player =  GetComponent<GameObject>();
    }
    
    void Update()
    {
        if (isRaycast){
            RaycastHit2D hitFront = Physics2D.Raycast(transform.position, moveDirection, detectionDistance);
            Vector2 rightDirection = Vector2.Perpendicular(moveDirection);  
            Vector2 leftDirection = -rightDirection;  
            Vector2 downDirection = -moveDirection;  
            
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightDirection, detectionDistance);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftDirection, detectionDistance);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, downDirection, detectionDistance);

            RaycastHit2D hitFrontPl = Physics2D.Raycast(transform.position, moveDirection, detectionDistancePl);
            RaycastHit2D hitRightPl = Physics2D.Raycast(transform.position, rightDirection, detectionDistancePl);
            RaycastHit2D hitLeftPl = Physics2D.Raycast(transform.position, leftDirection, detectionDistancePl);
            RaycastHit2D hitDownPl = Physics2D.Raycast(transform.position, downDirection, detectionDistancePl);

            if (hitFrontPl.collider != null && hitFrontPl.collider.CompareTag("Player"))
            {  
                speed = chaseSpeed;
            }
            else if (hitRightPl.collider != null && hitRightPl.collider.CompareTag("Player"))
            {
                speed = chaseSpeed;
                moveDirection = rightDirection; 
            }
            else if (hitLeftPl.collider != null && hitLeftPl.collider.CompareTag("Player"))
            {
                speed = chaseSpeed;
                moveDirection = leftDirection; 
            }
            else if (hitDownPl.collider != null && hitDownPl.collider.CompareTag("Player"))
            {
                speed = chaseSpeed;
                moveDirection = downDirection;  
            }
            else
            {
                Debug.Log("Player NOT Detected");
                speed = defaultSpeed;
                if (hitFront.collider != null && hitFront.collider.CompareTag("Wall"))
                {
                    if (hitRight.collider == null)
                    {
                        moveDirection = rightDirection;
                    }
                    else if (hitLeft.collider == null)
                    {
                        moveDirection = leftDirection;
                    }
                    else if (hitDown.collider == null)
                    {
                        moveDirection = downDirection;
                    }
                    else
                    {
                        moveDirection = -moveDirection;
                    }
                }
            }

            transform.Translate(moveDirection * speed * Time.deltaTime);

            if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
        }
    }


    void OnDrawGizmos()
    {
        if (isRaycast){
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDirection * detectionDistancePl);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)Vector2.Perpendicular(moveDirection) * detectionDistancePl);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(-Vector2.Perpendicular(moveDirection)) * detectionDistancePl);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(-moveDirection) * detectionDistancePl); // вниз


            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDirection * detectionDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)Vector2.Perpendicular(moveDirection) * detectionDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(-Vector2.Perpendicular(moveDirection)) * detectionDistance);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(-moveDirection) * detectionDistance); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {       
            animator.SetBool("isOnPlayer", true);
            StartCoroutine(DestroyCharacterAnimation());
        }
        if (other.CompareTag("Bullet"))
        {
            isRaycast = false;
            animator.SetBool("isDead", true);
            Debug.Log(animator.GetBool("isDead"));

            DisableAllColliders();

            StartCoroutine(DestroyAfterAnimation());

            Destroy(other.gameObject);
        }
    }

    private void DisableAllColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.enabled = false;

        spriteRenderer.sprite = burnedSprite;
    }

    private IEnumerator DestroyCharacterAnimation()
    {
        fireSound.Play();
        Debug.Log("AnimatorPl");
        yield return new WaitForSeconds(0.517f);
        animator.SetBool("isOnPlayer", false);

    }
}