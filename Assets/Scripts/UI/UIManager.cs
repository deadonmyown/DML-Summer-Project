using System.Collections;
using Player;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [SerializeField] private GameObject textBox;
        [SerializeField] private TMP_Text text;
        [SerializeField] private int defaultCharPerSec;

        private RectTransform textBoxPosition;
        private IEnumerator _textTyping;
        private bool _isTyping;

        private bool _isInteracting;
        
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
            textBoxPosition = textBox.GetComponent<RectTransform>();
            textBox.SetActive(false);
            _isInteracting = false;
        }

        public void TryInteract(string dialogueText, bool useTextTyping, Vector3 position, int charPerSec = 1)
        {
            if (!_isInteracting)
            {
                BeginInteraction(dialogueText, useTextTyping, position, charPerSec);
            }
            else
            {
                EndInteraction(useTextTyping);
            }
        }
        
        
        public void BeginInteraction(string dialogueText, bool useTextTyping, Vector3 position, int charPerSec = 1)
        {
            _isInteracting = true;
            TurnOnUI(dialogueText, useTextTyping, position, charPerSec);
        }

        public void BeginInteraction(Collider other, string dialogueText, bool useTextTyping, Vector3 position, int charPerSec = 1)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                var player = other.GetComponentInParent<Player.Player>();
                if (player == PlayerManager.Instance.CurrentPlayer)
                {
                    TurnOnUI(dialogueText, useTextTyping, position, charPerSec);
                }
            }
        }

        public void EndInteraction(bool useTextTyping)
        {
            TurnOffUI(useTextTyping);
            _isInteracting = false;
        }
        
        public void EndInteraction(Collider other, bool useTextTyping)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                var player = other.GetComponentInParent<Player.Player>();
                if (player == PlayerManager.Instance.CurrentPlayer)
                {
                    TurnOffUI(useTextTyping);
                }
            }
        }
        
        public void TriggerCheckCanvas(Collider other, bool active)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                var player = other.GetComponentInParent<Player.Player>();
                if (player == PlayerManager.Instance.CurrentPlayer)
                {
                    textBox.SetActive(active);
                }
            }
        }
        
        private IEnumerator TextTyping(string dialogueText, int charPerSec)
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

        private void TurnOnUI(string dialogueText, bool useTextTyping, Vector3 position, int charPerSec = 1)
        {
            textBox.SetActive(true);
            textBoxPosition.position = position;
            if (useTextTyping)
            {
                _textTyping = TextTyping(dialogueText, charPerSec);
                StartCoroutine(_textTyping);
            }
            else
            {
                text.text = dialogueText;
            }
        }

        private void TurnOffUI(bool useTextTyping)
        {
            if (useTextTyping)
            {
                StopCoroutine(_textTyping);
            }
            textBox.SetActive(false);
        }
    }
}