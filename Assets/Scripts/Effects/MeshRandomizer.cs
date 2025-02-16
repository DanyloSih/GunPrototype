using System;
using System.Collections.Generic;
using GunPrototype.Utilities;
using UnityEngine;

namespace GunPrototype.Weapons
{
    public class MeshRandomizer
    {
        [Serializable]
        public class Config
        {
            public float RandomRange = 0.2f;
        }

        private Vector3[] _originalVertices;
        private Mesh _mesh;
        private Config _config;

        public MeshRandomizer(Config config, MeshFilter meshFilter)
        {
            _config = config;
            _mesh = meshFilter.mesh.Clone();
            meshFilter.mesh = _mesh;
            _originalVertices = _mesh.vertices.Clone() as Vector3[];
        }

        public void Randomize()
        {
            if (_mesh == null || _originalVertices == null)
                return;

            Vector3[] vertices = new Vector3[_originalVertices.Length];
            Dictionary<Vector3, Vector3> displacementMap = new Dictionary<Vector3, Vector3>();

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 original = _originalVertices[i];

                if (!displacementMap.TryGetValue(original, out Vector3 displacement))
                {
                    displacement = UnityEngine.Random.insideUnitSphere * _config.RandomRange;
                    displacementMap[original] = displacement;
                }

                vertices[i] = original + displacement;
            }

            _mesh.vertices = vertices;
            _mesh.RecalculateNormals();
            _mesh.RecalculateBounds();
        }
    }
}