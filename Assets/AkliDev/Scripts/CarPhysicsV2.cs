using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsV2 : MonoBehaviour
{
    private BoxCollider _BoxCollider;

    private GetVertexPositionsOfBoxCollider _VertexPositionsOfBoxCollider;
    public Vector3[] _VertexPositions;

    private Vector3 _RaycastPosition;

    [SerializeField]
    public float _RaycastDistence;

    [SerializeField]
    public Vector3 _SurviceNormal, _PreSurviceNormal;

    [SerializeField]
    private float _CompressionRatio, _PreCompressionRatio;

    [SerializeField]
    private float _SuspensionForceMultipleir, _SuspensionFrictionMultiplier, _GravityMultiplier;
    [SerializeField]
    private bool _Grounded, _EngineIsOn;
    [SerializeField]
    private Vector3 _SuspensionGravityVelocity, _GravityForceDirection, _SuspensionForceDirection;
    [SerializeField]


    public Transform _Model;


    [SerializeField]
    private Mesh _HitMesh;
    [SerializeField]
    private int _HitTriIndex, _Index;
    [SerializeField]
    private int[] _TriVertIndices;


    void Start()
    {
        _BoxCollider = GetComponent<BoxCollider>();
       
        _VertexPositionsOfBoxCollider = GetComponent<GetVertexPositionsOfBoxCollider>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _EngineIsOn ^= true;
        }
       
    }
    private void FixedUpdate()
    {
        Suspension();
        Gravity();
        ElaborateMovement();
    }
    private void Gravity()
    {
        _SuspensionGravityVelocity -= _GravityForceDirection * Time.fixedDeltaTime;
    }
    private void ElaborateMovement()
    {
        transform.Translate(_SuspensionGravityVelocity *Time.fixedDeltaTime, Space.World);
    }
    private void Suspension()
    {
        RaycastInformarionAtDesirablePosition();

        GroundCheck();
        
        RotateObjectToNormal(_SurviceNormal);

        _GravityForceDirection = CalculateGravityDirection(_SurviceNormal);

        if (_EngineIsOn)
        {
            _SuspensionForceDirection = CalculateSuspensionForceDirection(_SurviceNormal);
        }
        else
        {
            _SuspensionForceDirection = _GravityForceDirection + (_GravityForceDirection * 0.01f);
        }
        
        if (_CompressionRatio == 1 && _CompressionRatio != _PreCompressionRatio)
        {
            _SuspensionGravityVelocity = Vector3.zero;
        }

        ElaborateSuspensionForce(_CompressionRatio); 

        if (_Grounded && _EngineIsOn)
        {
            FrictionDuringSuspension();
        }
       
        _PreCompressionRatio = _CompressionRatio;
    }
    
    private void RaycastInformarionAtDesirablePosition()
    {
        _VertexPositions = _VertexPositionsOfBoxCollider._VertexPositions;
 
        float yOffset = transform.lossyScale.y;

         _RaycastPosition = ((_VertexPositions[2] + _VertexPositions[3] + _VertexPositions[6] + _VertexPositions[7]) * 0.25f);

        RaycastHit hit = new RaycastHit();
    
        if (Physics.Raycast(_RaycastPosition + (transform.up * yOffset), -transform.up, out hit, (_RaycastDistence + yOffset)))
        {
            if (_HitMesh != hit.collider.gameObject.GetComponent<MeshCollider>().sharedMesh)
            {
                _HitMesh = hit.collider.gameObject.GetComponent<MeshCollider>().sharedMesh;              
            }

            _HitTriIndex = hit.triangleIndex;

            int index = _HitTriIndex * 3;

            _TriVertIndices[0] = _HitMesh.triangles[index];
            _TriVertIndices[1] = _HitMesh.triangles[index + 1];
            _TriVertIndices[2] = _HitMesh.triangles[index + 2];




            _CompressionRatio = CalculateCompressionRatio(hit.distance - yOffset);          
            _SurviceNormal = hit.normal;
            _PreSurviceNormal = _SurviceNormal;
        }
        else
        {
            _CompressionRatio = 0;
            //_SurviceNormal = Vector3.zero;
        } 
    }
    private void GroundCheck()
    {
        if (_CompressionRatio > 0)
        {
            _Grounded = true;
        }
        else
        {
            _Grounded = false;
        }
       
    }
 
    private void RotateObjectToNormal(Vector3 hitNormal) 
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, (hitNormal)) * transform.rotation;
        //_Model.rotation = Quaternion.Lerp(_Model.rotation, transform.rotation, 7 * Time.fixedDeltaTime);

        _Model.rotation = transform.rotation;      
    }
    private float CalculateCompressionRatio(float hitDistance)
    {
        float absoluteValeu = 1;
        if (hitDistance > 0)
        {
            float compressionPercentege = (hitDistance / _RaycastDistence);
            return absoluteValeu - compressionPercentege;
        }
        return 1;
    }
    private Vector3 CalculateGravityDirection(Vector3 hitNormal)
    {
        Vector3 gravityDirection = hitNormal * _GravityMultiplier;
        return gravityDirection;
    }
    private Vector3 CalculateSuspensionForceDirection(Vector3 hitNormal)
    {
        Vector3 suspensionForceDirection = hitNormal * _SuspensionForceMultipleir;
        return suspensionForceDirection;
    }
    private void ElaborateSuspensionForce(float compressionRatio)
    {
        _SuspensionGravityVelocity += _SuspensionForceDirection * compressionRatio * Time.fixedDeltaTime;
    }
    private void FrictionDuringSuspension()
    {
        _SuspensionGravityVelocity -= _SuspensionFrictionMultiplier * _SuspensionGravityVelocity.normalized * Time.fixedDeltaTime;
    }

    void OnDrawGizmosSelected()
    {
        float yOffset = transform.lossyScale.y;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_RaycastPosition + (transform.up * yOffset), -transform.up * (_RaycastDistence + yOffset));


        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(_RaycastPosition + (transform.up * yOffset), -transform.up, out hit, (_RaycastDistence + yOffset)))
        {
            if (_HitMesh != hit.collider.gameObject.GetComponent<MeshCollider>().sharedMesh)
            {
                _HitMesh = hit.collider.gameObject.GetComponent<MeshCollider>().sharedMesh;
            }

            _CompressionRatio = CalculateCompressionRatio(hit.distance - yOffset);
            _HitTriIndex = hit.triangleIndex;
            _SurviceNormal = hit.normal;
            _PreSurviceNormal = _SurviceNormal;

             _Index = _HitTriIndex * 3;

            _TriVertIndices[0] = _HitMesh.triangles[_Index];
            _TriVertIndices[1] = _HitMesh.triangles[_Index + 1];
            _TriVertIndices[2] = _HitMesh.triangles[_Index + 2];


            _TriVertIndices[3] = _HitMesh.triangles[(_HitTriIndex + 1) * 3 + 0];
            _TriVertIndices[4] = _HitMesh.triangles[(_HitTriIndex + 1) * 3 + 1];
            _TriVertIndices[5] = _HitMesh.triangles[(_HitTriIndex - 1) * 3 +1];
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[0]]), 0.1f);
        Gizmos.DrawRay(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[0]]), _HitMesh.normals[_TriVertIndices[0]] * 4);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[1]]), 0.1f);
        Gizmos.DrawRay(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[1]]), _HitMesh.normals[_TriVertIndices[1]] * 4);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[2]]), 0.1f);
        Gizmos.DrawRay(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[2]]), _HitMesh.normals[_TriVertIndices[2]] * 4);
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[3]]), 0.1f);
        //Gizmos.DrawRay(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[3]]), _HitMesh.normals[_TriVertIndices[3]] * 4);
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawSphere(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[4]]), 0.1f);
        //Gizmos.DrawRay(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[4]]), _HitMesh.normals[_TriVertIndices[4]] * 4);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[5]]), 0.1f);
        //Gizmos.DrawRay(hit.transform.TransformPoint(_HitMesh.vertices[_TriVertIndices[5]]), _HitMesh.normals[_TriVertIndices[5]] * 4);


    }
}
