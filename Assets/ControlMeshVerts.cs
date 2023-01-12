using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMeshVerts : MonoBehaviour
{
    public bool showMesh; 
    public PDvert pdvert; 
    public int index; 
    public MeshManager meshManager;

    public GameObject needle;
    // Start is called before the first frame update
    void Start()
    {
        needle = GameObject.Find("Needle"); 
        GetComponent<DragObjectScript>().onNeedleDragged += updateVertPos;
        GetComponent<MeshRenderer>().enabled = showMesh;

        needle.GetComponent<RopeDragger>().onDrag += updateVertPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateVertPos()
    {

        print(pdvert.position);
        pdvert.position = transform.localPosition;
        meshManager.updateMesh(pdvert);
        
    }

    public void initate()
    {
        print(pdvert.index);
        print(pdvert.position);
        transform.localPosition = pdvert.position;
        index = pdvert.index; 
    }
}
