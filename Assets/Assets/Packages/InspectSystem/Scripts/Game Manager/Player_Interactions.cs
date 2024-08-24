using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using UnityEngine.InputSystem;

public class Player_Interactions : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private ExamineSystem examineSystem;
    private PlayerInventory playerInventory;
    private pickUp _pickUp;
    private CursorIcon _cursorIcon;
    private float time;
    private float timer;

    public float ExamineDistance;
    public float InteractionDistance;
    public GameObject Examine_Object;

    // NPC Interactions
    public GameObject currentNPC;

    [SerializeField] private GameObject _panelDialogue;
    private DisableConversation _disableConversation;

    private NPCConversationHandler _nPCConversationHandler;
    private NPCConversation _myConversation;

    public void Start()
    {
        time = 0.5f;
        _pickUp = FindObjectOfType<pickUp>();
        examineSystem = FindObjectOfType<ExamineSystem>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _cursorIcon = FindObjectOfType<CursorIcon>();
        currentNPC = null;

        _disableConversation = _panelDialogue.GetComponent<DisableConversation>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray RayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        #region Examine System
        if (Input.GetKeyDown(KeyCode.E) && !ExamineSystem.ExamineMode && !PlayerInventory.InventoryIsOn)
        {
            if (Physics.Raycast(RayOrigin, out hit, ExamineDistance))
            {
                // Examining an object.
                if (hit.collider.CompareTag("Examine"))
                {
                    Examine_Object = hit.collider.gameObject;
                    examineSystem.ExamineAction(Examine_Object);
                    _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.white, 5);

                }
                // Object that can be picked up - Different Icon
                else if (hit.collider.CompareTag("pickUp")) 
                {
                    Examine_Object = hit.collider.gameObject;
                    _pickUp.pickUpSystem(hit.collider.gameObject);
                    _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);

                }
                // Interact with an NPC
                else if (hit.collider.CompareTag("NPC"))
                {
                    currentNPC = hit.collider.gameObject;

                    if (currentNPC != null)
                    {
                        // Get Handler Component
                        _nPCConversationHandler = currentNPC.GetComponentInChildren<NPCConversationHandler>();
                        _nPCConversationHandler.DisableMovementControls();

                        // Sets Current Conversation Script in the Disable Conversation script attached to the Dialogue Panel.
                        _disableConversation._currentConversation = _nPCConversationHandler;

                        // Get Dialogue
                        _myConversation = currentNPC.GetComponentInChildren<NPCConversation>();
                        Debug.Log(_myConversation);

                        // Start Dialogue
                        ConversationManager.Instance.StartConversation(_myConversation);                    }
                    else
                    {
                        Debug.Log("No current NPC");
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && Examine_Object.GetComponent<InspectSystem>().Pickup && ExamineSystem.ExamineMode)
        {
            if (!Examine_Object.GetComponent<InspectSystem>().has_pickup)
            {
                examineSystem.ExamineActionPickUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && ExamineSystem.ExamineMode) 
        {
            examineSystem.ResetTransform();
        }
        #endregion

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
            if (Physics.Raycast(RayOrigin, out hit, InteractionDistance))
            {
                if (hit.collider.CompareTag("Interaction"))
                {
                    if (hit.collider.GetComponent<AnimationReaction>() != null)
                    {
                        hit.collider.GetComponent<AnimationReaction>().StartAnimationReaction();
                    }
                    if (hit.collider.GetComponent<ChangeSceneReaction>() != null)
                    {
                        hit.collider.GetComponent<ChangeSceneReaction>().StartChangeScene();
                    }
                }
            }
        }
    }
}