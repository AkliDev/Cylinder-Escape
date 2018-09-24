using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col : MonoBehaviour
{

    [SerializeField]GameObject _Cam, Model, _Logic;
    [SerializeField] ProceduralShit _Shit;
    [SerializeField] ParticleSystem _Particle;
    private AudioSource _Audio;



    // Use this for initialization
    void Start()
    {
        _Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Dood()
    {
        this.transform.parent = null;
        _Shit._Speed = 0;
        _Shit.gameObject.SetActive(false);
        Model.SetActive(false);
        _Logic.SetActive(false);
        _Cam.GetComponent<RaceCamera>()._Shake =5.5f;
        _Particle.Play();
        _Audio.Play();

    }
}
