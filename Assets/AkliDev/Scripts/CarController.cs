using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    
    public bool _IsUsingController;

    private CarPhysics _Physics;

    [SerializeField]
    public float _Acceleration, _FrictionMultiplier, _MaxVelocity, _SpeedPerFrame;
    [SerializeField]
    private float _HorizontalAxis, _TurnSensitivity, _VerticalAxis, _LTrigger, _RTrigger;
    
   
    [SerializeField]
    public float _Velocity, _PreVelocity, Dir;

    

    public Material _Mat1;
    public Material _Mat2;
    public Material _Mat3;
    public Material _Mat4;

    private AudioSource _Audio;
    private static bool didQueryNumOfCtrlrs = false;

    private float m_pitchDest, m_pitch;

    private bool _Button;
    void Start()
    {
        Dir = 1;
        _Physics = GetComponent<CarPhysics>();
        _Audio = GetComponent<AudioSource>();
        m_pitch = 0;
        m_pitchDest = 1;



    }
    private void FixedUpdate()
    {
        if (_Button)
        {
            ElaborateMovement();
        }
       
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_pitchDest = 1;
            Dir = -Dir;
            _Button = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_pitchDest = 2;
            _Button = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        m_pitch = Mathf.Lerp(m_pitch, m_pitchDest, 5 * Time.deltaTime);
        _Audio.pitch = m_pitch;
    }

   
    private void Accelerate()
    {
        _SpeedPerFrame = 0;
        _SpeedPerFrame -= _LTrigger * _Acceleration * Time.fixedDeltaTime;
      


    }
    private void SetVelocity()
    {
        _Velocity += _SpeedPerFrame;
        if (_Velocity > 0)
        {
            _Velocity -= _FrictionMultiplier * Time.fixedDeltaTime;
        }
        else if (_Velocity < 0)

        {
            _Velocity += _FrictionMultiplier * Time.fixedDeltaTime;
        }
    }

    private void ElaborateMovement()
    {
        

        transform.Translate(_Physics.transform.right * 50* Time.fixedDeltaTime * Dir, Space.World);


        _Physics._Model.transform.position = transform.position;
        transform.Rotate(_Physics.transform.up * _HorizontalAxis * _TurnSensitivity * Time.fixedDeltaTime, Space.World);
    }
  
    private void FrictionEnd()
    {
        if (Mathf.Sign(_PreVelocity) != Mathf.Sign(_Velocity) && _PreVelocity != 0 && _Velocity != 0)
        {
            _Velocity = 0;
            _PreVelocity = 0;
        }
        _PreVelocity = _Velocity;
    }
}
