using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }
    public bool OnTarget;

    public GameObject selectedObject;

    public GameObject interaction_Info_UI;
    Text interaction_text;

    private void Start()
    {
        OnTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance= this;
        }
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();


            if (interactable && interactable.playerInRange)
            {
                OnTarget= true;
                selectedObject = interactable.gameObject;

                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                OnTarget = false;
                interaction_Info_UI.SetActive(false);
            }

        }
        else
        {
            OnTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }

}
