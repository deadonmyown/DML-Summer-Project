using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        public bool InDialogue { get; private set; }

        public GameObject dialogueBox;
        public Image characterSprite;
        public TMP_Text characterName;
        public TMP_Text text;

        public int charPerSec = 4;

        private bool _isTyping;

        private Dialogue.DialogueInfo _currDialogue;
        private IEnumerator _currCoroutine;

        private Queue<Dialogue.DialogueInfo> _dialogues;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _dialogues = new Queue<Dialogue.DialogueInfo>();
            dialogueBox.SetActive(false);
        }

        public void DialogueStart(Dialogue dialogue)
        {
            PlayerManager.Instance.SwitchPlayerInputMap("Dialogue");
            
            dialogueBox.SetActive(true);
            InDialogue = true;

            foreach (var info in dialogue.dialogues)
            {
                _dialogues.Enqueue(info);
            }
            
            PlayerInputHandler.OnDialogue += DialogueProcess;
            DialogueProcess();
        }

        public void DialogueProcess()
        {
            if (_isTyping)
            {
                StopCoroutine(_currCoroutine);
                text.maxVisibleCharacters = _currDialogue.text.Length;
                _isTyping = false;
                return;
            }
            
            if (CheckDialogueEnd())
            {
                Debug.Log("Dialogue end");
                return;
            }

            /*if (InDialogue == false)
            {
                dialogueBox.SetActive(true);
                InDialogue = true;
            }*/
            
            _currDialogue = _dialogues.Dequeue();
            characterSprite.sprite = _currDialogue.characterProfile.characterSprite;
            characterName.text = _currDialogue.characterProfile.characterName;
            _currCoroutine = TextTyping(_currDialogue.text);
            StartCoroutine(_currCoroutine);
        }

        private void DialogueEnd()
        {
            InDialogue = false;
            dialogueBox.SetActive(false);
            
            PlayerInputHandler.OnDialogue -= DialogueProcess;
            PlayerManager.Instance.SwitchPlayerInputMap("Main");
        }

        public bool CheckDialogueEnd()
        {
            if (_dialogues.Count == 0)
            {
                DialogueEnd();
                return true;
            }

            return false;
        }

        private IEnumerator TextTyping(string dialogueText)
        {
            _isTyping = true;
            int len = dialogueText.Length;
            text.maxVisibleCharacters = 0;
            text.text = dialogueText;
            for (int i = 0; i < len; i++)
            {
                text.maxVisibleCharacters++;
                yield return new WaitForSeconds((float)1 / charPerSec);
            }

            _isTyping = false;
        }

        private void OnDestroy()
        {
            PlayerInputHandler.OnDialogue -= DialogueProcess;
        }
    }
}
