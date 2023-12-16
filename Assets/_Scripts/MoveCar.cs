using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    private Vector3? _destination;
    private Vector3 _startPosition;
    private float _totalLerpDuration;
    private float _elapsedlerpDuration;
    private Action _onCompleteCallback;

    void Update()
    {
        if (_destination.HasValue == false)
            return;

        if (_elapsedlerpDuration >= _totalLerpDuration && _totalLerpDuration > 0)
            return;

        _elapsedlerpDuration = Time.deltaTime;
        float percent = _elapsedlerpDuration / _totalLerpDuration; 
        //percent = Mathf.Clamp01(percent);

        transform.position = Vector3.Lerp(_startPosition, _destination.Value, percent);

        if (_elapsedlerpDuration >= _totalLerpDuration)
            _onCompleteCallback?.Invoke();
    }
}
