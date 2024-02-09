using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public int MaxHP;
    public float Speed;
    public float JumpSpeed;
    public Sprite DefaultSprite;
    public AnimatorController Animator;
}
