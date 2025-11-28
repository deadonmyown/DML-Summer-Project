using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class UITrigger : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private string text;
        [SerializeField] private int charPerSec = 4;
        [SerializeField] private bool useTextTyping;

        private void OnTriggerEnter(Collider other)
        {
            UIManager.Instance.BeginInteraction(other, text, useTextTyping, transform.position + offset, charPerSec);
        }

        private void OnTriggerExit(Collider other)
        {
            UIManager.Instance.EndInteraction(other, useTextTyping);
        }
    }
}
