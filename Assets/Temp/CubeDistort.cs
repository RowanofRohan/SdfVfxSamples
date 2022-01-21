using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.SDF;
using UnityEngine.UI;
using MegaFiers;

public class CubeDistort : MonoBehaviour
{

    [SerializeField]
    private VisualEffect vfxGraph;
    // Start is called before the first frame update
    MegaCrumple crumpler;
    [SerializeField]
    private Slider slider;

    MeshToSDFBaker baker;
    [SerializeField]
    Mesh cubeMesh;
    MeshFilter meshRenderer;

    MegaModifyObject modifier;

    
    public int maxResolution = 64;
    public Vector3 center;
    public Vector3 sizeBox;
    public int signPassCount = 1;
    public float threshold = 0.5f;



    void Start()
    {
        meshRenderer = this.transform.GetComponent<MeshFilter>();
        crumpler = this.transform.GetComponent<MegaCrumple>();
        modifier = this.transform.GetComponent<MegaModifyObject>();
        cubeMesh = meshRenderer.sharedMesh;

        baker = new MeshToSDFBaker(sizeBox, center, maxResolution, cubeMesh, signPassCount, threshold);
        baker.BakeSDF();
        vfxGraph.SetTexture("SDF", baker.SdfTexture);
    }

    public void UpdateCrumple()
    {
        crumpler.scale = slider.value;

        StartCoroutine(updateSDF());
        

        //Update SDF HERE
    }


    private IEnumerator updateSDF()
    {
        yield return null;
        
        cubeMesh = meshRenderer.sharedMesh;
        //cubeMesh = modifier.mesh;
        

        baker = new MeshToSDFBaker(sizeBox, center, maxResolution, cubeMesh, signPassCount, threshold);
        baker.BakeSDF();
        vfxGraph.SetTexture("SDF", baker.SdfTexture);

    }

    void OnDestroy()
    {
        baker.Dispose();
    }
}
