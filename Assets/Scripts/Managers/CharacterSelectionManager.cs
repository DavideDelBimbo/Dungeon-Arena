using System;
using UnityEngine;

public class CharacterSelectionManager : Singleton<CharacterSelectionManager> {
    [Header("Settings")]
    [SerializeField] private GameObject[] _characterList;

    private int currentCharacterIndex = -1;


    public GameObject[] CharacterList => _characterList;
    public int CurrentCharacterIndex => currentCharacterIndex;


    public void SelectCharacter(GameObject character) {
        currentCharacterIndex = Array.IndexOf(_characterList, character);
    }
}