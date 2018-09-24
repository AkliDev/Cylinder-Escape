using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public CarController _Cont;
    private ParticleSystem _P;
    // Use this for initialization
    void Start()
    {
        _P = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Cont.Dir == 1)
        {
            _P.startColor = Color.white;


        }
        else
        {
            _P.startColor = Color.green;
        }
    }
}
