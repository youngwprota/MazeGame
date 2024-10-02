using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeDestroy = 3f; 
    public float speed = 200f;    

    private Rigidbody2D rb;       
    private Vector2 moveVector;  
    private Vector3 originalScale; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        Invoke("DestroyBullet", timeDestroy);

        AdjustBulletDirection();

        rb.velocity = moveVector * speed;
    }

    public void SetMoveDirection(Vector2 direction)
    {   
        moveVector = direction.normalized; 
    }

    private void AdjustBulletDirection()
    {
        if (moveVector.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveVector.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveVector.y < 0)
        {
            Debug.Log("Down");
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), Mathf.Abs(originalScale.y), originalScale.z);
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (moveVector.y > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), Mathf.Abs(originalScale.y), originalScale.z);
            transform.rotation = Quaternion.Euler(0, 0, 90);    
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   if (other.CompareTag("Wall")){
            DestroyBullet();
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
