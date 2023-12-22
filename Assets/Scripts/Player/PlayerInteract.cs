using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Script References
    [SerializeField] private CellphoneFlashlight _cellphoneFlashlight;
    [SerializeField] private ExamineSystem examineSystem;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private CursorIcon _cursorIcon;
    private pickUp _pickUp;

    // Variables
    private float time;
    private float timer;
    public float ExamineDistance;
    public float InteractionDistance;
    public GameObject ExamineObject;

    public void Start()
    {
        time = 0.5f;
        _pickUp = FindObjectOfType<pickUp>();
    }

    private void Update()
    {
        InspectItem();
        ToggleFlashlight();
    }

    private void InspectItem()
    {
        // 
        if (Input.GetKeyDown(KeyCode.E) && !ExamineSystem.ExamineMode && !PlayerInventory.InventoryIsOn)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, ExamineDistance);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact(transform);
                }

                if (collider.TryGetComponent(out InspectSystem inspectSystem))
                {
                    if (inspectSystem.CompareTag("Examine"))
                    {
                        ExamineObject = inspectSystem.gameObject;
                        examineSystem.ExamineAction(ExamineObject);
                        _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.white, 5);

                    }
                    else if (inspectSystem.CompareTag("pickUp"))
                    {
                        ExamineObject = inspectSystem.gameObject;
                        _pickUp.pickUpSystem(inspectSystem.gameObject);
                        _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && ExamineObject.GetComponent<InspectSystem>().Pickup && ExamineSystem.ExamineMode)
        {
            if (!ExamineObject.GetComponent<InspectSystem>().has_pickup)
            {
                examineSystem.ExamineActionPickUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && ExamineSystem.ExamineMode)
        {
            examineSystem.ResetTransform();
        }

        timer += Time.deltaTime;
        if (timer >= time)
        {
            if (Input.GetKey(KeyCode.I) && !ExamineSystem.ExamineMode && !TooltipTrigger.EximaneFromInventory)
            {
                playerInventory.InvetoryAction();
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !PlayerInventory.InventoryIsOn && !ExamineSystem.ExamineMode)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, ExamineDistance);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out AnimationReaction animationReaction))
                {
                    Debug.Log(animationReaction);
                }

            }
        }
    }

    private void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _cellphoneFlashlight.ToggleFlashlight();
        }
    }
}
