using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace Jobs.OOD
{
    public class OOD : MonoBehaviour
    {
        public int xHalfCount = 40;
        public int zHalfCount = 40;

        public float A = 1;
        public float B = 3;
        public float C = 0.2f;

        List<Transform> cubesList;

        static readonly ProfilerMarker<int> profilerMarker = new ProfilerMarker<int>("Wave", "Object Count");

        void Start()
        {
            cubesList = new List<Transform>();
            for (int i = -xHalfCount; i <= xHalfCount; i++)
            {
                for (int j = -zHalfCount; j <= zHalfCount; j++)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(i * 1.1f, 0, j * 1.1f);
                    var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    material.SetColor("_BaseColor", Color.blue);
                    cube.GetComponent<MeshRenderer>().material = material;
                    cubesList.Add(cube.transform);
                }
            }
        }
        void Update()
        {
            using (profilerMarker.Auto(cubesList.Count))
            {
                for (int i = 0; i < cubesList.Count; i++)
                {
                    var distance = Vector3.Distance(cubesList[i].position, Vector3.zero);
                    cubesList[i].localPosition += Vector3.up * A * Mathf.Sin(B * Time.time  + C * distance);
                }
            }
        }
    }
}