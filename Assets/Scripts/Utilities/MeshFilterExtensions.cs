using UnityEngine;

namespace AbstractDefence.Utilities
{
    public static class MeshFilterExtensions
    {
        public static Vector2 GetUV(this MeshFilter meshFilter, Vector3 worldPoint)
        {
            Mesh mesh = meshFilter.sharedMesh;
            Vector3 localPoint = meshFilter.transform.InverseTransformPoint(worldPoint);
            Bounds bounds = mesh.bounds;
            float u = Mathf.InverseLerp(bounds.min.x, bounds.max.x, localPoint.x);
            float v = Mathf.InverseLerp(bounds.min.z, bounds.max.z, localPoint.z);
            return new Vector2(u, v);
        }
    }
}