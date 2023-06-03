using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ActionManagingScript : MonoBehaviour
{
    ActionData[] gestureCollection;

    // Expected Gesture Managing
    public float gestureSimilarityThreshold = 0.4f;
    public GestureCompletionData[] BoxingAMoveList; // Plan 0
    private GestureCompletionData[] BoxingBMoveList; // Plan 1
    private GestureCompletionData[] RandomBoxingMoves; // Plan 2

    private GestureCompletionData[] myPlan;


    private int currentPlan = 0;

    private int currentGestureIndex = 0;
    private int numberOfMoves = 10;

    // Expected Gesture Display
    public GameObject ActionTextObject;
    private TextMeshProUGUI nextActionText;
    public GameObject nextActionInstructionText;
    public RawImage nextActionImage;
    public Texture[] textureList = new Texture[6];
    public Texture finishedTexture;
    public GameObject BackToMainMenuButton;

    public TMPro.TMP_Dropdown DiscionDropdown;




    void Start()
    {
        nextActionText = ActionTextObject.GetComponent<TextMeshProUGUI>();
        fillBoxingBMoveList();
        StartTrainingSession();

    }

    void fillBoxingBMoveList()
    {
        BoxingBMoveList = new GestureCompletionData[numberOfMoves];
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                BoxingBMoveList[i] = new GestureCompletionData();
                BoxingBMoveList[i].gestureID = 0;
                BoxingBMoveList[i].gestureName = "right-jab";
            }
            else
            {
                BoxingBMoveList[i] = new GestureCompletionData();
                BoxingBMoveList[i].gestureID = 1;
                BoxingBMoveList[i].gestureName = "left-jab";
            }
        }
    }

    void fillRandomBoxingMoves()
    {
        RandomBoxingMoves = new GestureCompletionData[numberOfMoves];
        for (int i = 0; i < numberOfMoves; i++)
        {
            int r = Random.Range(0, 6);
            RandomBoxingMoves[i] = new GestureCompletionData();
            RandomBoxingMoves[i].gestureID = r;
            switch (r)
            {
                case 0:
                    RandomBoxingMoves[i].gestureName = "right-jab";
                    break;
                case 1:
                    RandomBoxingMoves[i].gestureName = "left-jab";
                    break;
                case 2:
                    RandomBoxingMoves[i].gestureName = "right-uppercut";
                    break;
                case 3:
                    RandomBoxingMoves[i].gestureName = "left-uppercut";
                    break;
                case 4:
                    RandomBoxingMoves[i].gestureName = "right-hook";
                    break;
                case 5:
                    RandomBoxingMoves[i].gestureName = "left-hook";
                    break;
                default:
                    Debug.Log("WHAT THE FUCK HAPPENED?!?!!");
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void StartTrainingSession()
    {
        Debug.Log("Start Training Session is called, Value is " + DiscionDropdown.value);
        currentPlan = DiscionDropdown.value; // Get a correct value
        fillRandomBoxingMoves();
        BackToMainMenuButton.active = false; // Hide the "back" button
        currentGestureIndex = 0;
        nextActionInstructionText.active = true;
        switch (currentPlan)
        {
            case 0:
                myPlan = BoxingAMoveList;
                nextActionText.text = BoxingAMoveList[0].gestureName;
                nextActionImage.texture = textureList[BoxingAMoveList[0].gestureID];
                break;
            case 1:
                myPlan = BoxingBMoveList;
                nextActionText.text = BoxingBMoveList[0].gestureName;
                nextActionImage.texture = textureList[BoxingBMoveList[0].gestureID];
                break;
            case 2:
                myPlan = RandomBoxingMoves;
                nextActionText.text = RandomBoxingMoves[0].gestureName;
                nextActionImage.texture = textureList[RandomBoxingMoves[0].gestureID];
                break;
            default:
                Debug.Log("SOMETHING IS WROOOONGGG AHHHHHH");
                myPlan = BoxingAMoveList;
                break;
        }
    }

    public void GestureRecognized(GestureCompletionData gestureData)
    {
        if (gestureData.similarity < gestureSimilarityThreshold)
            return;
        //Debug.Log("Gesture recognized: " + gestureData.gestureID + " - " + gestureData.gestureName + ", similarity: " + gestureData.similarity);

        if (currentGestureIndex >= myPlan.Length)
            return;
        if (gestureData.gestureID == myPlan[currentGestureIndex].gestureID) // <------
        {
            // Move to the next gesture
            currentGestureIndex++;
            if (currentGestureIndex >= myPlan.Length)
            {
                nextActionText.text = "Training done!";
                nextActionImage.texture = finishedTexture;
                nextActionInstructionText.active = false;
                BackToMainMenuButton.active = true;
                return;
            }
            nextActionText.text = myPlan[currentGestureIndex].gestureName;
            int currentGestureId = myPlan[currentGestureIndex].gestureID;
            Debug.Log("Current Gesture ID: " + currentGestureId);
            nextActionImage.texture = textureList[currentGestureId];
        }
    }

}
