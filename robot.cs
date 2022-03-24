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
        HIPS, ABDOMEN, CHEST, NECK, HEAD 
    } 
    
    // Start is called before the first frame update 
    void Start() { 
        rotY = 0; 
        dirY = 1; 
        deltaY = 0.2f; 
        
        parts = new List<GameObject>(); 
        originals = new List<Vector3[]>(); 
        places = new List<Vector3>(); sizes = new List<Vector3>(); 
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.HIPS].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0, 0)); sizes.Add(new Vector3(1, 0.2f, 0.6f)); 
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.ABDOMEN].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.8f, 0.5f, 0.6f)); 
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.CHEST].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.25f + 0.25f, 0)); sizes.Add(new Vector3(1, 0.5f, 0.8f)); 
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.NECK].GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.25f + 0.1f, 0)); sizes.Add(new Vector3(0.2f, 0.2f, 0.2f)); 
        
        parts.Add(GameObject.CreatePrimitive(PrimitiveType.Cube)); 
        originals.Add(parts[(int)PARTES.HEAN.GetComponent<MeshFilter>().mesh.vertices); 
        places.Add(new Vector3(0, 0.1f + 0.25f, 0)); sizes.Add(new Vector3(0.5f, 0.5f, 0.5f)); 
    } 
    
    // Update is called once per frame 
    void Update() { 
        rotY += dirY * deltaY; // rotY =0, dirY =1, deltaY =0.2f; 
        if (rotY < -10 11 rotY > 10) dirY = -dirY; 
        
        List<Matrix4x4> matrices = new List<Matrix4x4>(); 
        
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
        
        Matrix4x4 tNeck = Transformations.TranslateM(places[(int)PARTES.NECK].x, places[(int)PARTES.NECK].y, places[(int)PARTES.NECK].z); 
        Matrix4x4 sNeck = Transformations.ScaleM(sizes[(int)PARTES.NECK].x, sizes[(int)PARTES.NECK].y, sizes[(int)PARTES.NECK].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tNeck * sNeck); 
        
        Matrix4x4 tHead = Transformations.TranslateM(places[(int)PARTES.HEAD].x, places[(int)PARTES.HEAD].y, places[(int)PARTES.HEAD].z); 
        Matrix4x4 sHead = Transformations.ScaleM(sizes[(int)PARTES.HEAD].x, sizes[(int)PARTES.HEAD].y, sizes[(int)PARTES.HEAD].z); 
        matrices.Add(tHips * tAbs * rChest * tChest * tNeck * tHead * sHead); 

        for(int i = 0; i < matrices.Count; i++) { 
            parts[i].GetComponent<MeshFilter>().mesh.vertices = ApplyTransform(matrices[i], originals[i]);
        }
    }
}
