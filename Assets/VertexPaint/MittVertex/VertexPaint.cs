using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class VertexPaint : MonoBehaviour
{
    bool allowPainting = true;
    bool isPainting = true;

    RaycastHit curHit;
    Camera cam;

    public Slider falloffSlider;
    float brushSize = 10.0f;
    float brushOpacity = 1.0f;
    public float brushFalloff = 1.0f;

    GameObject curGO;
    Mesh curMesh;
    GameObject lastGO;

    Color foregroundColor;
    Color activeColor;
  

    void Start()
    {
        cam = Camera.main;

        //brush Falloff slider
        falloffSlider.minValue = 1;
        falloffSlider.maxValue = 6;
        //falloffSlider.wholeNumbers = true;
        falloffSlider.value = 2;
    }
   
    void Update()
    {
        //Raycast to see where the
        Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0) && Physics.Raycast(worldRay, out curHit, 500f))
        {
            
            if (isPainting)
            {
               //Begin vertex painting
                 PaintVertexColors();
                
            }
            
            if (allowPainting)
            {   
                if (curHit.transform.gameObject != lastGO)
                {
                    curGO = curHit.transform.gameObject;
                    curMesh = FindMesh.GetMesh(curGO);
                    lastGO = curGO;
                }
            }
        }
        else
        {
            curGO = null;
            curMesh = null;
            lastGO = null;
        }
        // save the mesh
        if (Input.GetKeyDown("s"))
        {
            SavePrefab();
        }

    }

    void PaintVertexColors()
    {
        if (curMesh)
        {
            //Find the vertices off the current mesh and store its current colors in an array
            Vector3[] verts = curMesh.vertices;
            Color[] colors = new Color[0];
            
            if (curMesh.colors.Length > 0)
            {
                colors = curMesh.colors;
            }
            else
            {
                colors = new Color[verts.Length];
            }
            
            for (int i = 0; i < verts.Length; i++)
            {
                // square the distance we compare with & get the transformPoints off the vertices
                Vector3 vertPos = curGO.transform.TransformPoint(verts[i]);
                float sqrMag = (vertPos - curHit.point).sqrMagnitude;
                if(sqrMag > brushSize)
                { continue; }
               
                //Falloff to make it more realistic
                float falloff = FindMesh.LinearFalloff(sqrMag, brushSize);
                falloff = Mathf.Pow(falloff, brushFalloff * 3f) * brushOpacity;
                colors[i] = FindMesh.VtxColorLerp(colors[i], foregroundColor, falloff);
            }

            curMesh.colors = colors;
        }
    }

    void SavePrefab()
    {
        //Stores the CURRENT gameobjects in an array 
        GameObject[] objectarray = GameObject.FindGameObjectsWithTag("VertexCube");
        foreach(GameObject gameObject in objectarray)
        {
            //saves the current prefab in an folder
            string localPath = "Assets/" + gameObject.name + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction);
        }

    }
    public void OnValueChanged(float value)
    {
        brushSize = value;
        
    }
    //Change the painting color
    public void RedColor()
    {
        foregroundColor = Color.red;
    }
    public void BlueColor()
    {
        foregroundColor = Color.blue;
    }
    public void GreenColor()
    {
        foregroundColor = Color.green;
    }
    public void YellowColor()
    {
        foregroundColor = Color.yellow;
    }
    public void EraserColor()
    {
        foregroundColor = Color.white;
    }
}
