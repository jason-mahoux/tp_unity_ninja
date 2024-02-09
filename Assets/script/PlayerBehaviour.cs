using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{

    public CharacterData CharacterData;
    private Rigidbody2D _rb;
    private bool _isGrounded;
    public LayerMask CollisionMask;
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    public GameObject fireballPrefab;
    private bool _isAtacking = false;
    public Coroutine Coroutine;
    public int CurrentHP;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = false;
        CurrentHP = CharacterData.MaxHP;
        GameManager.Instance.PlayerBehaviour = this;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, CollisionMask);
        _isGrounded = hit.collider != null;
        Animator.SetFloat("VSpeed", _rb.velocity.y);

        if (Input.GetKey(KeyCode.A)) // La touche Q
        {
            Animator.SetBool("IsRunning", true);
            SpriteRenderer.flipX = true;
            _rb.velocity = new Vector2(-CharacterData.Speed, _rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Animator.SetBool("IsRunning", true);
            SpriteRenderer.flipX = false;
            _rb.velocity = new Vector2(CharacterData.Speed, _rb.velocity.y);

        }
        else
        {
            Animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * CharacterData.JumpSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("player_crouch", true);
        }
        else
        {
            Animator.SetBool("player_crouch", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && _isGrounded)
        {
            Animator.Play("player_attack");
            _isAtacking = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && _isGrounded)
        {
            Animator.Play("strick_attack");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Lancer la boule de feu
            Vector2 direction = SpriteRenderer.flipX ? Vector2.left : Vector2.right;
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.GetComponent<FireballController>().SetDirection(direction);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAtacking && other.CompareTag("Enemy")) // Si le joueur est en train d'attaquer et entre en collision avec un ennemi
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>(); // Récupère le script de santé de l'ennemi
            if (enemy != null)
            {
                enemy.TakeDamage(10); // Inflige des dégâts à l'ennemi
            }
        }
    }

    public float GetHPRatio()
    {
        return CurrentHP / (float)CharacterData.MaxHP;
    }

    public void OnDamage(int  damageAmount)
    {
        if (Coroutine == null)
            Coroutine = StartCoroutine(DamageAsync(damageAmount));
    }

    public void StopDamage()
    {
        //Animator.SetBool("IsStop", false);
    }

    public IEnumerator DamageAsync(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP <= 0)
            GameManager.Instance.LoseLife();
        //Animator.Play($"damage{PlayerPrefs.GetInt("IsPlayer")}");
        //Animator.SetBool("IsStop", true);
        yield return new WaitForSeconds(2);
        StopDamage();
        Coroutine = null;
    }
}
