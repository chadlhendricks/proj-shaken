using DialogueEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// The purpose of this script is for handling starting quests via the NPC dialogue. It also handles player input and cursor lock states during dialogue.
public class NPCConversationHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    // Enable player movement and lock the cursor
    public void EnableMovementControls()
    {
        Debug.Log("Enabling Movement Controls");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput.enabled = true;
    }

    // Disable player movement and unlock the cursor
    public void DisableMovementControls()
    {
        Debug.Log("Disabling Movement Controls");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerInput.enabled = false;
    }

    public void StartQuest()
    {
        Debug.Log("Starting Quest");
    }

}
