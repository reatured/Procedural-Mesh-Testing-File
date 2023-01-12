using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class MeshManager : MonoBehaviour
{
    private Mesh mesh;
    public PDvert[] pdVerts;

    public string debugText;
    public Transform debugSphere;
    public int debugIndex = 0;


    private ControlMeshVerts[] needles; 
    
    // Start is called before the first frame update
    void Start()
    {

        needles = GetComponentsInChildren<ControlMeshVerts>();
        //initiate array of PDverts; 
        initializeData(); 

        //initiate needles and attach verts; 
        foreach (ControlMeshVerts t in needles)
        {
            t.pdvert = getClosetVert(t.transform.localPosition);
            t.initate();
            t.meshManager = this; 
        }
        
    }
    
    public void initializeData()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        pdVerts = new PDvert[vertices.Length];
        for (int i = 0; i < pdVerts.Length; i++)
        {
            PDvert cur = new PDvert(vertices[i], i);
            pdVerts[i] = cur;
            debugText += cur.index + ": " + cur.position.x + "\n";
        }
    }


    private void OnMouseDown()
    {
        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            
                returnMeshInformation(hit);
            
        }
    }



    public void returnMeshInformation(RaycastHit hit)
    {

        Vector3 cursorPos = Vector3.zero; 
        Vector3[] vertices = mesh.vertices;
        float shortestDist = 1000;


        foreach(Vector3 vert in vertices)
        {
            float dist = (vert * 100 - hit.point).magnitude;

            if(dist < shortestDist)
            {
                cursorPos = vert; 
                shortestDist = dist;
            }
        }
        mesh.vertices = vertices;



    }
    public PDvert getClosetVert(Vector3 hitPos)
    {
        Vector3 cursorPos = Vector3.zero;
        Vector3[] vertices = mesh.vertices;
        float shortestDist = 1000;

        PDvert cloestVert = null; 
        foreach (PDvert pdv in pdVerts)
        {
            float dist = (pdv.position - hitPos).magnitude;

            if (dist < shortestDist)
            {
                cursorPos = pdv.position;
                shortestDist = dist;
                cloestVert = pdv; 

            }
        }
        return cloestVert; 
    }

    public void updateMesh(PDvert pdv)
    {
        Vector3[] vertices = mesh.vertices;
        vertices[pdv.index] = pdv.position;
        mesh.vertices = vertices;
    }

}



public class PDvert
{
    public Vector3 position;
    public int index;

    public PDvert(Vector3 position, int index)
    {
        this.position = position;
        this.index = index; 
    }

}
