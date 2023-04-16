using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;

}

public class GestureDetector : MonoBehaviour
{
    // public variables
    public float GestureRecognitionThreshold = 0.05f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;

    // private variables
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;



    // Start is called before the first frame update
    void Start()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognizes = !currentGesture.Equals(new Gesture()); // This is new!
        if(hasRecognizes && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("Gesture regonized! " + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }
    }

    void Save()
    {
        Gesture ges = new Gesture();
        ges.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach (var bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        ges.fingerDatas = data;
        gestures.Add(ges);
    }

    Gesture Recognize()
    {
        // variables
        Gesture currentGesture = new Gesture();
        float currentMinDistance = Mathf.Infinity;

        // check every gesture
        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++) // for each finger
            {
                Vector3 currentHandData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentHandData, gesture.fingerDatas[i]);
                if (distance > GestureRecognitionThreshold)
                { // The Gesture isn't similar enough
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }
            if(!isDiscarded && sumDistance < currentMinDistance)
            { // The Gesture is accepted as "similar enough", save if its best fitted
                currentMinDistance = sumDistance;
                currentGesture = gesture;
            }
        }
        return currentGesture;
    }
}
