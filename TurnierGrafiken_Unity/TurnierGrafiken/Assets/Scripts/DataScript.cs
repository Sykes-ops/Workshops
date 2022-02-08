using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEditor;
using System.IO;

using SmartDLL;

// using SimpleFileBrowser;

public class DataScript : MonoBehaviour
{
    [Header("References")]
    public TMPro.TextMeshPro[] playerTexts;
    public TMPro.TextMeshPro[] sqr_playerTexts;
    public TMPro.TextMeshPro dayText, dateTimeText;
    public TMPro.TextMeshPro sqr_dayText, sqr_dateTimeText;

    [Header("Inspector Set")]
    public string[] setPlayerNames;
    public string setDay, setDate, setTime;

    private string[] playerNames = new string[10];
    private string day, date, time;
    public string rmbFileName = "output";

    private SmartFileExplorer sfe = new SmartFileExplorer();

    public string fileFolderPath;
    

    void Awake(){
        var pathFolder = Directory.CreateDirectory("Files");
        // Debug.Log(pathFolder.FullName);
        fileFolderPath = pathFolder.FullName;
        day = (setDay == "none") ? "": setDay;
        date = (setDate == "none") ? "": setDate;
        time = (setTime == "none") ? "": setTime;

        for(int iy = 0; iy < 10; iy++){
            if(setPlayerNames[iy] == null) continue;
            playerNames[iy] = (setPlayerNames[iy] == "none") ? "" : setPlayerNames[iy];
        }
        
        SetTexts();
    }

    void SetTexts(){
        day = day.ToUpper();
        dayText.text = day;
        sqr_dayText.text = day;
        dateTimeText.text = date + " - " + time;
        sqr_dateTimeText.text = date + "\n" + time;

        for(int i = 0; i < 10; i++){
            if(playerNames[i] == null || playerNames[i] == "none") continue;
            playerTexts[i].text = playerNames[i];
            sqr_playerTexts[i].text =  playerNames[i];
        }

    }

    public void GenerateTemplate(){
        // string path = EditorUtility.SaveFilePanel("Generate Template Where?", "", "template.txt", "txt");
        // FileBrowser.ShowSaveDialog(null, null, FileBrowser.PickMode.Files, false, Application.dataPath, "template.txt", null, null);

        // if(path == null)
        //     return;
        // File f = new File(path);

        StreamWriter sw = new StreamWriter(fileFolderPath + "\\template.txt", false);
        sw.WriteLine("day: FREITAG");
        sw.WriteLine("date: 06.08.21");
        sw.WriteLine("time: 17:00");
        sw.WriteLine(" ");
        sw.WriteLine("#TEAM LINKS (sortiert top, jngl, mid, adc, supp)");
        sw.WriteLine("name Top");
        sw.WriteLine("name Jungle");
        sw.WriteLine("name Mid");
        sw.WriteLine("name Bot");
        sw.WriteLine("name Supp");
        sw.WriteLine(" ");
        sw.WriteLine("#TEAM RECHTS (sortiert top, jngl, mid, adc, supp)");
        sw.WriteLine("name Top");
        sw.WriteLine("name Jungle");
        sw.WriteLine("name Mid");
        sw.WriteLine("name Bot");
        sw.WriteLine("name Supp");
        sw.Close();
    }

    public void ReadFromFile(){

        sfe.OpenExplorer(fileFolderPath, true, "Open a Textfile", "txt", "txt files (*.txt)|*.txt");
        string path = sfe.fileName;
        if(path == null) return;
        rmbFileName = path.Split('\\')[path.Split('\\').Length-1];
        rmbFileName = rmbFileName.Split('.')[0];
        Debug.Log(rmbFileName);

        StreamReader sr = new StreamReader(path);
        day = sr.ReadLine().Split(':')[1].Trim();
        date = sr.ReadLine().Split(':')[1].Trim();
        string line = sr.ReadLine();
        time = line.Split(':')[1].Trim() + ":" + line.Split(':')[2].Trim();
        sr.ReadLine();
        sr.ReadLine();
        for(int i = 0; i < 10; i++){
            if(i == 5){
                sr.ReadLine();
                sr.ReadLine();
            }
            playerNames[i] = sr.ReadLine();
        }
        sr.Close();

        SetTexts();
    }

}

