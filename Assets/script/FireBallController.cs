using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FireballController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de d�placement de la boule de feu
    public float lifetime = 3f; // Temps de vie de la boule de feu en secondes
    private Vector3 direction;
    public SpriteRenderer SpriteRenderer;

    void Start()
    {
        // D�truit la boule de feu apr�s "lifetime" secondes
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // D�place la boule de feu dans la direction vers laquelle elle est orient�e
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized; // Normaliser la direction pour maintenir une vitesse constante
        if (direction.x < 0)
        {
            SpriteRenderer.flipX = true;
        }
        else
        {
            SpriteRenderer.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Si la boule de feu entre en collision avec un ennemi
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>(); // R�cup�re le script de sant� de l'ennemi
            if (enemy != null)
            {
                enemy.TakeDamage(10); // Inflige des d�g�ts � l'ennemi
            }
            Destroy(gameObject);
        }
    }
}