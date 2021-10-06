using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _collectedCoins;

    private void Awake()
    {
        _collectedCoins = 0;
        _text.SetText("Collected Coins: " + _collectedCoins);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _collectedCoins += 1;
            _text.SetText("Collected Coins: " + _collectedCoins);
            Destroy(coin.gameObject);
        }
    }
}
