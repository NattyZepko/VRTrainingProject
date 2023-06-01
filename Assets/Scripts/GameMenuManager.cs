using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameMenuManager : MonoBehaviour
{
    // Menu placement Variables
    public Transform head;
    public float mainMenuSpawnDistance = 1, actionMenuSpawnDistance = 2;
    private const float MAX_MENU_DISTANCE = 3.0f, MIN_MENU_DISTANCE = 0.6f, JUMP_MENU_DISTANCE = 0.2f;
    public double gestureSimilarityThreshold = 0.5;
    public GameObject Menu;


    // Debugging Text
    public GameObject DebuggingText;
    private TextMeshProUGUI debugText;

    // Log-in menu
    public GameObject LoginMenuObject;
    private string UsernameInput = "", PasswordInput = "";

    // Main menu
    public GameObject MainMenuObject;
    public GameObject WelcomeTextObject;
    private TextMeshProUGUI welcomeText;
    private bool canCheckInput = true;
    private float cooldownDuration = 0.5f;
    private float cooldownTimer;

    // Settings menu
    public GameObject SettingMenuObject;
    //[SerializeField] UnityEngine.UI.Slider MainMenuDistanceSlider;

    // Action Menu
    public GameObject ActionMenu;


    // Start is called before the first frame update
    void Start()
    {
        debugText = DebuggingText.GetComponent<TextMeshProUGUI>();
        debugText.text = "";

        welcomeText = WelcomeTextObject.GetComponent<TextMeshProUGUI>();
        welcomeText.text = "Welcome";
    }

    // Update is called once per frame
    void Update()
    {
        if (!canCheckInput)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
                canCheckInput = true;
        }

        //if (showButton.action.WasPressedThisFrame())
        if (canCheckInput && OVRInput.Get(OVRInput.Button.Start))
        {
            Menu.SetActive(!Menu.activeSelf); // show/hide
            setMenuPosition();


        }
        Menu.transform.LookAt(new Vector3(head.position.x, Menu.transform.position.y, head.position.z)); // rotate to the player
        ActionMenu.transform.LookAt(new Vector3(head.position.x, ActionMenu.transform.position.y, head.position.z));
        Menu.transform.forward *= -1;
        ActionMenu.transform.forward *= -1;
    }

    private void setMenuPosition()
    {
        Menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * mainMenuSpawnDistance; // set location
        ActionMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * actionMenuSpawnDistance; // set location
        canCheckInput = false;
        cooldownTimer = cooldownDuration;
    }

    private void HideAllMenus()
    {
        LoginMenuObject.SetActive(false);
        MainMenuObject.SetActive(false);
        SettingMenuObject.SetActive(false);
        ActionMenu.SetActive(false);
    }

    public void ReadUserNameInput(string input)
    {
        UsernameInput = input;
    }

    public void ReadPasswordInput(string input)
    {
        PasswordInput = input;
    }

    public void Login()
    {
        debugText.text = "";
        if (UsernameInput.Equals("") || PasswordInput.Equals(""))
        {
            debugText.text = "Error Getting user data.";
            return;
        }
        UserData currentUser = UserDataManager.GetUser(UsernameInput, PasswordInput);
        if (currentUser == null)
        {
            debugText.text = "Error Getting user data.";
            return;
        }
        if (currentUser.IsAdmin)
        {
            SceneManager.LoadScene(1);
            return;
        }
        HideAllMenus();
        welcomeText.text = "Welcome " + currentUser.Name;
        MainMenuObject.SetActive(true);

    }

    public void Settings()
    {
        HideAllMenus();
        SettingMenuObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        HideAllMenus();
        MainMenuObject.SetActive(true);
    }

    public void ExitProgram()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // quitting the editor
#endif
        Application.Quit(); // Signal wanting to quit the game
    }

    /*public void GestureRecognized(GestureCompletionData gestureData)
    {
        if (gestureData.similarity >= gestureSimilarityThreshold)
        {

            // Do something with the gesture recognition.
            Debug.Log("ID:" + gestureData.gestureID + ", Name:" + gestureData.gestureName + ", Similarity:" + gestureData.similarity);
        }
    }*/

    /* public void ChangeMainMenuDistance()
     {
         this.mainMenuSpawnDistance = MainMenuDistanceSlider.value;
         Menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * mainMenuSpawnDistance;
     }*/
    public void IncreaseMenuDistance()
    {
        if (mainMenuSpawnDistance > MIN_MENU_DISTANCE)
        {
            mainMenuSpawnDistance -= JUMP_MENU_DISTANCE;
            setMenuPosition();
        }

    }
    public void DecreaseMenuDistance()
    {
        if (mainMenuSpawnDistance < MAX_MENU_DISTANCE)
        {
            mainMenuSpawnDistance += JUMP_MENU_DISTANCE;
            setMenuPosition();
        }
    }

    public void ShowActionMenu()
    {
        HideAllMenus();
        ActionMenu.active = true;
    }



}
