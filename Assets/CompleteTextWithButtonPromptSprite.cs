using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public static class CompleteTextWithButtonPromptSprite {

    public static string ReadAndReplaceBinding (string textToDisplay, InputBinding actionNeeded, TMP_SpriteAsset spriteAsset) {
        string stringButtonName = actionNeeded.ToString();
        stringButtonName = RenameInput(stringButtonName);

        textToDisplay = textToDisplay.Replace("BUTTONPROMPT", $"<sprite=\"{spriteAsset.name}\" name=\"{stringButtonName}\">");
        
        return textToDisplay;
    }


    private static string RenameInput (string stringButtonName) { 
        stringButtonName = stringButtonName.Replace("Interact:", string.Empty);

        stringButtonName = stringButtonName.Replace("<Keyboard>/", "Keyboard_");

        stringButtonName = stringButtonName.Replace("<Gamepad>/", "Gamepad_");

        return stringButtonName;
    }
}
