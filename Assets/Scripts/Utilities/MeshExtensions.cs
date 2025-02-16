using UnityEngine;

namespace GunPrototype.Utilities
{
    public static class MeshExtensions
    {
        public static Mesh Clone(this Mesh originalMesh)
        {
            Mesh newMesh = new Mesh();
            newMesh.vertices = (Vector3[])originalMesh.vertices.Clone();
            newMesh.triangles = (int[])originalMesh.triangles.Clone();
            newMesh.uv = (Vector2[])originalMesh.uv.Clone();
            newMesh.normals = (Vector3[])originalMesh.normals.Clone();
            newMesh.tangents = (Vector4[])originalMesh.tangents.Clone();
            newMesh.colors = (Color[])originalMesh.colors.Clone();

            newMesh.RecalculateBounds();
            return newMesh;
        }
    }
}