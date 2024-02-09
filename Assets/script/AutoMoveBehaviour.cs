using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoMoveBehaviour : MonoBehaviour
{

    public float Dist = 5f;
    public float TravelTime = 1.5f;

    private float _time;
    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;
    public SpriteRenderer SpriteRenderer;

    void Start()
    {
        _time = TravelTime / 2f;
        _sourcePosition = transform.position;
        _targetPosition = transform.position + new Vector3(Dist, 0f);
    }

    void Update()
    {
        _time += Time.deltaTime;
        transform.position = Vector3.Lerp(_sourcePosition, _targetPosition, _time / TravelTime);

        if (_time >= TravelTime)
        {
            Dist *= -1;
            _time = 0f;
            _sourcePosition = _targetPosition;
            _targetPosition += new Vector3(2 * Dist, 0f);
        }

        if (Dist < 0)
        {
            SpriteRenderer.flipX = false;
        }
        else
        {
            SpriteRenderer.flipX = true;
        }
    }
}
