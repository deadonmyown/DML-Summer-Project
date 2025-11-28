using System.Collections.Generic;
using Environment.Interactable.Abstraction;
using Player;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHandler : MonoBehaviour, IInteractable
    {
        [SerializeField] private Dialogue dialogues;
        
        public bool Interact(Interactor interactor)
        {
            if (!DialogueManager.Instance.InDialogue)
            {
                DialogueManager.Instance.DialogueStart(dialogues);
                //interactor.UpdateCachedInteractable(this);
                
            }
            //DialogueManager.Instance.DialogueProcess();

            /*if (!DialogueManager.Instance.InDialogue)
            {
                interactor.UpdateCachedInteractable(null);
            }*/

            return true;
        }

        public void DialogueProcess()
        {
            if (!DialogueManager.Instance.InDialogue)
            {
                DialogueManager.Instance.DialogueStart(dialogues);
            }
            //DialogueManager.Instance.DialogueProcess();
        } 
    }
}