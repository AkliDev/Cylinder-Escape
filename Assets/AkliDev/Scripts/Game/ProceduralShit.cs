using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralShit : MonoBehaviour
{

    public float _Speed;

    void Start()
    {

    }


    void Update()
    {
        _Speed += Time.deltaTime * 10;
    }
}
