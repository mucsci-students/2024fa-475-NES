using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string currentScene = "StartMenu";
    // Some variables for button positioning and size (optional, to keep things organized)
    private float buttonWidth = 200;
    private float buttonHeight = 50;
    private bool showOptions = false;
    private bool gamePause = false;
    private bool showInstructions = false;
    private int playerHealth;
    private int playerArmor;
    private int playerMoney;
    private int playerWeaponLevel;
    private int playerVehicleLevel;
    private int playerArmorLevel;
    private int volumeVal;
    private int currentLevel;


    // Create a custom GUIStyle for the game name
    private GUIStyle titleStyle;
    private GUIStyle buttonStyle;

    // Start is called before the first frame update
    void Start()
    {   
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // Load any player data
        playerHealth = PlayerPrefs.GetInt("PlayerHealth");
        if (playerHealth == 0) {
            PlayerPrefs.SetInt("PlayerHealth", 100);
            playerHealth = 100;
        }
        playerArmor = PlayerPrefs.GetInt("PlayerArmor");
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        volumeVal = PlayerPrefs.GetInt("Volume");
        PlayerPrefs.SetInt("GamePaused", 0);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        // Create a custom GUIStyle for the game name
        titleStyle = new GUIStyle();
        titleStyle.fontSize = 30;  // Adjust the size as needed
        titleStyle.normal.textColor = Color.white; // Change the color as needed
        titleStyle.fontStyle = FontStyle.Bold; // Bold text
        titleStyle.alignment = TextAnchor.MiddleCenter; // Center the text

        // Create a custom GUIStyle for the buttons
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 20;  // Adjust the size as needed
        buttonStyle.normal.textColor = Color.white; // Change the color as needed
        buttonStyle.fontStyle = FontStyle.Bold; // Bold text
        buttonStyle.alignment = TextAnchor.MiddleCenter; // Center the text
        
        // Create a brown Texture2D for the button background
        Texture2D brownBackground = new Texture2D(1, 1);
        brownBackground.SetPixel(0, 0, new Color(0.3f, 0.15f, 0.0f)); // Brown color (RGB: 153, 76, 0)
        brownBackground.Apply();

        // Assign the brown background texture to the button style
        buttonStyle.normal.background = brownBackground;
        buttonStyle.hover.background = brownBackground;
        buttonStyle.active.background = brownBackground;

        // Set the text color to green
        buttonStyle.normal.textColor = new Color(0.0f, 0.5f, 0.0f); // Dark Green color for text
        buttonStyle.hover.textColor = new Color(0.0f, 0.5f, 0.0f);  // Keep green text on hover
        buttonStyle.active.textColor = new Color(0.0f, 0.5f, 0.0f); // Keep green text on click
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // Update player data
        playerHealth = PlayerPrefs.GetInt("PlayerHealth");
        playerArmor = PlayerPrefs.GetInt("PlayerArmor");
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        playerWeaponLevel = PlayerPrefs.GetInt("PlayerWeaponLevel");
        playerVehicleLevel = PlayerPrefs.GetInt("PlayerVehicleLevel");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        // Save the player data
        PlayerPrefs.SetInt("PlayerHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerArmor", playerArmor);
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
        PlayerPrefs.SetInt("PlayerWeaponLevel", playerWeaponLevel);
        PlayerPrefs.SetInt("PlayerVehicleLevel", playerVehicleLevel);
        PlayerPrefs.SetInt("Volume", volumeVal);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        if (PlayerPrefs.GetInt("PlayerHealth") <= 0) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
        }
    }

    // GUI function
    void OnGUI()
    {
        // Display GUI depending on scene loaded
        // Define some positions (centered on screen)
        float centerX = (Screen.width / 2) - (buttonWidth / 2);
        float centerY = (Screen.height / 2) - (buttonHeight / 2);

        // Main Menu UI
        if (currentScene == "StartMenu") {
            if (!showOptions)
            {
                // Game Name
                GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Zombie Survival Game", titleStyle);

                // Start Button
                if (GUI.Button(new Rect(centerX, centerY, buttonWidth, buttonHeight), "Start", buttonStyle))
                {
                    // Load the game scene (replace "GameScene" with your scene name)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
                }

                // Options Button
                if (GUI.Button(new Rect(centerX, centerY + 60, buttonWidth, buttonHeight), "Options", buttonStyle))
                {
                    // Show options UI
                    showOptions = true;
                }

                // Exit Button
                if (GUI.Button(new Rect(centerX, centerY + 120, buttonWidth, buttonHeight), "Exit", buttonStyle))
                {
                    // Exit the application (only works in a built game)
                    Application.Quit();
                }
            }
            else
            {
                if (!showInstructions) {
                    // Options Menu UI (you can add more options here as needed)
                    GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Options Menu", titleStyle);

                    // Back Button
                    if (GUI.Button(new Rect(centerX, centerY - 50, buttonWidth, buttonHeight), "Back", buttonStyle))
                    {
                        // Return to the main menu
                        showOptions = false;
                    }

                    // Instructions Button
                    if (GUI.Button(new Rect(centerX, centerY + 10, buttonWidth, buttonHeight), "Instructions", buttonStyle))
                    {
                        // Show instructions
                        showInstructions = true;
                    }

                    GUI.Label(new Rect(centerX - 50, centerY + 65, buttonWidth + 50, buttonHeight), "Volume");
                    // Volume Slider
                    volumeVal = (int)GUI.HorizontalSlider(new Rect(centerX, centerY + 70, buttonWidth, buttonHeight), (float)volumeVal, 0.0f, 100.0f);

                    // Reset Stats Button
                    if (GUI.Button(new Rect(centerX, centerY + 130, buttonWidth, buttonHeight), "Reset Stats", buttonStyle))
                    {
                        // Reset player stats
                        PlayerPrefs.SetInt("PlayerHealth", 100);
                        PlayerPrefs.SetInt("PlayerArmor", 0);
                        PlayerPrefs.SetInt("PlayerMoney", 0);
                        PlayerPrefs.SetInt("PlayerWeaponLevel", 1);
                        PlayerPrefs.SetInt("PlayerVehicleLevel", 1);
                        PlayerPrefs.SetInt("CurrentLevel", 0);
                        PlayerPrefs.SetInt("Level1Complete", 0);
                        PlayerPrefs.SetInt("Level2Complete", 0);
                        PlayerPrefs.SetInt("Level3Complete", 0);
                        PlayerPrefs.SetInt("Level4Complete", 0);
                        PlayerPrefs.SetInt("Level5Complete", 0);
                        PlayerPrefs.SetInt("Level6Complete", 0);
                    }

                } else {
                    // Instructions Menu UI (you can add more instructions here as needed)
                    GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Instructions", titleStyle);
                    GUI.Label(new Rect(centerX - 15, centerY - 60, buttonWidth + 50, buttonHeight), "You have to save your family by finding the cure.");
                    GUI.Label(new Rect(centerX - 15, centerY - 20, buttonWidth + 50, buttonHeight), "Get through all the levels to find it.");
                    GUI.Label(new Rect(centerX - 15, centerY + 20, buttonWidth + 50, buttonHeight), "Get money by killing zombies and upgrade your equipment.");

                    // Back Button
                    if (GUI.Button(new Rect(centerX, centerY + 60, buttonWidth, buttonHeight), "Back", buttonStyle))
                    {
                        // Return to the options menu
                        showInstructions = false;
                    }
                }

            }
        } else if (currentScene == "Level1" || currentScene == "Level2" || currentScene == "Level3" || currentScene == "Level4" || currentScene == "Level5" || currentScene == "Level6") {
            // Game UI
            // Display the game UI here
            if (PlayerPrefs.GetInt("CurrentLevelFinished") == 0) {
                if (!gamePause) {
                    // show a pause button in the top right corner
                    if (GUI.Button(new Rect(Screen.width - 100, 10, 90, 40), "Pause", buttonStyle)) {
                        // Pause the game
                        Time.timeScale = 0;
                        PlayerPrefs.SetInt("GamePaused", 1);
                        gamePause = true;
                    }

                    // show level up buttons for weapon, vehicle, and armor
                    if (GUI.Button(new Rect(25, Screen.height - 50, 200, 40), "Level Up Weapon", buttonStyle)) {
                        // Level up the weapon
                        if (playerWeaponLevel < currentLevel * 3) {
                            if (playerMoney >= 100) {
                                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") - 100);
                                PlayerPrefs.SetInt("PlayerWeaponLevel", PlayerPrefs.GetInt("PlayerWeaponLevel") + 1);
                            }
                        }
                    }
                    if (GUI.Button(new Rect(275, Screen.height - 50, 200, 40), "Level Up Vehicle", buttonStyle)) {
                        // Level up the vehicle
                        if (playerVehicleLevel < currentLevel * 3) {
                            if (playerMoney >= 100) {
                                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") - 100);
                                PlayerPrefs.SetInt("PlayerVehicleLevel", PlayerPrefs.GetInt("PlayerVehicleLevel") + 1);
                            }
                        }
                    }
                    if (GUI.Button(new Rect(525, Screen.height - 50, 200, 40), "Level Up Armor", buttonStyle)) {
                        // Level up the armor
                        if (playerMoney >= 100 && PlayerPrefs.GetInt("PlayerArmor") < 100) {
                            PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") - 100);
                            if (PlayerPrefs.GetInt("PlayerArmor") < 100) {
                                PlayerPrefs.SetInt("PlayerArmor", (PlayerPrefs.GetInt("PlayerArmor") + 25 < 100) ? PlayerPrefs.GetInt("PlayerArmor") + 25 : 100);
                            }
                            
                        }
                    }

                    // Display the player's health, armor, and money
                    GUI.Label(new Rect(10, 10, 200, 20), "Health: " + PlayerPrefs.GetInt("PlayerHealth"));
                    GUI.Label(new Rect(10, 30, 200, 20), "Armor: " + PlayerPrefs.GetInt("PlayerArmor"));
                    GUI.Label(new Rect(10, 50, 200, 20), "Money: " + PlayerPrefs.GetInt("PlayerMoney"));
                    GUI.Label(new Rect(10, 70, 200, 20), "Weapon Level: " + PlayerPrefs.GetInt("PlayerWeaponLevel"));
                    GUI.Label(new Rect(10, 90, 200, 20), "Vehicle Level: " + PlayerPrefs.GetInt("PlayerVehicleLevel"));

                } else {
                    if(!showOptions) {
                        // Options Menu UI (you can add more options here as needed)
                        GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Game Paused", titleStyle);

                        // Back Button
                        if (GUI.Button(new Rect(centerX, centerY, buttonWidth, buttonHeight), "Back", buttonStyle))
                        {
                            // Return to the game
                            Time.timeScale = 1;
                            gamePause = false;
                            PlayerPrefs.SetInt("GamePaused", 0);
                        }

                        // Exit Level Button
                        if (GUI.Button(new Rect(centerX, centerY + 60, buttonWidth, buttonHeight), "Exit Level", buttonStyle))
                        {
                            // Return to the LevelSelect scene
                            Time.timeScale = 1;
                            gamePause = false;
                            PlayerPrefs.SetInt("GamePaused", 0);
                            PlayerPrefs.SetInt("CurrentLevel", 0);
                            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
                        }

                        // Options button
                        if (GUI.Button(new Rect(centerX, centerY + 120, buttonWidth, buttonHeight), "Options", buttonStyle))
                        {
                            // Show options UI
                            showOptions = true;
                        }
                    } else {
                        if (!showInstructions) {
                            // Options Menu UI (you can add more options here as needed)
                            GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Options Menu", titleStyle);

                            // Back Button
                            if (GUI.Button(new Rect(centerX, centerY - 50, buttonWidth, buttonHeight), "Back", buttonStyle))
                            {
                                // Return to the main menu
                                showOptions = false;
                            }

                            // Instructions Button
                            if (GUI.Button(new Rect(centerX, centerY + 10, buttonWidth, buttonHeight), "Instructions", buttonStyle))
                            {
                                // Show instructions
                                showInstructions = true;
                            }

                            // Volume Label
                            GUI.Label(new Rect(centerX - 50, centerY + 65, buttonWidth + 50, buttonHeight), "Volume");
                            // Volume Slider
                            volumeVal = (int)GUI.HorizontalSlider(new Rect(centerX, centerY + 70, buttonWidth, buttonHeight), (float)volumeVal, 0.0f, 100.0f);

                        } else {
                            // Instructions Menu UI (you can add more instructions here as needed)
                            GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Instructions", titleStyle);
                            GUI.Label(new Rect(centerX - 15, centerY - 60, buttonWidth + 50, buttonHeight), "You have to save your family by finding the cure.");
                            GUI.Label(new Rect(centerX - 15, centerY - 20, buttonWidth + 50, buttonHeight), "Get through all the levels to find it.");
                            GUI.Label(new Rect(centerX - 15, centerY + 20, buttonWidth + 50, buttonHeight), "Get money by killing zombies and upgrade your equipment.");

                            // Back Button
                            if (GUI.Button(new Rect(centerX, centerY + 60, buttonWidth, buttonHeight), "Back", buttonStyle))
                            {
                                // Return to the options menu
                                showInstructions = false;
                            }
                        }
                    }
                }
            } else {
                GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Level Complete", titleStyle);
                if (GUI.Button(new Rect(centerX, centerY, buttonWidth, buttonHeight), "Return", buttonStyle))
                {
                    int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
                    PlayerPrefs.SetInt("CurrentLevelFinished", 0);
                    PlayerPrefs.SetInt("CurrentLevel", 0);
                    if (currentLevel == 6) {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("GameWin");
                    } else {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
                    }
                }
            }
        } else if (currentScene == "LevelSelect") {
            // Level Select UI
            // Display the level select UI here
            // Level 1 Button
            if (GUI.Button(new Rect(0, centerY + 50, 90, 40), "Level 1", buttonStyle))
            {
                PlayerPrefs.SetInt("CurrentLevel", 1);
                // Load the Level1 scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            }

            string display = "";
            if (PlayerPrefs.GetInt("Level1Complete") == 1) {
                display = "Level 2";
            } else {
                display = "Locked";
            }
            // Level 2 Button
            if (GUI.Button(new Rect((Screen.width / 6), centerY - 50, 90, 40), display, buttonStyle))
            {
                if (PlayerPrefs.GetInt("Level1Complete") == 1) {
                    PlayerPrefs.SetInt("CurrentLevel", 2);
                    // Load the Level2 scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
                }
            }

            string display2 = "";
            if (PlayerPrefs.GetInt("Level2Complete") == 1) {
                display2 = "Level 3";
            } else {
                display2 = "Locked";
            }
            // Level 3 Button
            if (GUI.Button(new Rect((Screen.width / 6) * 2, centerY + 50, 90, 40), display2, buttonStyle))
            {
                if (PlayerPrefs.GetInt("Level2Complete") == 1) {
                    PlayerPrefs.SetInt("CurrentLevel", 3);
                    // Load the Level3 scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
                }
            }

            string display3 = "";
            if (PlayerPrefs.GetInt("Level3Complete") == 1) {
                display3 = "Level 4";
            } else {
                display3 = "Locked";
            }
            // Level 4 Button
            if (GUI.Button(new Rect((Screen.width / 6) * 3, centerY - 50, 90, 40), display3, buttonStyle))
            {
                if (PlayerPrefs.GetInt("Level3Complete") == 1) {
                    PlayerPrefs.SetInt("CurrentLevel", 4);
                    // Load the Level4 scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Level4");
                }
            }

            string display4 = "";
            if (PlayerPrefs.GetInt("Level4Complete") == 1) {
                display4 = "Level 5";
            } else {
                display4 = "Locked";
            }
            // Level 5 Button
            if (GUI.Button(new Rect((Screen.width / 6) * 4, centerY + 50, 90, 40), display4, buttonStyle))
            {
                if (PlayerPrefs.GetInt("Level4Complete") == 1) {
                    PlayerPrefs.SetInt("CurrentLevel", 5);
                    // Load the Level5 scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Level5");
                }
            }

            string display5 = "";
            if (PlayerPrefs.GetInt("Level5Complete") == 1) {
                display5 = "Level 6";
            } else {
                display5 = "Locked";
            }
            // Level 6 Button
            if (GUI.Button(new Rect((Screen.width / 6) * 5, centerY - 50, 90, 40), display5, buttonStyle))
            {
                if (PlayerPrefs.GetInt("Level5Complete") == 1) {
                    PlayerPrefs.SetInt("CurrentLevel", 6);
                    // Load the Level6 scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Level6");
                }
            }

            // Back Button
            if (GUI.Button(new Rect(Screen.width - 100, 10, 90, 40), "Back", buttonStyle))
            {
                PlayerPrefs.SetInt("CurrentLevel", 0);
                // Load the main menu scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
            }
        } else if (currentScene == "EndGame") {
            // End Game UI
            // Display the end game UI here
            // Game Name
            GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "Game Over", titleStyle);

            // Restart Button
            if (GUI.Button(new Rect(centerX, centerY, buttonWidth, buttonHeight), "Restart", buttonStyle))
            {
                // Reset stats
                PlayerPrefs.SetInt("PlayerHealth", 100);
                PlayerPrefs.SetInt("PlayerArmor", 0);
                PlayerPrefs.SetInt("PlayerMoney", 0);
                PlayerPrefs.SetInt("PlayerWeaponLevel", 1);
                PlayerPrefs.SetInt("PlayerVehicleLevel", 1);
                PlayerPrefs.SetInt("CurrentLevel", 0);
                // Load the LevelSelect scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
            }
        } else if (currentScene == "GameWin") {
            // Game Win UI
            // Display the game win UI here
            // Game Name
            GUI.Label(new Rect(centerX - 15, centerY - 100, buttonWidth + 50, buttonHeight), "You Win!", titleStyle);
            // Restart Button
            if (GUI.Button(new Rect(centerX, centerY, buttonWidth, buttonHeight), "Restart", buttonStyle))
            {
                // Reset stats
                PlayerPrefs.SetInt("PlayerHealth", 100);
                PlayerPrefs.SetInt("PlayerArmor", 0);
                PlayerPrefs.SetInt("PlayerMoney", 0);
                PlayerPrefs.SetInt("PlayerWeaponLevel", 1);
                PlayerPrefs.SetInt("PlayerVehicleLevel", 1);
                PlayerPrefs.SetInt("CurrentLevel", 0);
                // Load the LevelSelect scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
            }

            // StartMenu Button
            if (GUI.Button(new Rect(centerX, centerY + 60, buttonWidth, buttonHeight), "Start Menu", buttonStyle))
            {
                // Load the StartMenu scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
            }

            if (GUI.Button(new Rect(centerX, centerY + 120, buttonWidth, buttonHeight), "Exit", buttonStyle))
            {
                // Exit the application (only works in a built game)
                Application.Quit();
            }
        }
    }
}
