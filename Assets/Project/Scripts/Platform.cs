using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private Vector3 _point1;
    private Vector3 _point2;

    private float _lerpPct;

    private void Start()
    {
        _point1 = point1.position;
        _point2 = point2.position;
        _lerpPct = 0;
    }

    private void Update()
    {
        _lerpPct = Mathf.Clamp(_lerpPct + speed * Time.deltaTime, 0f, 1f);

        if (_lerpPct >= 1 || _lerpPct <= 0)
            speed *= -1f;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(point1.position, point2.position, _lerpPct);
    }
}
