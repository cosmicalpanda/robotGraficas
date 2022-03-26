// Ernesto Adame A00825923
// Javier Domene A01197164
// Rodrigo Casale A01234245
// Mariano Shaar A00825287

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Robot : MonoBehaviour { 
    Vector3[] ApplyTransform(Matrix4x4 m, Vector3[] verts) { 
        int num = verts.Length; 
        Vector3[] result = new Vector3[num]; 
        
        for (int i = 0; i < num; i++) { 
            Vector3 v = verts[i]; 
            Vector4 temp = new Vector4(v.x, v.y, v.z, 1); 
            result[i] = m * temp; 
        } 
        
        return result; 
    } 
    
    List<GameObject> parts; 
    List<Vector3[]> originals; 
    List<Vector3> places; 
    List<Vector3> sizes; 

    float trHips, deltaHips;
    float rotChest, dirChest, deltaChest;
    float rotShoulder, dirShoulder, deltaShoulder;
    float rotFArm, trFArm;
    float rotHand;
    float rotLThigh, dirLThigh, deltaLThigh;
    float rotRThigh, dirRThigh, deltaRThigh;
    float rotLFoot, dirLFoot, deltaLFoot;
    float rotRFoot, dirRFoot, deltaRFoot;
    
    enum PARTES { 
        HIPS, 
        ABDOMEN, 
        CHEST, 
        NECK, 
        HEAD, 
        LSHOULDER, 
        RSHOULDER, 
        LARM, 
        RARM, 
        LFOREARM, 
        RFOREARM,
        LHAND,
        RHAND,
        LTHIGH,
        RTHIGH,
        LLEG,
        RLEG,
        LFOOT,
        RFOOT
    } 

    float[,] dims = new float[,] {
        {1.0f, 0.3f, 0.4f}, // HIPS
        {0.9f, 0.4f, 0.3f}, // ABS
        {1.0f, 0.65f, 0.5f}, // CHEST
        {0.3f, 0.1f, 0.3f}, // NECK
        {0.4f, 0.4f, 0.4f}, // HEAD
        {0.4f, 0.35f, 0.3f}, // LSHOULDER
        {0.4f, 0.35f, 0.3f}, // RSHOULDER
        {0.25f, 0.4f, 0.25f}, // LARM
        {0.25f, 0.4f, 0.25f}, // RARM
        {0.3f, 0.6f, 0.3f}, // LFOREARM
        {0.3f, 0.6f, 0.3f}, // RFOREARM
        {0.25f, 0.25f, 0.25f}, // LHAND
        {0.25f, 0.25f, 0.25f}, // RHAND
        {0.3f, 0.6f, 0.2f}, // LTHIGH
        {0.3f, 0.6f, 0.2f}, // RTHIGH
        {0.4f, 0.6f, 0.3f}, // LLEG
        {0.4f, 0.6f, 0.3f}, // RLEG
        {0.4f, 0.3f, 0.6f}, // LFOOT
        {0.4f, 0.3f, 0.6f}, // RFOOT
    };

    Color[] colors = {
        Color.grey,
        Color.white,
        Color.red,
        Color.blue,
    };

    int[] partColors = {
        0, // HIPS
        1, // ABS
        2, // CHEST
        1, // NECK
        3, // HEAD
        2, // LSHOULDER
        2, // RSHOULDER
        1, // LARM
        1, // RARM
        2, // LFOREARM
        2, // RFOREARM
        3, // LHAND
        3, // RHAND
        1, // LTHIGH
        1, // RTHIGH
        3, // LLEG
        3, // RLEG
        3, // LFOOT
        3, // RFOOT
    };
    
    // Start is called before the first frame update 
    void Start() { 
        trHips = 0; 
        deltaHips = 0.0025f;  
        
        rotChest = 0;
        dirChest = 1;
        deltaChest = 0.125f;
        
        rotShoulder = 0;
        dirShoulder = 1;
        deltaShoulder = 0.125f;
        
        rotFArm = 20f; 
        trFArm = 0.1f;
        rotHand = 6f;
        
        rotLThigh = 0;
        deltaLThigh = 0.5f;
        dirLThigh = 1;
        
        rotRThigh = 0;
        deltaRThigh = 0.5f;
        dirRThigh = -1;
        
        parts = new List<GameObject>(); 
        originals = new List<Vector3[]>(); 
        places = new List<Vector3>(); 
        sizes = new List<Vector3>(); 
        
        //hips
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.HIPS].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.HIPS, 0], dims[(int)PARTES.HIPS, 1], dims[(int)PARTES.HIPS, 2])); 
        
        //abs
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.ABDOMEN].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.ABDOMEN, 1] / 2 + dims[(int)PARTES.HIPS, 1] / 2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.ABDOMEN, 0], dims[(int)PARTES.ABDOMEN, 1], dims[(int)PARTES.ABDOMEN, 2])); 
        
        //chest
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.CHEST].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.ABDOMEN, 1] / 2 + dims[(int)PARTES.CHEST, 1] / 2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.CHEST, 0], dims[(int)PARTES.CHEST, 1], dims[(int)PARTES.CHEST, 2]));
        
        //neck
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.NECK].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.NECK, 1] / 2 + dims[(int)PARTES.CHEST, 1] / 2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.NECK, 0], dims[(int)PARTES.NECK, 1], dims[(int)PARTES.NECK, 2]));
        
        //head
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.HEAD].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.NECK, 1] / 2 + dims[(int)PARTES.HEAD, 1] / 2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.HEAD, 0], dims[(int)PARTES.HEAD, 1], dims[(int)PARTES.HEAD, 2]));

        //shoulders
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LSHOULDER].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(dims[(int)PARTES.CHEST, 0] / 2 + dims[(int)PARTES.LSHOULDER, 0] / 2, 0.15f, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LSHOULDER, 0], dims[(int)PARTES.LSHOULDER, 1], dims[(int)PARTES.LSHOULDER, 2]));

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RSHOULDER].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(dims[(int)PARTES.CHEST, 0] / -2 + dims[(int)PARTES.RSHOULDER, 0] / -2, 0.15f, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RSHOULDER, 0], dims[(int)PARTES.RSHOULDER, 1], dims[(int)PARTES.RSHOULDER, 2]));

        //arms
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LARM].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.LARM, 1] / -2 + dims[(int)PARTES.LSHOULDER, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LARM, 0], dims[(int)PARTES.LARM, 1], dims[(int)PARTES.LARM, 2]));

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RARM].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.RARM, 1] / -2 + dims[(int)PARTES.RSHOULDER, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RARM, 0], dims[(int)PARTES.RARM, 1], dims[(int)PARTES.RARM, 2]));
       
        //forearms
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LFOREARM].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, trFArm + dims[(int)PARTES.LARM, 1] / -2 + dims[(int)PARTES.LFOREARM, 1] / -2, trFArm)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LFOREARM, 0], dims[(int)PARTES.LFOREARM, 1], dims[(int)PARTES.LFOREARM, 2]));

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RFOREARM].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, trFArm + dims[(int)PARTES.RARM, 1] / -2 + dims[(int)PARTES.RFOREARM, 1] / -2, trFArm)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RFOREARM, 0], dims[(int)PARTES.RFOREARM, 1], dims[(int)PARTES.RFOREARM, 2]));

        //hands
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LHAND].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.LFOREARM, 1] / -2 + dims[(int)PARTES.LHAND, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LHAND, 0], dims[(int)PARTES.LHAND, 1], dims[(int)PARTES.LHAND, 2]));

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RHAND].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.RFOREARM, 1] / -2 + dims[(int)PARTES.RHAND, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RHAND, 0], dims[(int)PARTES.RHAND, 1], dims[(int)PARTES.RHAND, 2]));
        
        //thighs
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LTHIGH].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0.3f, dims[(int)PARTES.HIPS, 1] / -2 + dims[(int)PARTES.LTHIGH, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LTHIGH, 0], dims[(int)PARTES.LTHIGH, 1], dims[(int)PARTES.LTHIGH, 2]));

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RTHIGH].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(-0.3f, dims[(int)PARTES.HIPS, 1] / -2 + dims[(int)PARTES.RTHIGH, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RTHIGH, 0], dims[(int)PARTES.RTHIGH, 1], dims[(int)PARTES.RTHIGH, 2]));

        //legs
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LLEG].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.LTHIGH, 1] / -2 + dims[(int)PARTES.LLEG, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LLEG, 0], dims[(int)PARTES.LLEG, 1], dims[(int)PARTES.LLEG, 2]));
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RLEG].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.RTHIGH, 1] / -2 + dims[(int)PARTES.RLEG, 1] / -2, 0)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RLEG, 0], dims[(int)PARTES.RLEG, 1], dims[(int)PARTES.RLEG, 2]));

        //feet
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LFOOT].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.LLEG, 1] / -2 + dims[(int)PARTES.LFOOT, 1] / -2, -.15f)); 
        sizes.Add(new Vector3(dims[(int)PARTES.LFOOT, 0], dims[(int)PARTES.LFOOT, 1], dims[(int)PARTES.LFOOT, 2]));

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RFOOT].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, dims[(int)PARTES.RLEG, 1] / -2 + dims[(int)PARTES.RFOOT, 1] / -2, -.15f)); 
        sizes.Add(new Vector3(dims[(int)PARTES.RFOOT, 0], dims[(int)PARTES.RFOOT, 1], dims[(int)PARTES.RFOOT, 2]));

        for (int i = 0; i < parts.Count; i++) {
            parts[i].GetComponent<Renderer>().material.SetColor("_Color", colors[partColors[i]]);
        }
    } 
    
    // Update is called once per frame 
    void Update() { 
        trHips += deltaHips;
        if (trHips < -0.12f || trHips > 0) deltaHips = -deltaHips;

        rotChest += deltaChest * dirChest;
        if (rotChest < -6f || rotChest > 6f) dirChest = -dirChest; 

        rotShoulder += deltaShoulder * dirShoulder;
        if (rotShoulder < -6f || rotShoulder > 6f) dirShoulder = -dirShoulder; 

        rotLThigh += deltaLThigh * dirLThigh;
        if (rotLThigh < -24f) {
            // deltaLThigh = 0.75f;
            dirLThigh = 1;
        } else if (rotLThigh > 24f) {
            // deltaLThigh = 0.375f;
            dirLThigh = -1;
        }

        rotRThigh += deltaRThigh * dirRThigh;
        if (rotRThigh < -24f) {
            // deltaRThigh = 0.75f;
            dirRThigh = 1;
        } else if (rotRThigh > 24f) {
            // deltaRThigh = 0.375f;
            dirRThigh = -1;
        }
        
        List<Matrix4x4> matrices = new List<Matrix4x4>(); 
        
        //hip
        Matrix4x4 tHips = Transformations.TranslateM(places[(int)PARTES.HIPS].x, places[(int)PARTES.HIPS].y + trHips, places[(int)PARTES.HIPS].z); 
        Matrix4x4 sHips = Transformations.ScaleM(sizes[(int)PARTES.HIPS].x, sizes[(int)PARTES.HIPS].y, sizes[(int)PARTES.HIPS].z); 
        matrices.Add(tHips * sHips); 
        
        //abs
        Matrix4x4 tAbs = Transformations.TranslateM(places[(int)PARTES.ABDOMEN].x, places[(int)PARTES.ABDOMEN].y, places[(int)PARTES.ABDOMEN].z); 
        Matrix4x4 sAbs = Transformations.ScaleM(sizes[(int)PARTES.ABDOMEN].x, sizes[(int)PARTES.ABDOMEN].y, sizes[(int)PARTES.ABDOMEN].z); 
        matrices.Add(tHips * tAbs * sAbs); 
        
        //chest
        Matrix4x4 rChest = Transformations.RotateM(rotChest, Transformations.AXIS.AX_Y); 
        Matrix4x4 tChest = Transformations.TranslateM(places[(int)PARTES.CHEST].x, places[(int)PARTES.CHEST].y, places[(int)PARTES.CHEST].z); 
        Matrix4x4 sChest = Transformations.ScaleM(sizes[(int)PARTES.CHEST].x, sizes[(int)PARTES.CHEST].y, sizes[(int)PARTES.CHEST].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * sChest); 
        
        //head and neck
        Matrix4x4 rNeck = Transformations.RotateM(-rotChest, Transformations.AXIS.AX_Y);
        Matrix4x4 tNeck = Transformations.TranslateM(places[(int)PARTES.NECK].x, places[(int)PARTES.NECK].y, places[(int)PARTES.NECK].z); 
        Matrix4x4 sNeck = Transformations.ScaleM(sizes[(int)PARTES.NECK].x, sizes[(int)PARTES.NECK].y, sizes[(int)PARTES.NECK].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rNeck * tNeck * sNeck); 
        
        Matrix4x4 tHead = Transformations.TranslateM(places[(int)PARTES.HEAD].x, places[(int)PARTES.HEAD].y, places[(int)PARTES.HEAD].z); 
        Matrix4x4 sHead = Transformations.ScaleM(sizes[(int)PARTES.HEAD].x, sizes[(int)PARTES.HEAD].y, sizes[(int)PARTES.HEAD].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rNeck * tNeck * tHead * sHead); 

        //shoulders
        Matrix4x4 rLShoulder = Transformations.RotateM(rotShoulder, Transformations.AXIS.AX_X); 
        Matrix4x4 tLShoulder = Transformations.TranslateM(places[(int)PARTES.LSHOULDER].x, places[(int)PARTES.LSHOULDER].y, places[(int)PARTES.LSHOULDER].z); 
        Matrix4x4 sLShoulder = Transformations.ScaleM(sizes[(int)PARTES.LSHOULDER].x, sizes[(int)PARTES.LSHOULDER].y, sizes[(int)PARTES.LSHOULDER].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rLShoulder * tLShoulder * sLShoulder); 

        Matrix4x4 rRShoulder = Transformations.RotateM(-rotShoulder, Transformations.AXIS.AX_X); 
        Matrix4x4 tRShoulder = Transformations.TranslateM(places[(int)PARTES.RSHOULDER].x, places[(int)PARTES.RSHOULDER].y, places[(int)PARTES.RSHOULDER].z); 
        Matrix4x4 sRShoulder = Transformations.ScaleM(sizes[(int)PARTES.RSHOULDER].x, sizes[(int)PARTES.RSHOULDER].y, sizes[(int)PARTES.RSHOULDER].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rRShoulder * tRShoulder * sRShoulder); 

        //arms
        Matrix4x4 tLARM = Transformations.TranslateM(places[(int)PARTES.LARM].x, places[(int)PARTES.LARM].y, places[(int)PARTES.LARM].z); 
        Matrix4x4 sLARM = Transformations.ScaleM(sizes[(int)PARTES.LARM].x, sizes[(int)PARTES.LARM].y, sizes[(int)PARTES.LARM].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rLShoulder * tLShoulder * tLARM * sLARM);      

        Matrix4x4 tRARM = Transformations.TranslateM(places[(int)PARTES.RARM].x, places[(int)PARTES.RARM].y, places[(int)PARTES.RARM].z); 
        Matrix4x4 sRARM = Transformations.ScaleM(sizes[(int)PARTES.RARM].x, sizes[(int)PARTES.RARM].y, sizes[(int)PARTES.RARM].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rRShoulder * tRShoulder * tRARM * sRARM);      

        //forearms
        Matrix4x4 rLFArm = Transformations.RotateM(rotFArm, Transformations.AXIS.AX_X);
        Matrix4x4 tLfArm = Transformations.TranslateM(places[(int)PARTES.LFOREARM].x, places[(int)PARTES.LFOREARM].y, places[(int)PARTES.LFOREARM].z); 
        Matrix4x4 sLfArm = Transformations.ScaleM(sizes[(int)PARTES.LFOREARM].x, sizes[(int)PARTES.LFOREARM].y, sizes[(int)PARTES.LFOREARM].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rLShoulder * tLShoulder * tLARM * rLFArm * tLfArm * sLfArm);      

        Matrix4x4 rRFArm = Transformations.RotateM(rotFArm, Transformations.AXIS.AX_X);
        Matrix4x4 tRfArm = Transformations.TranslateM(places[(int)PARTES.LFOREARM].x, places[(int)PARTES.LFOREARM].y, places[(int)PARTES.LFOREARM].z); 
        Matrix4x4 sRfArm = Transformations.ScaleM(sizes[(int)PARTES.LFOREARM].x, sizes[(int)PARTES.LFOREARM].y, sizes[(int)PARTES.LFOREARM].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rRShoulder * tRShoulder * tRARM * rRFArm * tRfArm * sRfArm);      

        //hands
        Matrix4x4 rLHand = Transformations.RotateM(-rotHand, Transformations.AXIS.AX_Z);
        Matrix4x4 tLHand = Transformations.TranslateM(places[(int)PARTES.LHAND].x, places[(int)PARTES.LHAND].y, places[(int)PARTES.LHAND].z); 
        Matrix4x4 sLHand = Transformations.ScaleM(sizes[(int)PARTES.LHAND].x, sizes[(int)PARTES.LHAND].y, sizes[(int)PARTES.LHAND].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rLShoulder * tLShoulder * tLARM * rLFArm * tLfArm * rLHand * tLHand * sLHand);  
        
        Matrix4x4 rRHand = Transformations.RotateM(rotHand, Transformations.AXIS.AX_Z);
        Matrix4x4 tRHand = Transformations.TranslateM(places[(int)PARTES.RHAND].x, places[(int)PARTES.RHAND].y, places[(int)PARTES.RHAND].z); 
        Matrix4x4 sRHand = Transformations.ScaleM(sizes[(int)PARTES.RHAND].x, sizes[(int)PARTES.RHAND].y, sizes[(int)PARTES.RHAND].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * rRShoulder * tRShoulder * tRARM * rRFArm * tRfArm * rRHand * tRHand * sRHand);     

        //thighs 
        Matrix4x4 rLThigh = Transformations.RotateM(rotLThigh, Transformations.AXIS.AX_X);
        Matrix4x4 tLThigh = Transformations.TranslateM(places[(int)PARTES.LTHIGH].x, places[(int)PARTES.LTHIGH].y, places[(int)PARTES.LTHIGH].z); 
        Matrix4x4 sLThigh = Transformations.ScaleM(sizes[(int)PARTES.LTHIGH].x, sizes[(int)PARTES.LTHIGH].y, sizes[(int)PARTES.LTHIGH].z); 
        matrices.Add(tHips * rLThigh * tLThigh * sLThigh); 

        Matrix4x4 rRThigh = Transformations.RotateM(rotRThigh, Transformations.AXIS.AX_X);
        Matrix4x4 tRThigh = Transformations.TranslateM(places[(int)PARTES.RTHIGH].x, places[(int)PARTES.RTHIGH].y, places[(int)PARTES.RTHIGH].z); 
        Matrix4x4 sRThigh = Transformations.ScaleM(sizes[(int)PARTES.RTHIGH].x, sizes[(int)PARTES.RTHIGH].y, sizes[(int)PARTES.RTHIGH].z); 
        matrices.Add(tHips * rRThigh * tRThigh * sRThigh); 
       
        //legs
        Matrix4x4 tLLEG = Transformations.TranslateM(places[(int)PARTES.LLEG].x, places[(int)PARTES.LLEG].y, places[(int)PARTES.LLEG].z); 
        Matrix4x4 sLLEG = Transformations.ScaleM(sizes[(int)PARTES.LLEG].x, sizes[(int)PARTES.LLEG].y, sizes[(int)PARTES.LLEG].z); 
        matrices.Add(tHips * rLThigh * tLThigh * tLLEG * sLLEG); 

        Matrix4x4 tRLEG = Transformations.TranslateM(places[(int)PARTES.RLEG].x, places[(int)PARTES.RLEG].y, places[(int)PARTES.RLEG].z); 
        Matrix4x4 sRLEG = Transformations.ScaleM(sizes[(int)PARTES.RLEG].x, sizes[(int)PARTES.RLEG].y, sizes[(int)PARTES.RLEG].z); 
        matrices.Add(tHips  * rRThigh* tRThigh * tRLEG * sRLEG); 

        //feet
        Matrix4x4 tLFoot = Transformations.TranslateM(places[(int)PARTES.LFOOT].x, places[(int)PARTES.LFOOT].y, places[(int)PARTES.LFOOT].z); 
        Matrix4x4 sLFoot = Transformations.ScaleM(sizes[(int)PARTES.LFOOT].x, sizes[(int)PARTES.LFOOT].y, sizes[(int)PARTES.LFOOT].z); 
        matrices.Add(tHips * rLThigh * tLThigh * tLLEG * tLFoot * sLFoot); 

        Matrix4x4 tRFoot = Transformations.TranslateM(places[(int)PARTES.LFOOT].x, places[(int)PARTES.LFOOT].y, places[(int)PARTES.LFOOT].z); 
        Matrix4x4 sRFoot = Transformations.ScaleM(sizes[(int)PARTES.LFOOT].x, sizes[(int)PARTES.LFOOT].y, sizes[(int)PARTES.LFOOT].z); 
        matrices.Add(tHips * rRThigh * tRThigh * tLLEG * tLFoot * sLFoot); 

        for(int i = 0; i < matrices.Count; i++) { 
            parts[i].GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(matrices[i], originals[i]);
        }
    }
}
