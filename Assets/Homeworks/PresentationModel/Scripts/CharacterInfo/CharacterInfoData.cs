using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Data/New Character")]
public class CharacterInfoData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    public Sprite Icon => _icon;
    public string Description => _description;
}