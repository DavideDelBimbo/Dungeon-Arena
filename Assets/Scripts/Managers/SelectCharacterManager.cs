using UnityEngine;

public class SelectCharacterManager : Singleton<SelectCharacterManager> {
    [Header("Character Selection Settings")]
    [SerializeField] private Player[] _playerCharactersList;

    public int _selectedPlayerCharacterIndex = -1;


    public Player[] PlayerCharactersList => _playerCharactersList;

    public Player SelectedPlayerCharacter {
        get => _selectedPlayerCharacterIndex != -1 ? _playerCharactersList[_selectedPlayerCharacterIndex] : null;
        set => _selectedPlayerCharacterIndex = value != null ? System.Array.IndexOf(_playerCharactersList, value) : -1;
    }
}
