using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TextFileReader : MonoBehaviour
{
    void Start()
    {
        ReadString();
    }

    void ReadString()
    {
        //string path = "Assets/TextFiles/colorcodes.txt";
        string path = Path.Combine(Directory.GetCurrentDirectory(), "colorcodes.txt");
        StreamReader reader = new StreamReader(path); 
        string fileMsg = reader.ReadToEnd();
        fileMsg = fileMsg.Replace("\n", "").Replace("\r", "");
        reader.Close();

        CreateFromFile(fileMsg);
    }

    //instanziert jedes object aus der textdatei
    void CreateFromFile(string msg){
        string[] sMsg = msg.Split(',');
        int length = sMsg.Length/6;

        for(int a=0;a<length;a++){
            ColorController.CreateColorObj(sMsg[(a*6)],Int32.Parse(sMsg[(a*6)+1]),sMsg[(a*6)+2],sMsg[(a*6)+3],sMsg[(a*6)+4],Int32.Parse(sMsg[(a*6)+5]));
            //Debug.Log(sMsg[(a*6)+5]);
        }
    }
}
