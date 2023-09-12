using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class mangy : MonoBehaviour
{
    // Start is called before the first frame update
    private List<int> playerTaskList = new List<int>();
    private List<int> playerSequenceList = new List<int>();

    public List<AudioClip> soundsList = new List<AudioClip>();

    //color 32 numbers 0-25
    public List<List<Color32>> buttonColors = new List<List<Color32>>();

    public List<Button> clickableButtons;
    public CanvasGroup buttons;
    public AudioClip loseSound;
    public AudioSource AudioSource;
    public GameObject startButton;

    public RectTransform buttonsParent;

    public void Awake()
    {
        //list of colors
        //.add adds to the list
        //first list
        //then initialize new list of color 32
        //initilize colors
        buttonColors.Add(new List<Color32>
        {
            new Color32(255, 0, 0, 255),
            new Color32(255, 100, 100, 255)
        });
        buttonColors.Add(new List<Color32>
        {
            new Color32(255, 136, 0, 255),
            new Color32(255, 187, 109, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(72, 248, 0, 255),
            new Color32(162, 255, 124, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(10, 70, 255, 255),
            new Color32(57, 111, 255, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(245, 40, 145, 255),
            new Color32(245, 40, 145, 150),
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(255, 255, 73, 255),
            new Color32(255, 255, 73, 150)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(59, 70, 155, 255),
            new Color32(39, 47, 110, 150)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(135, 52, 0, 255),
            new Color32(136, 70, 29, 150)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(0, 236, 255, 255),
            new Color32(121, 245, 255, 150)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(0, 136, 99, 255),
            new Color32(28, 133, 105, 150)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(156, 156, 156, 255),
            new Color32(201, 201, 201, 150)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(75, 0, 4, 255),
            new Color32(80, 27, 29, 150)
        });

        //get component sprite render color and set the color
        //set button color to [first list][and first value]
        for (int i = 0; i < 11; i++)
        {
            clickableButtons[i].GetComponent<Image>().color 
                = buttonColors[i][0];
        }
    }
    
    //every time we click a sprite we need to add it to sequence list
    public void AddToPlayerSequenceList(int buttonID)
    {
        //add click button ID to the players sequence list
        playerSequenceList.Add(buttonID);
        //highlight the button and play a sound
        StartCoroutine(HighLightButton(buttonID));
        
        //check if player sequence matches the task list
        for (int i = 0; i < playerSequenceList.Count; i++)
        {
            //if the value are the same continue
            if (playerTaskList[i] == playerSequenceList[i])
            {
                continue;
                
            }
            else
            {
                StartCoroutine(PlayerLost());
                return;
            }
        }

        if (playerSequenceList.Count == playerTaskList.Count)
        {
            Debug.Log("start next round");
            StartCoroutine(StartNextRound());
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartNextRound());
        startButton.SetActive(false);
    }

    public IEnumerator HighLightButton(int buttonID)
    {
        //highlight the button by changing its color and playing a sound
        clickableButtons[buttonID].GetComponent<Image>().color = buttonColors[buttonID][1];
        AudioSource.PlayOneShot(soundsList[buttonID]);
        //wait .5 in between
        yield return new WaitForSeconds(.5f);
        //change color
        clickableButtons[buttonID].GetComponent<Image>().color = buttonColors[buttonID][0];
    }

    public IEnumerator PlayerLost()
    {
        AudioSource.PlayOneShot(loseSound);
        //clear the tasks and sequence when player loses
        playerSequenceList.Clear();
        playerTaskList.Clear();
        //wait for 2 seconds activate start button
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true); //show button after losing
    }

    //delay between the start button and next round
    public IEnumerator StartNextRound()
    {
        
        //have to clear player sequence list
        playerSequenceList.Clear();
        //turn off buttons
        buttons.interactable = false;
        //wait .5f before adding a new random player task list
        yield return new WaitForSeconds(2f);

        ShuffleButtonPos();

        yield return new WaitForSeconds(1f);
        
        int taskCount = Random.Range(1, 5);
        playerTaskList.Clear();
        for (int i = 0; i < taskCount; i++)
        {
            playerTaskList.Add(Random.Range(0, 11));
            playerTaskList.Add(Random.Range(0, 11));
            //playerTaskList.Add(Random.Range(0,11));//add a random task
        }
        
        foreach (int index in playerTaskList)
        {
            yield return StartCoroutine(HighLightButton(index));


        }
        buttons.interactable = true;
        yield return null;
    }

    public void ShuffleButtonPos()
    {
        int buttonCount = clickableButtons.Count;
        for (int i = 0; i < buttonCount; i++)
        {
            int randomIndex = Random.Range(i, buttonCount);
            //swap positions of button at i and random index
            Vector2 tempPos = clickableButtons[i].transform.localPosition;
            clickableButtons[i].transform.localPosition = clickableButtons[randomIndex].transform.localPosition;
            clickableButtons[randomIndex].transform.localPosition = tempPos;
        }
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
