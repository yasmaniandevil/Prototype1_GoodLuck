using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
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
        buttonColors.Add(new List<Color32>
        {
            new Color32(255, 100, 100, 255),
            new Color32(255, 0, 0, 255)
        });
        buttonColors.Add(new List<Color32>
        {
            new Color32(255, 187, 109, 255),
            new Color32(255, 136, 0, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(162, 255, 124, 255),
            new Color32(72, 248, 0, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(57, 111, 255, 255),
            new Color32(0, 70, 255, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(245, 40, 145, 150),
            new Color32(245, 40, 145, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(255, 255, 73, 150),
            new Color32(255, 255, 73, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(39, 47, 110, 150),
            new Color32(59, 70, 155, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(136, 70, 29, 150),
            new Color32(135, 52, 0, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(121, 245, 255, 150),
            new Color32(0, 236, 255, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(28, 133, 105, 150),
            new Color32(0, 136, 99, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(201, 201, 201, 150),
            new Color32(156, 156, 156, 255)
        });
        
        buttonColors.Add(new List<Color32>
        {
            new Color32(80, 27, 29, 150),
            new Color32(75, 0, 4, 255)
        });

        //get component sprite render color and set the color
        //set button color to [first list][and first value]
        for (int i = 0; i < 12; i++)
        {
            clickableButtons[i].GetComponent<Image>().color 
                = buttonColors[i][0];
        }
    }
    
    //every time we click a sprite we need to add it to sequence list
    public void AddToPlayerSequenceList(int buttonID)
    {
        playerSequenceList.Add(buttonID);
        StartCoroutine(HighLightButton(buttonID));
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
        clickableButtons[buttonID].GetComponent<Image>().color = buttonColors[buttonID][1];
        AudioSource.PlayOneShot(soundsList[buttonID]);
        yield return new WaitForSeconds(.5f);
        clickableButtons[buttonID].GetComponent<Image>().color = buttonColors[buttonID][0];
    }

    public IEnumerator PlayerLost()
    {
        AudioSource.PlayOneShot(loseSound);
        //clear the tasks and sequence player clicked
        playerSequenceList.Clear();
        playerTaskList.Clear();
        yield return new WaitForSeconds(2f);
        startButton.SetActive(true);
    }

    //delay between the start button and next round
    public IEnumerator StartNextRound()
    {
        ShuffleButtonPos();
        //have to clear player sequence list
        playerSequenceList.Clear();
        buttons.interactable = false;
        yield return new WaitForSeconds(1f);
        playerTaskList.Add(Random.Range(0, 4));
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
}
