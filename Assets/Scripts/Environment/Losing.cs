using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Losing : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Ball ball))
        {
            ball.enabled = false;
            _text.SetText("You lose");
        }
    }
}
