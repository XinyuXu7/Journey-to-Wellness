using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject ItemInfoUI;
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject> ();

    public List<string> itemList = new List<string> ();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    public bool isFull;

    //Pickup Popup
    public GameObject pickupAlert;
    public Text pickupName;
    public Image pickupImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if(child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.B) && isOpen)
        {
            inventoryScreenUI.SetActive(false);

            if(!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }


    public void AddToInventory(string itemName)
    {
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName),whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);

        TriggerPickupPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);




        ReCalculeList();
        CraftingSystem.Instance.RefreshNeededItems();
        
    }

    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);

        pickupName.text = itemName;

        pickupImage.sprite = itemSprite;
    }



    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }

            
        }
        return new GameObject();
    }

    public bool CheckifFull()
    {
        int counter = 0;

        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount >0)
            {
                counter += 1;
            }

        }

        if(counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int removedCount = 0;

        // Iterate over the list from end to start.
        for (var i = slotList.Count - 1; i >= 0 && removedCount < amountToRemove; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                string itemName = slotList[i].transform.GetChild(0).name;

                // If the item matches the name to remove, destroy it.
                if (itemName == nameToRemove + "(Clone)")
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    removedCount++;
                }
            }
        }

        ReCalculeList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void ReCalculeList()
    {
        itemList.Clear();

        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str1 = "(Clone)";
                string result = name.Replace(str1,"");

                itemList.Add(result);
            }
        }
    }

}