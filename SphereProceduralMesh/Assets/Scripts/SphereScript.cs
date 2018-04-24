using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class SphereScript : MonoBehaviour {

    private Vector3 center;
    private float radius = 1f;
    private float timeMod = .5f;
    private float timer = 0;
    private float scaler = 2f;
    private bool usingNoise = false;

    private void Update()
    {

        center = transform.position;
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshCreator mc = new MeshCreator(center);

        timer += Time.deltaTime * timeMod;
        if(timer > timeMod * 5)
        {
            usingNoise = true;
        }
        if(timer > 255)
        {
            timer = 0;
        }

        float noiseValue;

        float t = (radius + Mathf.Sqrt(5f)) / 2f;

        List<Vector3> verts = new List<Vector3>();
        verts.Add( new Vector3(-radius, t, 0));
        verts.Add(new Vector3(radius, t, 0));
        verts.Add(new Vector3(-radius, -t, 0));
        verts.Add(new Vector3(radius, -t, 0));


        verts.Add(new Vector3(0, -radius, t));
        verts.Add(new Vector3(0, radius, t));
        verts.Add(new Vector3(0, -radius, -t));
        verts.Add(new Vector3(0, radius, -t));


        verts.Add( new Vector3(t, 0, -radius));
        verts.Add( new Vector3(t, 0, radius));
        verts.Add(new Vector3(-t, 0, -radius));
        verts.Add(new Vector3(-t, 0, radius));



        if (usingNoise)
        {
            for(int i = 0; i < verts.Count; i++)
            {
                noiseValue = Perlin.Noise(verts[i].x , verts[i].y, timer);
                verts[i] += (center - verts[i]).normalized * noiseValue * scaler;
            }
            
        }

        mc.BuildTriangle(verts[5], verts[11], verts[0]);
        mc.BuildTriangle(verts[1], verts[5], verts[0]);
        mc.BuildTriangle(verts[7], verts[1], verts[0]);
        mc.BuildTriangle(verts[10], verts[7], verts[0]);
        mc.BuildTriangle(verts[11], verts[10], verts[0]);

        mc.BuildTriangle(verts[9], verts[5], verts[1]);
        mc.BuildTriangle(verts[4], verts[11], verts[5]);
        mc.BuildTriangle(verts[2], verts[10], verts[11]);
        mc.BuildTriangle(verts[6], verts[7], verts[10]);
        mc.BuildTriangle(verts[8], verts[1], verts[7]);

        mc.BuildTriangle(verts[4], verts[9], verts[3]);
        mc.BuildTriangle(verts[2], verts[4], verts[3]);
        mc.BuildTriangle(verts[6], verts[2], verts[3]);
        mc.BuildTriangle(verts[8], verts[6], verts[3]);
        mc.BuildTriangle(verts[9], verts[8], verts[3]);

        mc.BuildTriangle(verts[5], verts[9], verts[4]);
        mc.BuildTriangle(verts[11], verts[4], verts[2]);
        mc.BuildTriangle(verts[10], verts[2], verts[6]);
        mc.BuildTriangle(verts[7], verts[6], verts[8]);
        mc.BuildTriangle(verts[1], verts[8], verts[9]);



        meshFilter.mesh = mc.CreateMesh();
    }
}
