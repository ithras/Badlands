using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text nameInputField = null;
    [SerializeField] private Button confirmButton = null;

    public static string DisplayName { get; private set; }

    private const string PlayerPrefsNameKey = "PlayerName";

    void Start() => SetupInputField();

    private void SetupInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
            return;

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

        nameInputField.text = defaultName;
    }

    public void SetPlayerName(string name)
    {
        confirmButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
    }
}
