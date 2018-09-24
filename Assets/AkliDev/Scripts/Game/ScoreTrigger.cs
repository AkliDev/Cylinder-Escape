using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
   [SerializeField] private ScoreCounter _Score;

    void Start()
    {
        _Score = GameObject.Find("ScoreCounter").GetComponent<ScoreCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Score.AddScore(1);         
        }
    }

    
}
