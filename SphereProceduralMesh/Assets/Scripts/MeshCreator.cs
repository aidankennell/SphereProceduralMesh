using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator{
    private List<Vector3> verts = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<int> triangleIndices = new List<int>();
    private Vector3 center;

    public MeshCreator(Vector3 center)
    {
        this.center = center;
    }


    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        int v0Index = verts.Count;
        int v1Index = verts.Count + 1;
        int v2Index = verts.Count + 2;

        // Put vertex data into verts array 
        verts.Add(vertex0);
        verts.Add(vertex1);
        verts.Add(vertex2);
        
        // Use the same normal for all verts (i.e., a surface normal) 
        // Could change function signature to pass in 3 normals if needed  
        normals.Add(findNormal(vertex0));
        normals.Add(findNormal(vertex1));
        normals.Add(findNormal(vertex2));

        // Use standard uv coordinates  
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));

        // Add integer pointers to verts into triangles list 
        triangleIndices.Add(v0Index);
        triangleIndices.Add(v1Index);
        triangleIndices.Add(v2Index);

    }

    //public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    //{
    //    Vector3 normal = Vector3.Cross(vertex0 - vertex1, vertex2 - vertex1).normalized * -1f;
    //    BuildTriangle(vertex0, vertex1, vertex2, normal);
    //}


    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangleIndices.ToArray();
        return mesh;

    }
    
    private void midPoint(int v1, int v2)
    {

    }

    private void refineASphere()
    {
        //foreach()
    }


    private Vector3 findNormal(Vector3 point)
    {
        return (center - point).normalized;
    }

}
