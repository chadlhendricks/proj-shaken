using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using UnityEngine.InputSystem;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using NavKeypad;

public class PlayerInteractions : MonoBehaviour
{
    private ExamineSystem _examineSystem;
    private PlayerInventory _playerInventory;
    private pickUp _pickUp;
    private CursorIcon _cursorIcon;
    private float _time;
    private float _timer;

    public float ExamineDistance;
    public float InteractionDistance;
    public GameObject Examine_Object;

    // NPC Interactions
    public GameObject currentNPC;
    [SerializeField] private GameObject _panelDialogue;
    private DisableConversation _disableConversation;
    private NPCConversationHandler _nPCConversationHandler;
    private NPCConversation _myConversation;

    // Debug Raycast
    [SerializeField] private LayerMask _aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform _debugTransform;

    // Gun
    [SerializeField] private Transform _gunParticleEffect;
    [SerializeField] private Transform _bloodParticleEffect;

    public void Start()
    {
        _time = 0.5f;
        _pickUp = FindFirstObjectByType<pickUp>();
        _examineSystem = FindFirstObjectByType<ExamineSystem>();
        _playerInventory = FindFirstObjectByType<PlayerInventory>();
        _cursorIcon = FindFirstObjectByType<CursorIcon>();
        currentNPC = null;

        _disableConversation = _panelDialogue.GetComponent<DisableConversation>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray RayOrigin = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        RaycastHit hit;

        if (Physics.Raycast(RayOrigin, out hit, 999f, _aimColliderLayerMask) ) 
        {
            _debugTransform.position = hit.point;
            mouseWorldPosition = hit.point;
            hitTransform = hit.transform;
        }

        #region Examine System
        if (Input.GetKeyDown(KeyCode.E) && !ExamineSystem.ExamineMode && !PlayerInventory.InventoryIsOn)
        {
            if (Physics.Raycast(RayOrigin, out hit, ExamineDistance))
            {
                // Examining an object.
                if (hit.collider.CompareTag("Examine"))
                {
                    Examine_Object = hit.collider.gameObject;
                    _examineSystem.ExamineAction(Examine_Object);
                    _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.white, 5);

                }
                // Object that can be picked up - Different Icon
                else if (hit.collider.CompareTag("pickUp")) 
                {
                    Examine_Object = hit.collider.gameObject;
                    _pickUp.pickUpSystem(hit.collider.gameObject);
                    _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);

                }
                // Keypad Interact
                else if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    keypadButton.PressButton();
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
                _examineSystem.ExamineActionPickUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && ExamineSystem.ExamineMode) 
        {
            _examineSystem.ResetTransform();
        }
        #endregion

        _timer += Time.deltaTime;
        if (_timer >= _time)
        {
            if (Input.GetKey(KeyCode.I) && !ExamineSystem.ExamineMode && !TooltipTrigger.EximaneFromInventory)
            {
                _playerInventory.InvetoryAction();
                _timer = 0;
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

        // Shooting
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hitTransform != null)
            {
                if (hitTransform.GetComponent<NPCInteractable>() != null)
                {
                    // Debug.Log(hitTransform);
                    Instantiate(_bloodParticleEffect, _debugTransform.position, UnityEngine.Quaternion.identity);
                }
                else
                {
                    // Debug.Log(hitTransform);
                    Instantiate(_gunParticleEffect, _debugTransform.position, UnityEngine.Quaternion.identity);
                }
            }
        }
    }
}
