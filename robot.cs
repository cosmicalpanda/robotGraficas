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
    
    float rotY; 
    float dirY; 
    float deltaY; 
    
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
        LKNEE,
        RKNEE,
        LLEG,
        RLEG,
        LFOOT,
        RFOOT
    } 
    
    // Start is called before the first frame update 
    void Start() { 
        rotY = 0; 
        dirY = 1; 
        deltaY = 0.2f; 
        
        parts = new List<GameObject>(); 
        originals = new List<Vector3[]>(); 
        places = new List<Vector3>(); sizes = new List<Vector3>(); 
        
        //hips
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.HIPS].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0, 0)); sizes.Add(new Vector3(1, 0.2f, 0.6f)); 
        
        //abs
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.ABDOMEN].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.8f, 0.5f, 0.6f)); 
        
        //chest
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.CHEST].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.25f + 0.25f, 0)); sizes.Add(new Vector3(1, 0.5f, 0.8f)); 
        
        //head and neck
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.NECK].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.25f + 0.1f, 0)); sizes.Add(new Vector3(0.2f, 0.2f, 0.2f)); 
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.HEAD].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f));

        //shoulders
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LSHOULDER].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0.7f, 0, 0)); sizes.Add(new Vector3(.40f,  .40f, .40f)); 

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RSHOULDER].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(-0.7f, 0, 0)); sizes.Add(new Vector3(.40f,  .40f, .40f)); 

        // //arms
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.LARM].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, -.5f, 0)); sizes.Add(new Vector3(0.15f, 0.7f, 0.3f)); 

        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.RARM].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, -.5f, 0)); sizes.Add(new Vector3(0.15f, 0.7f, 0.3f)); 
       
        // //forearms
        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.LFOREARM].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 

        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.RFOREARM].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
        
        // //hands
        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.LHAND].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 

        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.RHAND].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
        
        // //thighs
        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.LTHIGH].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 

        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.RTHIGH].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
        
        // //knees *creo que no es necesario
        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.LKNEE].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 

        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.RKNEE].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
        
        // //legs
        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.LLEG].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 

        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.RLEG].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
        
        // //feet
        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.LFOOT].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 

        // parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        // originals.Add(parts[(int)PARTES.RFOOT].GetComponent<MeshFilter>().mesh.vertices); 
        // places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
   
    } 
    
    // Update is called once per frame 
    void Update() { 
        rotY += dirY * deltaY; // rotY =0, dirY =1, deltaY =0.2f; 
        if (rotY < -10 || rotY > 10) dirY = -dirY; 
        
        List<Matrix4x4> matrices = new List<Matrix4x4>(); 
        
        //trunk
        Matrix4x4 tHips = Transformations.TranslateM(places[(int)PARTES.HIPS].x, places[(int)PARTES.HIPS].y, places[(int)PARTES.HIPS].z); 
        Matrix4x4 sHips = Transformations.ScaleM(sizes[(int)PARTES.HIPS].x, sizes[(int)PARTES.HIPS].y, sizes[(int)PARTES.HIPS].z); 
        matrices.Add(tHips * sHips); 
        
        Matrix4x4 tAbs = Transformations.TranslateM(places[(int)PARTES.ABDOMEN].x, places[(int)PARTES.ABDOMEN].y, places[(int)PARTES.ABDOMEN].z); 
        Matrix4x4 sAbs = Transformations.ScaleM(sizes[(int)PARTES.ABDOMEN].x, sizes[(int)PARTES.ABDOMEN].y, sizes[(int)PARTES.ABDOMEN].z); 
        matrices.Add(tHips * tAbs * sAbs); 
        
        Matrix4x4 rChest = Transformations.RotateM(rotY, Transformations.AXIS.AX_Y); 
        Matrix4x4 tChest = Transformations.TranslateM(places[(int)PARTES.CHEST].x, places[(int)PARTES.CHEST].y, places[(int)PARTES.CHEST].z); 
        Matrix4x4 sChest = Transformations.ScaleM(sizes[(int)PARTES.CHEST].x, sizes[(int)PARTES.CHEST].y, sizes[(int)PARTES.CHEST].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * sChest); 
        
        //head and neck
        Matrix4x4 tNeck = Transformations.TranslateM(places[(int)PARTES.NECK].x, places[(int)PARTES.NECK].y, places[(int)PARTES.NECK].z); 
        Matrix4x4 sNeck = Transformations.ScaleM(sizes[(int)PARTES.NECK].x, sizes[(int)PARTES.NECK].y, sizes[(int)PARTES.NECK].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tNeck * sNeck); 
        
        Matrix4x4 tHead = Transformations.TranslateM(places[(int)PARTES.HEAD].x, places[(int)PARTES.HEAD].y, places[(int)PARTES.HEAD].z); 
        Matrix4x4 sHead = Transformations.ScaleM(sizes[(int)PARTES.HEAD].x, sizes[(int)PARTES.HEAD].y, sizes[(int)PARTES.HEAD].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tNeck * tHead * sHead); 

        //shoulders
        Matrix4x4 tLShoulder = Transformations.TranslateM(places[(int)PARTES.LSHOULDER].x, places[(int)PARTES.LSHOULDER].y, places[(int)PARTES.LSHOULDER].z); 
        Matrix4x4 sLShoulder = Transformations.ScaleM(sizes[(int)PARTES.LSHOULDER].x, sizes[(int)PARTES.LSHOULDER].y, sizes[(int)PARTES.LSHOULDER].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tLShoulder * sLShoulder); 

        Matrix4x4 tRShoulder = Transformations.TranslateM(places[(int)PARTES.RSHOULDER].x, places[(int)PARTES.RSHOULDER].y, places[(int)PARTES.RSHOULDER].z); 
        Matrix4x4 sRShoulder = Transformations.ScaleM(sizes[(int)PARTES.RSHOULDER].x, sizes[(int)PARTES.RSHOULDER].y, sizes[(int)PARTES.RSHOULDER].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tRShoulder * sRShoulder); 

        //arms
        Matrix4x4 tLARM = Transformations.TranslateM(places[(int)PARTES.LARM].x, places[(int)PARTES.LARM].y, places[(int)PARTES.LARM].z); 
        Matrix4x4 sLARM = Transformations.ScaleM(sizes[(int)PARTES.LARM].x, sizes[(int)PARTES.LARM].y, sizes[(int)PARTES.LARM].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tLShoulder * tLARM * sLARM);      

        Matrix4x4 tRARM = Transformations.TranslateM(places[(int)PARTES.RARM].x, places[(int)PARTES.RARM].y, places[(int)PARTES.RARM].z); 
        Matrix4x4 sRARM = Transformations.ScaleM(sizes[(int)PARTES.RARM].x, sizes[(int)PARTES.RARM].y, sizes[(int)PARTES.RARM].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tRShoulder * tRARM * sRARM);      

        //forearms

        //hands
        
        //thighs

        //knees *no se necesita

        //legs

        //feet

        for(int i = 0; i < matrices.Count; i++) { 
            parts[i].GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(matrices[i], originals[i]);
        }
    }
}
