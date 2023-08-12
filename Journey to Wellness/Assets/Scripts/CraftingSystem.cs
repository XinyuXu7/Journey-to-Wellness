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
    public GameObject foodsScreenUI1;
    public GameObject foodsScreenUI2;
    public GameObject foodsScreenUI3;
    public GameObject foodsScreenUI4;
    public GameObject foodsScreenUI5;
    public GameObject constructionScreenUI;
    public GameObject constructionScreenUI1;
    public GameObject refineScreenUI;

    private List<string> inventoryItemList = new List<string>();

    //Category Button
    Button toolsBTN;
    Button foodsBTN, foodsBTN1, foodsBTN2, foodsBTN3, foodsBTN4, foodsBTN5, constructionBTN, constructionBTN1, refineBTN;


    //Craft Buttons
    Button craftAxeBTN;
    Button craftFruitSaladBTN, craftCarrotMilkshakeBTN, craftMushroomOnionBTN, craftAvocadoToastBTN, craftBroccoliMeatBTN, craftLemonCucumberBTN;
    Button craftPlankBTN;
    Button craftFoundationBTN, craftWallBTN;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;
    TextMeshProUGUI FruitSaladReq1, FruitSaladReq2;
    TextMeshProUGUI CarrotMilkshakeReq1, CarrotMilkshakeReq2;
    TextMeshProUGUI MushroomOnionReq1, MushroomOnionReq2;
    TextMeshProUGUI AvocadoToastReq1, AvocadoToastReq2;
    TextMeshProUGUI BroccoliMeatReq1, BroccoliMeatReq2;
    TextMeshProUGUI LemonCucumberReq1, LemonCucumberReq2;
    TextMeshProUGUI PlankReq1;
    TextMeshProUGUI FoundationReq1;
    TextMeshProUGUI WallReq1;

    public bool isOpen;

    //All blueprint
    public Blueprint AxeBLP = new Blueprint("Axe",1, 2, "Stone", 3, "Stick", 3);
    public Blueprint CarrotMilkshakeBLP = new Blueprint("CarrotMilkshake", 1, 2, "Carrot", 2, "Yogurt", 1);
    public Blueprint FruitSaladBLP = new Blueprint("FruitSalad", 1, 2, "Apple", 2, "Pear", 1);
    public Blueprint MushroomOnionBLP = new Blueprint("MushroomOnion", 1, 2, "Mushroom", 2, "Onion", 1);
    public Blueprint AvocadoToastBLP = new Blueprint("AvocadoToast", 1, 2, "Avocado", 1, "Bread", 1);
    public Blueprint BroccoliMeatBLP = new Blueprint("BroccoliMeat", 1, 2, "Broccoli", 1, "Meat", 1);
    public Blueprint LemonCucumberBLP = new Blueprint("LemonCucumber", 1, 2, "Lemon", 1, "Cucumber", 1);
    public Blueprint PlankBLP = new Blueprint("Plank",2, 1, "Log", 1, "", 0);
    public Blueprint FoundationBLP = new Blueprint("Foundation", 1, 1, "Plank", 4, "", 0);
    public Blueprint WallBLP = new Blueprint("Wall", 1, 1, "Plank", 2, "", 0);


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

        foodsBTN = craftingScreenUI.transform.Find("FoodsButtonFruitSalad").GetComponent<Button>();
        foodsBTN.onClick.AddListener(delegate { OpenFoodsCategory(); });

        foodsBTN1 = craftingScreenUI.transform.Find("FoodsButtonCarrotMilkshake").GetComponent<Button>();
        foodsBTN1.onClick.AddListener(delegate { OpenFoodsCategory1(); });

        foodsBTN2 = craftingScreenUI.transform.Find("FoodsButtonMushroomOnion").GetComponent<Button>();
        foodsBTN2.onClick.AddListener(delegate { OpenFoodsCategory2(); });

        foodsBTN3 = craftingScreenUI.transform.Find("FoodsButtonAvocadoToast").GetComponent<Button>();
        foodsBTN3.onClick.AddListener(delegate { OpenFoodsCategory3(); });

        foodsBTN4 = craftingScreenUI.transform.Find("FoodsButtonBroccoliMeat").GetComponent<Button>();
        foodsBTN4.onClick.AddListener(delegate { OpenFoodsCategory4(); });

        foodsBTN5 = craftingScreenUI.transform.Find("FoodsButtonLemonCucumber").GetComponent<Button>();
        foodsBTN5.onClick.AddListener(delegate { OpenFoodsCategory5(); });

        constructionBTN = craftingScreenUI.transform.Find("ConstructionButtonWall").GetComponent<Button>();
        constructionBTN.onClick.AddListener(delegate { OpenConstructionCategory(); });

        constructionBTN1 = craftingScreenUI.transform.Find("ConstructionButtonFoundation").GetComponent<Button>();
        constructionBTN1.onClick.AddListener(delegate { OpenConstructionCategory1(); });

        refineBTN = craftingScreenUI.transform.Find("RefineButton").GetComponent<Button>();
        refineBTN.onClick.AddListener(delegate { OpenRefineCategory(); });

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

        //Carrot Milkshake
        CarrotMilkshakeReq1 = foodsScreenUI1.transform.Find("CarrotMilkshake").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        CarrotMilkshakeReq2 = foodsScreenUI1.transform.Find("CarrotMilkshake").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftCarrotMilkshakeBTN = foodsScreenUI1.transform.Find("CarrotMilkshake").transform.Find("Button").GetComponent<Button>();
        craftCarrotMilkshakeBTN.onClick.AddListener(delegate { CraftAnyItem(CarrotMilkshakeBLP); });

        //Mushroom Onion
        MushroomOnionReq1 = foodsScreenUI2.transform.Find("MushroomOnion").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        MushroomOnionReq2 = foodsScreenUI2.transform.Find("MushroomOnion").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftMushroomOnionBTN = foodsScreenUI2.transform.Find("MushroomOnion").transform.Find("Button").GetComponent<Button>();
        craftMushroomOnionBTN.onClick.AddListener(delegate { CraftAnyItem(MushroomOnionBLP); });

        //Avocado Toast
        AvocadoToastReq1 = foodsScreenUI3.transform.Find("AvocadoToast").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AvocadoToastReq2 = foodsScreenUI3.transform.Find("AvocadoToast").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftAvocadoToastBTN = foodsScreenUI3.transform.Find("AvocadoToast").transform.Find("Button").GetComponent<Button>();
        craftAvocadoToastBTN.onClick.AddListener(delegate { CraftAnyItem(AvocadoToastBLP); });

        //Broccoli Meat
        BroccoliMeatReq1 = foodsScreenUI4.transform.Find("BroccoliMeat").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        BroccoliMeatReq2 = foodsScreenUI4.transform.Find("BroccoliMeat").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftBroccoliMeatBTN = foodsScreenUI4.transform.Find("BroccoliMeat").transform.Find("Button").GetComponent<Button>();
        craftBroccoliMeatBTN.onClick.AddListener(delegate { CraftAnyItem(BroccoliMeatBLP); });

        //Lemon Cucumber Drink
        LemonCucumberReq1 = foodsScreenUI5.transform.Find("LemonCucumber").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        LemonCucumberReq2 = foodsScreenUI5.transform.Find("LemonCucumber").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftLemonCucumberBTN = foodsScreenUI5.transform.Find("LemonCucumber").transform.Find("Button").GetComponent<Button>();
        craftLemonCucumberBTN.onClick.AddListener(delegate { CraftAnyItem(LemonCucumberBLP); });

        //Plank
        PlankReq1 = refineScreenUI.transform.Find("Plank").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        craftPlankBTN = refineScreenUI.transform.Find("Plank").transform.Find("Button").GetComponent<Button>();
        craftPlankBTN.onClick.AddListener(delegate { CraftAnyItem(PlankBLP); });

        //Foundation
        FoundationReq1 = constructionScreenUI1.transform.Find("Foundation").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        craftFoundationBTN = constructionScreenUI1.transform.Find("Foundation").transform.Find("Button").GetComponent<Button>();
        craftFoundationBTN.onClick.AddListener(delegate { CraftAnyItem(FoundationBLP); });

        //Wall
        WallReq1 = constructionScreenUI.transform.Find("Wall").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        craftWallBTN = constructionScreenUI.transform.Find("Wall").transform.Find("Button").GetComponent<Button>();
        craftWallBTN.onClick.AddListener(delegate { CraftAnyItem(WallBLP); });
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
    void OpenFoodsCategory1()
    {
        craftingScreenUI.SetActive(false);
        foodsScreenUI1.SetActive(true);
    }
    void OpenFoodsCategory2()
    {
        craftingScreenUI.SetActive(false);
        foodsScreenUI2.SetActive(true);
    }
    void OpenFoodsCategory3()
    {
        craftingScreenUI.SetActive(false);
        foodsScreenUI3.SetActive(true);
    }
    void OpenFoodsCategory4()
    {
        craftingScreenUI.SetActive(false);
        foodsScreenUI4.SetActive(true);
    }
    void OpenFoodsCategory5()
    {
        craftingScreenUI.SetActive(false);
        foodsScreenUI5.SetActive(true);
    }

    void OpenConstructionCategory()
    {
        craftingScreenUI.SetActive(false);
        constructionScreenUI.SetActive(true);
    }
    void OpenConstructionCategory1()
    {
        craftingScreenUI.SetActive(false);
        constructionScreenUI1.SetActive(true);
    }

    void OpenRefineCategory()
    {
        craftingScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculeList();
        RefreshNeededItems(); 
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        Debug.Log("Crafting: " + blueprintToCraft.itemName);
        SoundManager.Instance.PlaySound(SoundManager.Instance.craftingSound);

        //Produce the amount of items according to the blueprint
        for (var i = 0;i< blueprintToCraft.numOfItemsToProduce;i++)
        {
            //add item into inventory
            InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        }

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

        //RefreshNeededItems();

    }
    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            foodsScreenUI.SetActive(false);
            foodsScreenUI1.SetActive(false);
            foodsScreenUI2.SetActive(false);
            foodsScreenUI3.SetActive(false);
            foodsScreenUI4.SetActive(false);
            foodsScreenUI5.SetActive(false);
            constructionScreenUI.SetActive(false);
            constructionScreenUI1.SetActive(false);
            refineScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        int apple_count = 0;
        int pear_count = 0;

        int carrot_count = 0;
        int yogurt_count = 0;

        int mushroom_count = 0;
        int onion_count = 0;

        int broccoli_count = 0;
        int meat_count = 0;

        int avocado_count = 0;
        int bread_count = 0;

        int lemon_count = 0;
        int cucumber_count = 0;

        int plank_count = 0;

        int log_count = 0;

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
                case "Pear":
                    pear_count += 1;
                    break;
                case "Carrot":
                    carrot_count += 1;
                    break;
                case "Yogurt":
                    yogurt_count += 1;
                    break;
                case "Mushroom":
                    mushroom_count += 1;
                    break;
                case "Onion":
                    onion_count += 1;
                    break;
                case "Broccoli":
                    broccoli_count += 1;
                    break;
                case "Meat":
                    meat_count += 1;
                    break;
                case "Avocado":
                    avocado_count += 1;
                    break;
                case "Bread":
                    bread_count += 1;
                    break;
                case "Cucumber":
                    cucumber_count += 1;
                    break;
                case "Log":
                    log_count += 1;
                    break;
                case "Plank":
                    plank_count += 1;
                    break;
                default:
                    break;
            }
        }

        //AXE
        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        if(stone_count >= 3 && stick_count >=3 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }

        //Fruit Salad
        FruitSaladReq1.text = "2 Apple [" + apple_count + "]";
        FruitSaladReq2.text = "1 Pear [" + pear_count + "]";

        if (apple_count >= 2 && pear_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftFruitSaladBTN.gameObject.SetActive(true);
        }
        else
        {
            craftFruitSaladBTN.gameObject.SetActive(false);
        }

        //Carrot Milkshake
        CarrotMilkshakeReq1.text = "2 Carrot [" + carrot_count + "]";
        CarrotMilkshakeReq2.text = "1 Yogurt [" + yogurt_count + "]";

        if (carrot_count >= 2 && yogurt_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftCarrotMilkshakeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftCarrotMilkshakeBTN.gameObject.SetActive(false);
        }

        //Mushroom Onion
        MushroomOnionReq1.text = "2 Mushroom [" + mushroom_count + "]";
        MushroomOnionReq2.text = "1 Onion [" + onion_count + "]";

        if (mushroom_count >= 2 && onion_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftMushroomOnionBTN.gameObject.SetActive(true);
        }
        else
        {
            craftMushroomOnionBTN.gameObject.SetActive(false);
        }

        //Avocado Toast
        AvocadoToastReq1.text = "1 Avocado [" + avocado_count + "]";
        AvocadoToastReq2.text = "1 Bread [" + bread_count + "]";

        if (avocado_count >= 1 && bread_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftAvocadoToastBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAvocadoToastBTN.gameObject.SetActive(false);
        }

        //Broccoli Meat
        BroccoliMeatReq1.text = "1 Broccoli [" + broccoli_count + "]";
        BroccoliMeatReq2.text = "1 Meat [" + meat_count + "]";

        if (broccoli_count >= 1 && meat_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftBroccoliMeatBTN.gameObject.SetActive(true);
        }
        else
        {
            craftBroccoliMeatBTN.gameObject.SetActive(false);
        }

        //Lemon Cucumber
        LemonCucumberReq1.text = "1 Lemon [" + lemon_count + "]";
        LemonCucumberReq2.text = "1 Cucumber [" + cucumber_count + "]";

        if (lemon_count >= 1 && cucumber_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftLemonCucumberBTN.gameObject.SetActive(true);
        }
        else
        {
            craftLemonCucumberBTN.gameObject.SetActive(false);
        }

        //Plank
        PlankReq1.text = "1 Log [" + log_count + "]";

        if (log_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(2))
        {
            craftPlankBTN.gameObject.SetActive(true);
        }
        else
        {
            craftPlankBTN.gameObject.SetActive(false);
        }

        //Foundation
        FoundationReq1.text = "4 Plank [" + plank_count + "]";

        if (plank_count >= 4 && InventorySystem.Instance.CheckSlotsAvailable(2))
        {
            craftFoundationBTN.gameObject.SetActive(true);
        }
        else
        {
            craftFoundationBTN.gameObject.SetActive(false);
        }

        //Wall
        WallReq1.text = "1 Plank [" + plank_count + "]";

        if (plank_count >= 1 && InventorySystem.Instance.CheckSlotsAvailable(2))
        {
            craftWallBTN.gameObject.SetActive(true);
        }
        else
        {
            craftWallBTN.gameObject.SetActive(false);
        }
    }

}
