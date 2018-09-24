using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeVertexNormals : MonoBehaviour
{
    [SerializeField] private bool _ShowVertexNormals;
    private void OnDrawGizmosSelected()
    {
        if (_ShowVertexNormals)
        {

            Gizmos.color = Color.magenta;
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;


            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                Gizmos.DrawRay(transform.TransformPoint(mesh.vertices[i]), mesh.normals[i] * 3);
            }
        }
    }
}
