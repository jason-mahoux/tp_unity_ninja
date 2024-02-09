using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class Idle1Click : MonoBehaviour
{
    public CharacterData CharacterData;
    public SpriteRenderer spriteRenderer;

    public void OnMouseDown()
    {
        SceneManager.LoadScene("Level1");
        GameManager.Instance.PlayerBehaviour.CharacterData = CharacterData;
        GameManager.Instance.PlayerBehaviour.SpriteRenderer.sprite = CharacterData.DefaultSprite;
        GameManager.Instance.PlayerBehaviour.Animator.runtimeAnimatorController = CharacterData.Animator;
    }
}
