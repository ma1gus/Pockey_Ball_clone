using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _templateCoin;
    [SerializeField] [Range(0, 100)] private float _coinSpawnChance;
    [SerializeField] private int _firstBlockToSpawnCoin;

    private static int _blockNumber = 1;

    private void Start()
    {
        _blockNumber += 1;
        if (Random.Range(0, 100) < _coinSpawnChance && _blockNumber > _firstBlockToSpawnCoin)
        {
            Instantiate(_templateCoin, transform.position, Quaternion.Euler(90, Random.Range(0, 360), 0));
        }
    }
}
