using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;

    private float _movementFactor;
    private Vector3 _startingPosition;

    // Start is called before the first frame update
    void Start() {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (period <= Mathf.Epsilon)
            return;
        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        _movementFactor = rawSinWave;
        Vector3 offset = movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}