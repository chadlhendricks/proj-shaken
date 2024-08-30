using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;
    public GameObject itemToInspect;
    public GameObject tempItemToInspcet;
    public ExamineSystem examineSystem;
    public PlayerInteractions interaction;

    public static bool EximaneFromInventory = false;
    public bool isThatOn = false;

    public void Start()
    {
        examineSystem = FindFirstObjectByType<ExamineSystem>();
        interaction = FindFirstObjectByType<PlayerInteractions>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isThatOn = true;
        tempItemToInspcet = itemToInspect;
        StartCoroutine(ShowDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isThatOn = false;
        tempItemToInspcet = null;
        TooltipSystem.Hide();
    }

    IEnumerator ShowDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        TooltipSystem.Show(content, header);
    }

    public void Update()
    {
        if (isThatOn)
            if (Input.GetKey(KeyCode.E))
            {
                isThatOn = false;
                EximaneFromInventory = true;
                examineSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ExamineSystem>();
                interaction = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerInteractions>();
                interaction.Examine_Object = itemToInspect;
                examineSystem.ItemInspect(tempItemToInspcet);
                tempItemToInspcet = null;
            }
    }
}
