using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    public TMP_Text[] names;
    public GameObject[] giveButtons;
    public GameObject[] idButtons;
    public CharactersData empty;
    public GameObject infoPanel;
    public ScrollRect scrollRect;
    public TMP_Text infos;

    public GameObject Dorm;
    public void Setup()
    {
        Dorm = GameObject.Find("Dorm");
        if (Dorm != null)
        {
            names = Dorm.transform.GetChild(0).Find("Names").GetComponentsInChildren<TMP_Text>();
            Transform[] children;

            children = Dorm.transform.GetChild(0).Find("GiveButtons").GetComponentsInChildren<Transform>();
            giveButtons = new GameObject[(children.Length-1)/2];
            int j = 0;
            for (int i = 1; i < children.Length; i++)
            {
                int temp = j;
                if (children[i].name.Contains("give"))
                {
                    giveButtons[j] = children[i].gameObject;
                    children[i].GetComponent<Button>().onClick.AddListener(()=>GiveButtonClick(temp));
                    j++;
                }
            }
            /*
            children = Dorm.transform.GetChild(0).Find("IDButtons").GetComponentsInChildren<Transform>();
            idButtons = new GameObject[(children.Length-1) / 2];
            j = 0;
            for (int i = 1; i < children.Length; i++)
            {
                int temp = j;
                if (children[i].name.Contains("ID"))
                {
                    idButtons[j] = children[i].gameObject;
                    children[i].GetComponent<Button>().onClick.AddListener(() => IDButtonClick(temp));
                    j++;
                }
            }*/
        }
        MainManager.instance.MainCheck();
    }

    public void IDButtonClick(int id)
    {
        Debug.Log(id);
        GetComponent<ButtonController>().openIdPanel(id);
    }

    public void GiveButtonClick(int id)
    {
        GetComponent<ButtonController>().openResourcesPanel(id);
    }

    public void DayFunction()
    {

    }

    public void UpdateMainUi(string characterMessage = "", bool opening = false)
    {
        scrollRect.verticalNormalizedPosition = 1f;
        if (opening)
        {
            infoPanel.SetActive(true);
        }
        else
        {
            if(characterMessage == "UPDATE")
            {
                infoPanel.SetActive(true);
            }
            else if (characterMessage != "")
            {
                infos.text = characterMessage;
                infoPanel.SetActive(true);
            }

        }   

        if (Inventory.instance.content.Count > 0)
                giveButtons[0].transform.parent.gameObject.SetActive(true);
        else
            giveButtons[0].transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < names.Length && i < CharacterManager.instance.charactersLists.CharactersInDorm.Count; i++)
        {
            Character character = CharacterManager.instance.charactersLists.CharactersInDorm[i];
            
                if(character.isKey)
                {
                    names[i].text = "[K] " + character.firstname + " " + character.surname;
                }
                else if (character.friendshipLevel > 0)
                {
                    names[i].text = character.firstname + " " + character.surname;
                    //idButtons[i].SetActive(true);
                }
                else
                {
                    names[i].text = "";
                    //idButtons[i].SetActive(true);
                }

                //if (Inventory.instance.content.Count > 0)
                //    giveButtons[i].SetActive(true);
                //else
                //    giveButtons[i].SetActive(false);
        }
    }

    public void QuitInfoPanel()
    {
        infoPanel.SetActive(false);
        infos.text = "";
    }

    
}
