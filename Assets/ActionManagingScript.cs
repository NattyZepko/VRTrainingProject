using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagingScript : MonoBehaviour
{
    ActionData[] gestureCollection;

    public float gestureSimilarityThreshold = 0.5f;
    public GestureCompletionData[] gestureList;
    public SpriteRenderer gestureImageRenderer; // Reference to the SpriteRenderer component to display the gesture image
    public Dictionary<int, Texture2D> gestureImages; // Dictionary to map gesture ID to GIF images
    private int currentGestureIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Add hardcoded gestures
        AddGestures();

        // Initialize the first expected gesture
        string nextGesture = GetNextExpectedGesture();
        Debug.Log("Next Gesture: " + nextGesture);

        // Display the gesture image
        DisplayGestureImage(gestureImages[currentGestureIndex]);
    }

    // Update is called once per frame
    void Update()
    {

    } 

    public void GestureRecognized(GestureCompletionData gestureData)
    {
        if (gestureData.similarity < gestureSimilarityThreshold)
            return;
        Debug.Log("Gesture recognized: " + gestureData.gestureID + " - " + gestureData.gestureName + ", similarity: " + gestureData.similarity);
        if (gestureData.gestureID == currentGestureIndex)
        {
            // Move to the next gesture
            currentGestureIndex++;
            if (currentGestureIndex < gestureImages.Count)
            {
                string nextGesture = GetNextExpectedGesture();
                Debug.Log("Next Gesture: " + nextGesture);

                // Display the gesture image
                DisplayGestureImage(gestureImages[currentGestureIndex]);
            }
            else
            {
                Debug.Log("All gestures completed.");
                // Do something when all gestures are completed
            }
        }
    }

    string GetNextExpectedGesture()
    {
        if (currentGestureIndex < gestureList.Length)
        {
            return gestureList[currentGestureIndex].gestureName;
        }
        else
        {
            return "No more gestures";
        }
    }

    void DisplayGestureImage(Texture2D gestureImage)
    {
        // Convert the Texture2D to Sprite
        Sprite gestureSprite = Sprite.Create(gestureImage, new Rect(0, 0, gestureImage.width, gestureImage.height), Vector2.one * 0.5f);

        // Set the SpriteRenderer's sprite to display the gesture image
        gestureImageRenderer.sprite = gestureSprite;
    }

    Texture2D LoadGestureImage(string imagePath)
    {
        // Load the gesture image from file using the provided image path
        // Replace this implementation with your own method to load GIF images
        Texture2D gestureImage = Resources.Load<Texture2D>(imagePath);
        return gestureImage;
    }


    // HARD CODED adding gestures
    void AddGestures()
    {
        gestureImages = new Dictionary<int, Texture2D>();

        // Add right hand jab gesture
        int rightHandJabID = 1;
        Texture2D rightHandJabImage = LoadGestureImage("RightHandJab.gif"); // Replace with actual path
        gestureImages.Add(rightHandJabID, rightHandJabImage);

        // Add left hand jab gesture
        int leftHandJabID = 2;
        Texture2D leftHandJabImage = LoadGestureImage("LeftHandJab.gif"); // Replace with actual path
        gestureImages.Add(leftHandJabID, leftHandJabImage);

        // Add right hand arc gesture
        int rightHandArcID = 3;
        Texture2D rightHandArcImage = LoadGestureImage("RightHandArc.gif"); // Replace with actual path
        gestureImages.Add(rightHandArcID, rightHandArcImage);

        // Add left hand arc gesture
        int leftHandArcID = 4;
        Texture2D leftHandArcImage = LoadGestureImage("LeftHandArc.gif"); // Replace with actual path
        gestureImages.Add(leftHandArcID, leftHandArcImage);

        // Add right hand uppercut gesture
        int rightHandUppercutID = 5;
        Texture2D rightHandUppercutImage = LoadGestureImage("RightHandUppercut.gif"); // Replace with actual path
        gestureImages.Add(rightHandUppercutID, rightHandUppercutImage);

        // Add left hand uppercut gesture
        int leftHandUppercutID = 6;
        Texture2D leftHandUppercutImage = LoadGestureImage("LeftHandUppercut.gif"); // Replace with actual path
        gestureImages.Add(leftHandUppercutID, leftHandUppercutImage);
    }
}
