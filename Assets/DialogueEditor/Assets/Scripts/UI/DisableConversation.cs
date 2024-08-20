using UnityEngine;

// This script makes reference to the NPCConversationHandler script. When the NPC dialogue panel is closed, it will re-enable movement and disable the cursor.
public class DisableConversation : MonoBehaviour
{
    public NPCConversationHandler _currentConversation;

    private void OnDisable()
    {
        // Ensure _nPCConversationHandler is not null before calling the method
        if (_currentConversation != null)
        {
            _currentConversation.EnableMovementControls();
        }
        else
        {
            Debug.LogWarning("_nPCConversationHandler is not assigned.");
        }
    }
}
