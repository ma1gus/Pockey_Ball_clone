using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Ball _ball;

    private void OnEnable()
    {
        _ball.PowerChanged += OnPowerChanged;
    }

    private void OnDisable()
    {
        _ball.PowerChanged -= OnPowerChanged;
    }

    private void OnPowerChanged(float power)
    {
        _animator.SetFloat("Blend", power);
    }
}
