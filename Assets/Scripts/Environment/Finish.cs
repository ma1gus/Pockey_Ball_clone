using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private TMP_Text _text;
    private Ball _ball;

    private void Awake()
    {
        _text = FindObjectOfType<FinishView>().GetComponent<TMP_Text>();
        _ball = FindObjectOfType<Ball>();
    }

    private void OnEnable()
    {
        _ball.LevelFinished += OnFinished;
    }

    private void OnDisable()
    {
        _ball.LevelFinished -= OnFinished;
    }

    private void OnFinished()
    {
        _ball.FixBall(true);
        _ball.enabled = false;
        _text.SetText("Level Finished");
    }
}
