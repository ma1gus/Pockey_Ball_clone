using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class Ball : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _powerScale;
    [SerializeField] private BallSnapPoint _snapPoint;
    [SerializeField] private StickAnimator _stick;
    [SerializeField] private Renderer _stickRenderer;
    [SerializeField] private TMP_Text _text;


    private Rigidbody _rigidbody;
    private Vector3 _startTapPosition;
    private Vector3 _currentTapPosition;
    private Vector3 _tempBallPosition;
    private float _power;

    public UnityAction<float> PowerChanged;
    public UnityAction BallBlocked;
    public UnityAction LevelFinished;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _power = 0;
    }

    private void Update()
    {
        if (_rigidbody.isKinematic)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetTempBallPosition();
            }
            if (Input.GetMouseButton(0))
            {
                _currentTapPosition = Input.mousePosition;
                _power = (_startTapPosition.y - _currentTapPosition.y) * _powerScale;
                _power = Mathf.Clamp(_power, 0, 1);
                PowerChanged?.Invoke(_power);
            }
            if (Input.GetMouseButtonUp(0))
            {
                transform.position = _tempBallPosition;

                FixBall(false);

                _rigidbody.AddForce(Vector3.up * _jumpForce * _power, ForceMode.Impulse);

                transform.SetParent(null);
                _stick.transform.SetParent(transform);

                _power = 0;
                PowerChanged?.Invoke(_power);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(transform.position, Vector3.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.TryGetComponent(out Block block))
                    {
                        BallBlocked?.Invoke();
                    }
                    else if (hitInfo.collider.TryGetComponent(out Segment segment))
                    {
                        SetTempBallPosition();

                        _stick.transform.SetParent(null);
                        transform.SetParent(_snapPoint.transform);

                        FixBall(true);
                    }
                    else if (hitInfo.collider.TryGetComponent(out Finish finish))
                    {
                        LevelFinished?.Invoke();
                    }
                }
            }
        }
    }

    private void SetTempBallPosition()
    {
        _tempBallPosition = transform.position;
        _startTapPosition = Input.mousePosition;
    }

    public void FixBall(bool isFixed)
    {
        _rigidbody.isKinematic = isFixed;
        _stickRenderer.enabled = isFixed;

        if(isFixed)
            _rigidbody.velocity = Vector3.zero;
    }
}
