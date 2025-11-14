using UnityEngine;
using UnityEngine.Serialization;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Data/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [System.Serializable]
        public class DialogueInfo
        {
            public CharacterProfile characterProfile;
            public string text;
        }

        public DialogueInfo[] dialogues;
    }
}