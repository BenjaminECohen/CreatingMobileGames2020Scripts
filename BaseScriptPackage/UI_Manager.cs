using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    //This script can be attached to the same gameobject as the Game Manager. This manager handles health and score representation on the Canvas UI

    public GameManager_SideScroller gameManager;
    [Tooltip("Drag and drop the player gameobject that has the Health Script attached")]
    public Health playerHealth;
    [Header("Health Bar")]
    public Image healthBar;
    [Tooltip("Set to true if you want to use this functionality")]
    public bool useHealthBar = false;
    [Header("Health Icons")]
    [Tooltip("WARNING: YOU CANNOT USE OVERHEALTH FOR ICONS! \n\nOrder matters for this list. The first health icon that will be taken away on hit must be the element 0 and so on and so forth. The last icon to disappear should be at the end of the list" +
        "\n\nWARNING: Make sure your health values and the number of icons divide evenly. Additionally, make sure all weapon damage creates a clean ratio between a players health and their max health." +
        " To make things simple, it is highly recommended to keep player health equal to the number of health icons you want (Ex: 3 icons, player max health is 3).")]
    public List<Image> healthIcon;
    [Tooltip("Set to true if you want to use this functionality")]
    public bool useHealthIcons = false;
   
    private int maxHealth;
    private int health;
    private int iconsActive;
    private int previousIconsActive = -1;


    [Header("Score")]
    [Tooltip("Text box where the score will be displayed")]
    public Text scoreText;
    [Tooltip("Set to true if you want to use this functionality")]
    public bool useScore = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = playerHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health != health) //Small check to only use the functions if the players health has changed
        {
            health = playerHealth.health; //update the health in this script to the player's health

            if (useHealthBar) //Updates health bar
            {
                healthToHealthBar();
            }

            if (useHealthIcons) //updates health icon
            {
                healthToIcons();
            }
        }
        

        if (useScore) //updates score
        {
            scoreText.text = "Score: " + gameManager.score.ToString();
        }
        
        
    }

    private void healthToIcons()
    {
        int factor = Mathf.RoundToInt((float) maxHealth / (float)healthIcon.Count);
        iconsActive = Mathf.RoundToInt((float)health / (float) factor);

        //Accounts for first update
        if (previousIconsActive == -1)
        {
            previousIconsActive = iconsActive;
        }

        if (previousIconsActive != iconsActive)
        {
            //Activate
            for (int i = healthIcon.Count - iconsActive; i < healthIcon.Count; i++)
            {
                healthIcon[i].gameObject.SetActive(true);
            }
            //Deactivate
            for (int i = 0; i < healthIcon.Count - iconsActive; i++)
            {
                healthIcon[i].gameObject.SetActive(false);
            }
            
        }

        previousIconsActive = iconsActive;

    }

    private void healthToHealthBar()
    {
        float percentage = (float)health / (float)maxHealth;
        healthBar.fillAmount = percentage;
    }


}
