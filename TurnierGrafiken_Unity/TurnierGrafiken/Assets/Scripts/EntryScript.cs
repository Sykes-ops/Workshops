using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryScript : MonoBehaviour
{
    [Header("Data")]
    public string teamName;
    public int wins;
    public int loses;

    [Header("References")]
    public TMPro.TextMeshPro teamName_text;
    public TMPro.TextMeshPro win_lose_text;
    public TMPro.TextMeshPro orbNumber;

    void Awake(){
        teamName_text.text = teamName;
        win_lose_text.text = wins + " W\n"+ loses + " L";
        orbNumber.text = "0";
    }

}
