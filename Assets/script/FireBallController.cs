using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FireballController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement de la boule de feu
    public float lifetime = 3f; // Temps de vie de la boule de feu en secondes
    private Vector3 direction;
    public SpriteRenderer SpriteRenderer;

    void Start()
    {
        // Détruit la boule de feu après "lifetime" secondes
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Déplace la boule de feu dans la direction vers laquelle elle est orientée
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
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>(); // Récupère le script de santé de l'ennemi
            if (enemy != null)
            {
                enemy.TakeDamage(10); // Inflige des dégâts à l'ennemi
            }
            Destroy(gameObject);
        }
    }
}