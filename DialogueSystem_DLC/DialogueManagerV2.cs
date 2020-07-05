using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/********************
 * DIALOGUE MANAGER *
 ********************
 * This Dialogue Manager is what links your dialogue which is sent by the Dialogue Trigger to Unity
 *
 * The Dialogue Manager navigates the sent text and prints it to text objects in the canvas and will toggle
 * the Dialogue Box when appropriate
 * 
 * A script by Michael O'Connell, extended by Benjamin Cohen
 */

public class DialogueManagerV2 : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("your fancy canvas image that holds your text objects")]
    public GameObject CanvasBox;


    [Tooltip("Your text body")]
    public Text TextBox; // the text body
    [Tooltip("the text body of the name you want to display")]
    public Text NameText; // the text body of the name you want to display
    [Tooltip("Image where the speaker images will appear")]
    public Image speaker; //Image where the speaker images will appear


    // private bool isOpen; // represents if the dialogue box is open or closed

    private Queue<string> inputStream = new Queue<string>(); // stores dialogue


    /* IMPORTANT IMPORTANT IMPORTANT IMPORTANT */
    /*This variable will store the script we need to access to stop player movement! If you are using a different movement script
     * make sure to change this variable appropriately and to make sure the movement script has a way to prevent character movement!
    */
    private PlayerMovement_SideScroller playerMovement;

    /* ^ IMPORTANT IMPORTANT IMPORTANT IMPORTANT ^*/

    [HideInInspector]
    public DialogueTriggerV2 currentTrigger;

    private bool levelBool = false;
    private int levelIndex;

    
    [System.Serializable]
    public class SpriteInfo
    {
        public string name;
        public Sprite sprite;
    }
    [Header("Dialogue Image")]
    [Tooltip("Invisible/Placeholder sprite for when no one is talking")]
    public Sprite invisSprite;
    public List<SpriteInfo> speakerSpriteList = new List<SpriteInfo>();
    [HideInInspector]
    public List<string> speakerSpriteNames;
    
    

    [Header("Options")]
    public bool freezePlayerOnDialogue = true;

    private void Start()
    {

        foreach(SpriteInfo info in speakerSpriteList)
        {
            speakerSpriteNames.Add(info.name);
        }
        speaker.sprite = invisSprite;
    }

    private void FreezePlayer()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_SideScroller>();
        playerMovement.frozen = true;
    }

    private void UnFreezePlayer()
    {
        playerMovement.frozen = false;
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        speaker.sprite = invisSprite; //Clear the speaker
        CanvasBox.SetActive(true);
        if (freezePlayerOnDialogue)
        {
            FreezePlayer();
        }


        // open the dialogue box
        // isOpen = true;
        inputStream = dialogue; // store the dialogue from dialogue trigger
        PrintDialogue(); // Prints out the first line of dialogue
    }

    public void AdvanceDialogue() // call when a player presses a button in Dialogue Trigger
    {
        PrintDialogue();

    }

    private void PrintDialogue()
    {
        if (inputStream.Peek().Contains("EndQueue")) // special phrase to stop dialogue
        {
            inputStream.Dequeue(); // Clear Queue
            EndDialogue();
        }
        else if (inputStream.Peek().Contains("[NAME=")) //Set the name of the speaker
        {
            string name = inputStream.Peek();
            name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
            NameText.text = name;
            PrintDialogue(); // print the rest of this line
        }
        else if (inputStream.Peek().Contains("[LEVEL=")) //On dialogue finish, go to following level
        {
            string part = inputStream.Peek();
            string level = inputStream.Dequeue().Substring(part.IndexOf('=') + 1, part.IndexOf(']') - (part.IndexOf('=') + 1));
            int levelIndex = Convert.ToInt32(level); //Convert string to integer
            levelBool = true;
            PrintDialogue(); // print the rest of this line
        }
        else if (inputStream.Peek().Contains("[SPEAKERSPRITE="))
        {
            string part = inputStream.Peek();
            string spriteName = inputStream.Dequeue().Substring(part.IndexOf('=') + 1, part.IndexOf(']') - (part.IndexOf('=') + 1));
            if(spriteName != "")
            {
                speaker.sprite = speakerSpriteList[speakerSpriteNames.IndexOf(spriteName)].sprite; //sets the speaker sprite to corresponding sprite
            }
            else
            {
                speaker.sprite = invisSprite;
            }
            PrintDialogue(); // print the rest of this line


        }
        else
        {
            TextBox.text = inputStream.Dequeue();
        }


    }

    public void EndDialogue()
    {
        TextBox.text = "";
        NameText.text = "";
        inputStream.Clear();
        CanvasBox.SetActive(false);


        // isOpen = false;
        if (freezePlayerOnDialogue)
        {
            UnFreezePlayer();
        }
        if (levelBool)
        {
            SceneManager.LoadScene(levelIndex);
        }
        if (currentTrigger.singleUseDialogue)
        {
            currentTrigger.hasBeenUsed = true;
        }
        inputStream.Clear();
    }
}
