using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private float _speed;
    [SerializeField] private int _range;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _ball.BallBlocked += OnBallBlocked;
    }

    private void OnDisable()
    {
        _ball.BallBlocked -= OnBallBlocked;
    }

    private void OnBallBlocked()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        int shakeRange = _range;
        Quaternion targetRotation;
        _ball.FixBall(true);
        _ball.enabled = false;

        while (shakeRange != 0)
        {
            targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, shakeRange);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _speed * Time.deltaTime);

            if (transform.rotation == targetRotation)
                shakeRange = (Mathf.Abs(shakeRange) - 1) * -1;

            yield return new WaitForEndOfFrame();
        }

        _ball.enabled = true;
        _ball.FixBall(false);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        _coroutine = null;
    }
}