using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCamera : MonoBehaviour
{
    public Transform _LookAt;
    public Transform _Destination;


    [SerializeField]   private float _Interpolation, _RotatonInterpolation;

    public float _Shake;


    [SerializeField] private GameObject _Death;
    private bool _Trigger;
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _Destination.position, _Interpolation * Time.fixedDeltaTime);
        
        transform.rotation = Quaternion.Lerp(transform.rotation,_Destination.rotation, _RotatonInterpolation * Time.fixedDeltaTime);     
    }

    private void Update()
    {
        if (_Shake > 0)
        {
            _Shake-= Time.deltaTime * 5;
            transform.position = transform.position + Random.insideUnitSphere * _Shake;
            if (_Shake <= 0)
            {
                _Trigger = true;
            }
        }

        if (_Trigger)
        {
            _Death.SetActive(true);
        }     
    }
}
