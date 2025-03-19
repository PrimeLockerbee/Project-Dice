using UnityEngine;

public class DiceFaceDetector : MonoBehaviour
{
     [System.Serializable]
    public struct Face
    {
        public int faceValue;    // Value of this face (e.g., 1, 2, 3, etc.)
        public Vector3 normal;   // Local normal direction of the face
    }

    public Face[] faces;  // Assign face values and their corresponding normals in Inspector

    public int GetTopFace()
    {
        int topFace = -1;
        float highestDot = -1f;

        foreach (var face in faces)
        {
            // Convert local normal to world space
            Vector3 worldNormal = transform.TransformDirection(face.normal);
            
            // Compare to world's up vector
            float dot = Vector3.Dot(worldNormal, Vector3.up);
            
            // Highest dot product indicates the face most aligned with up
            if (dot > highestDot)
            {
                highestDot = dot;
                topFace = face.faceValue;
            }
        }
        
        return topFace;
    }
}
