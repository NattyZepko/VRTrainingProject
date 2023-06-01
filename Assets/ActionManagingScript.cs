using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionManagingScript : MonoBehaviour
{
    ActionData[] gestureCollection;

    public float gestureSimilarityThreshold = 0.4f;
    public GestureCompletionData[] gestureList;
    public SpriteRenderer gestureImageRenderer; // Reference to the SpriteRenderer component to display the gesture image
    public Dictionary<int, Texture2D> gestureImages; // Dictionary to map gesture ID to GIF images
    private int currentGestureIndex = 0;


    public GameObject ActionTextObject;
    private TextMeshProUGUI nextActionText;


    void Start()
    {
        nextActionText = ActionTextObject.GetComponent<TextMeshProUGUI>();
        nextActionText.text = gestureList[0].gestureName;
        Debug.Log("Gesture list size: " + gestureList.Length);
    }

    // Start is called before the first frame update
    /*
    void Start()
    {
        // Add hardcoded gestures
        AddGestures();

        // Initialize the first expected gesture
        string nextGesture = GetNextExpectedGesture();
        Debug.Log("Next Gesture: " + nextGesture);
        currentGestureText = GestureText.GetComponent<TextMeshProUGUI>();
        currentGestureText.text = nextGesture;

        // Display the gesture image
        DisplayGestureImage(gestureImages[currentGestureIndex]);
    }
    */

    // Update is called once per frame
    void Update()
    {


    }

    public void GestureRecognized(GestureCompletionData gestureData)
    {
        //Debug.Log("HELLO WORLD");
        if (gestureData.similarity < gestureSimilarityThreshold)
            return;
        Debug.Log("Gesture recognized: " + gestureData.gestureID + " - " + gestureData.gestureName + ", similarity: " + gestureData.similarity);
        if (currentGestureIndex >= gestureList.Length)
            return;
        if (gestureData.gestureID == gestureList[currentGestureIndex].gestureID)
        {
            // Move to the next gesture
            currentGestureIndex++;
            if (currentGestureIndex >= gestureList.Length)
            {
                nextActionText.text = "Training done, mainifico! BRAVO";
                // FUNCTION - TO DO ON PRACTICE COMPLETE <-- 
                return;
            }
            nextActionText.text = gestureList[currentGestureIndex].gestureName;
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
    void AddGestures()
    { }


}
