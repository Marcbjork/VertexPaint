using UnityEngine;

public class FindMesh
{
    public static Mesh GetMesh(GameObject aGO)
    {
        Mesh curMesh = null;

        if (aGO)
        {
            //find the current Mesh and check wheter its and meshfiler or skinnedmesh;
            MeshFilter curFilter = aGO.GetComponent<MeshFilter>();
            SkinnedMeshRenderer curSkinned = aGO.GetComponent<SkinnedMeshRenderer>();

            if (curFilter && !curSkinned)
            {
                curMesh = curFilter.sharedMesh;
            }

            if (!curFilter && curSkinned)
            {
                curMesh = curSkinned.sharedMesh;
            }
        }
        // return the curmesh for use in Vertexpaint script
        return curMesh;
    }

    //Fallof 
    public static float LinearFalloff(float distance, float brushRadius)
    {
        return Mathf.Clamp01(1 - distance / brushRadius);
    }

    //Lerp colors
    public static Color VtxColorLerp(Color colorA, Color colorB, float value)
    {
       
        if (value > 1f)
        {
            return colorB;
        }
        else if (value < 0f)
        {
            return colorA;
        }

        return new Color(colorA.r + (colorB.r - colorA.r) * value,
                         colorA.g + (colorB.g - colorA.g) * value,
                         colorA.b + (colorB.b - colorA.b) * value,
                         colorA.a + (colorB.a - colorA.a) * value);
    }
}
