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
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showButton;

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

    // Settings menu
    public GameObject SettingMenuObject;


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
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf); // show/hide
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance; // set location
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z)); // rotate to the player
        menu.transform.forward *= -1;
    }

    private void HideAllMenus()
    {
        LoginMenuObject.SetActive(false);
        MainMenuObject.SetActive(false);
        SettingMenuObject.SetActive(false);
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
}
