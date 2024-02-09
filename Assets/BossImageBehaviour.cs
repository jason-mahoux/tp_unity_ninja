using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossImageBehaviour : MonoBehaviour
{

    public Image Image;

    void FixedUpdate()
    {
        Image.sprite = GameManager.Instance.EnemyBehaviour.EnemyData.DefaultSprite;
    }
}
