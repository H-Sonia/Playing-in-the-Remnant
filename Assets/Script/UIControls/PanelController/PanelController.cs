using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelController : MonoBehaviour
{
    public GameObject ResourcesPanel;
    public TMP_Text ResourceDescription;
    public TMP_Text ResourcesCounter;
    public Image RessourceImage;
    public Sprite emptyImage;
    public UIDisplay MainUI;
    string characterMessage;


    public void Setup()
    {
        UpdatePanelUI();
        MainManager.instance.MainCheck();
    }
   

    public void Quit()
    {
        ResourcesPanel.SetActive(false);
        Debug.LogWarning(characterMessage);
        MainUI.UpdateMainUi(characterMessage, false);
        characterMessage = "";
    }

    public void UpdatePanelUI()
    {
        int count = Inventory.instance.content.Count;
        ResourcesCounter.text = count.ToString();

        if (Inventory.instance.content.Count > 0)
        {
            ResourceDescription.text = Inventory.instance.content[Inventory.instance.currentResource].itemName;
            RessourceImage.sprite = Inventory.instance.content[Inventory.instance.currentResource].itemImage;
        }
        else 
        {
            ResourceDescription.text = "";
            RessourceImage.sprite = emptyImage;
        }

    }

    public void GetNextResources()
    {
        if(Inventory.instance.content.Count == 0)
        {
            return;
        }

        Inventory.instance.currentResource++;
        if(Inventory.instance.currentResource > Inventory.instance.content.Count - 1)
        {
            Inventory.instance.currentResource = 0;
        }

        UpdatePanelUI();
    }

    public void GetPreviousResources()
    {
        if (Inventory.instance.content.Count == 0)
        {
            return;
        }

        Inventory.instance.currentResource--;
        if (Inventory.instance.currentResource < 0)
        {
            Inventory.instance.currentResource = Inventory.instance.content.Count - 1;
        }

        UpdatePanelUI();
    }

    public void GiveResources()
    {
        ItemData currentResource = Inventory.instance.content[Inventory.instance.currentResource];
        CharacterManager.instance.charactersLists.CharactersInDorm[CharacterManager.instance.charactersLists.currentCharacter].resourcesAttribuated.Add(currentResource);
        CharacterManager.instance.charactersLists.CharactersInDorm[CharacterManager.instance.charactersLists.currentCharacter].daysBeforeExpiration.Add(currentResource.daysBeforeExpiration);
        CharacterManager.instance.charactersLists.CharactersInDorm[CharacterManager.instance.charactersLists.currentCharacter].friendshipLevel += 1;
        Inventory.instance.content.Remove(currentResource);
        GetNextResources();
        UpdatePanelUI();
        characterMessage = CharacterManager.instance.charactersLists.CharactersInDorm[CharacterManager.instance.charactersLists.currentCharacter].firstname + " thanks you.\n";
        
    }

}
