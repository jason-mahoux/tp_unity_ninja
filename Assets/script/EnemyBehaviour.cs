using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyData EnemyData;
    public int CurrentHP;
    public Coroutine Coroutine;
    public int Damage;

    public void Start()
    {
        CurrentHP = EnemyData.MaxHP;
        GameManager.Instance.EnemyBehaviour = this;
    }

    public float GetHPRatio()
    {
        return CurrentHP / (float) EnemyData.MaxHP;
    }

    public virtual void TakeDamage(int  damage)
    {
        if (Coroutine == null)
            Coroutine = StartCoroutine(DamageAsync(damage));
    }


    public IEnumerator DamageAsync(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
            Instantiate(GameManager.Instance.BearPrefab, new Vector2(32.43f, -29f), Quaternion.identity);
        }

        yield return new WaitForSeconds(2);
        Coroutine = null;
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerBehaviour>().OnDamage(Damage);
        }
    }

}
