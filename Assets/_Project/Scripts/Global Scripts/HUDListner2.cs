using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class HUDListner2 : MonoBehaviour
{
    public NPCConversation levelZeroTutorialConversation;
    public bool IsEndNode;
    public GameObject CloseBtn;
    public GameObject ConversationPanel;
    // Start is called before the first frame update
    void Start()
    {
        ConversationManager.Instance.StartConversation(levelZeroTutorialConversation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTutorialDialogueOptions(bool _isActive)
    {
        ConversationManager.Instance.OptionsPanel.gameObject.SetActive(_isActive);
        IsEndNode = _isActive;
    }
    public void HideConversationPanel()
    {
        ConversationPanel.SetActive(false);
        CloseBtn.SetActive(false);
    }
}
