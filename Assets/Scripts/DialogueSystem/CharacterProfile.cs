using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Data/Characters", order = 0)]
    public class CharacterProfile : ScriptableObject
    {
        public Sprite characterSprite;
        public string characterName;
    }
}