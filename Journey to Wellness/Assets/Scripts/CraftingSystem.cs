using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public GameObject foodsScreenUI;

    private List<string> inventoryItemList = new List<string>();

    //Category Button
    Button toolsBTN;
    Button foodsBTN;


    //Craft Buttons
    Button craftAxeBTN;
    Button craftFruitSaladBTN;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;
    TextMeshProUGUI FruitSaladReq1, FruitSaladReq2;

    public bool isOpen;

    //All blueprint
    public Blueprint AxeBLP = new Blueprint("Axe", 2, "Stone", 3, "Stick", 3);
    public Blueprint FruitSaladBLP = new Blueprint("FruitSalad", 2, "Apple", 2, "Lemon", 1);


    public static CraftingSystem Instance { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen= false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        foodsBTN = craftingScreenUI.transform.Find("FoodsButton").GetComponent<Button>();
        foodsBTN.onClick.AddListener(delegate { OpenFoodsCategory(); });

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });


        //Fruit salad
        FruitSaladReq1 = foodsScreenUI.transform.Find("FruitSalad").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        FruitSaladReq2 = foodsScreenUI.transform.Find("FruitSalad").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftFruitSaladBTN = foodsScreenUI.transform.Find("FruitSalad").transform.Find("Button").GetComponent<Button>();
        craftFruitSaladBTN.onClick.AddListener(delegate { CraftAnyItem(FruitSaladBLP); });
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    void OpenFoodsCategory()
    {
        craftingScreenUI.SetActive(false);
        foodsScreenUI.SetActive(true);
    }


    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);

        InventorySystem.Instance.ReCalculeList();
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        //add item into inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        //InventorySystem.Instance.ReCalculeList();
        StartCoroutine(calculate());

        RefreshNeededItems();

    }
    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            foodsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }

    private void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        int apple_count = 0;
        int lemon_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;
        
        foreach(string itemName in inventoryItemList)
        {
            switch(itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1;
                    break;
                case "Apple":
                    apple_count += 1;
                    break;
                case "Lemon":
                    lemon_count += 1;
                    break;
                default:
                    Debug.Log("Unknown item: " + itemName);
                    break;
            }
        }

        //AXE
        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if(stone_count >= 3 && stick_count >=3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }

        //Fruit Salad
        FruitSaladReq1.text = "2 Apple [" + apple_count + "]";
        FruitSaladReq2.text = "1 Lemon [" + lemon_count + "]";

        if (apple_count >= 2 && lemon_count >= 1)
        {
            craftFruitSaladBTN.gameObject.SetActive(true);
        }
        else
        {
            craftFruitSaladBTN.gameObject.SetActive(false);
        }
    }

}
