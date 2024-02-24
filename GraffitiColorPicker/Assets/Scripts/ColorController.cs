using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    public static List<CollorObject> colorObjList = new List<CollorObject>();
    public static List<CollorObject> colorObjFilteredList = new List<CollorObject>();
    public static List<GameObject> prefabObjectList = new List<GameObject>();

    public static Toggle toggleMontanaBlack;
    public static Toggle toggleMontanaGold;
    public static Toggle toggleLoop;
    public static Toggle toggleMTNHardcore;
    public static Toggle toggleMTN94;
    public static Toggle toggleMolotow;
    public static Toggle toggleFlameBlue;
    public static Toggle toggleFlameOrange;

    public static Button saveBtn;

    private static GameObject prefab;

    void Awake() 
    {
        prefab = (GameObject)Resources.Load("colorTile", typeof(GameObject));

        toggleMontanaBlack = GameObject.Find("MontanaBlack").GetComponent<Toggle>();
        toggleMontanaGold = GameObject.Find("MontanaGold").GetComponent<Toggle>();
        toggleLoop = GameObject.Find("Loop").GetComponent<Toggle>();
        toggleMTNHardcore = GameObject.Find("MTNHardcore").GetComponent<Toggle>();
        toggleMTN94 = GameObject.Find("MTN94").GetComponent<Toggle>();
        toggleMolotow = GameObject.Find("Molotow").GetComponent<Toggle>();
        toggleFlameBlue = GameObject.Find("FlameBlue").GetComponent<Toggle>();
        toggleFlameOrange = GameObject.Find("FlameOrange").GetComponent<Toggle>();
        saveBtn = GameObject.Find("Button-SaveValues").GetComponent<Button>();
    }

    //constructor for Object
    public static void CreateColorObj(string type, int capacity,string hex, string name, string altName, int num){
        int[] rgb = getHexToRGB(hex);
        float[] lab = Lab(rgb);

        colorObjList.Add(new CollorObject(type, capacity, hex, name, altName, num, rgb, lab));
        colorObjFilteredList.Add(new CollorObject(type, capacity, hex, name, altName, num, rgb, lab));
        
        //CreateObj();
    }

    public void printList(){
        for(int a=0;a<colorObjFilteredList.Count;a++){
            colorObjFilteredList[a].Info();
        }
    }

    //generiert das letzte listenitem von "colorObjFilteredList" und speichert in "gol"
    public static void CreateObj(){
        prefabObjectList.Add(Instantiate(prefab, new Vector3(((prefabObjectList.Count-1)*0.325f)-12, 4, 0), Quaternion.identity));

        List<int> rgb = colorObjFilteredList[prefabObjectList.Count-1].getRGB();
        prefabObjectList[prefabObjectList.Count-1].GetComponent<SpriteRenderer>().color = new Color((float)rgb[0]/(float)255,(float)rgb[1]/(float)255,(float)rgb[2]/(float)255,1);
    }

    //delta-e for color comparison
    //sqrt((l2-l1)^2+(a2-a1)^2+(b2-b1)^2));
    public static List<CollorObject> findNearestColor(string hex){
        //find color for this hex value
        int[] rgbMain = getHexToRGB(hex);
        float[] labMain = Lab(rgbMain);

        double distanceValue = 1000;
        List<CollorObject> nearestColorList = new List<CollorObject>();
        CollorObject nearestColorObject0 = colorObjFilteredList[0];
        CollorObject nearestColorObject1 = colorObjFilteredList[0];
        CollorObject nearestColorObject2 = colorObjFilteredList[0];
        CollorObject nearestColorObject3 = colorObjFilteredList[0];

        for(int a=0;a<colorObjFilteredList.Count;a++){
            double value = Math.Sqrt(Math.Pow((colorObjFilteredList[a].getLab()[0]-labMain[0]),2)+Math.Pow((colorObjFilteredList[a].getLab()[1]-labMain[1]),2)+Math.Pow((colorObjFilteredList[a].getLab()[2]-labMain[2]),2));

            if(value<distanceValue){
                nearestColorObject0 = colorObjFilteredList[a];
                distanceValue = value; 
            }
        }
        nearestColorList.Add(nearestColorObject0);

        Debug.Log("val: "+distanceValue+" obj: ");
        nearestColorObject0.Info();



        //finde die nächste color aber ohne die mainfarbe in der colorObjFilteredList liste



        nearestColorList.Add(nearestColorObject1);
        nearestColorList.Add(nearestColorObject2);
        nearestColorList.Add(nearestColorObject3);

        return nearestColorList;
    }



    public static List<CollorObject> findNearestColorTest(string hex){
        //find color for this hex value
        int[] rgbMain = getHexToRGB(hex);
        float[] labMain = Lab(rgbMain);

        int nearestColors = 5;
        List<CollorObject> nearestColorList = new List<CollorObject>();
        CollorObject nearestColorObject0 = colorObjFilteredList[0];
        
        Debug.Log("colorObjFilteredList size davor: "+colorObjFilteredList.Count+" nearestColorList size:"+nearestColorList.Count);

        for(int a=0;a<nearestColors;a++){

            double distanceValue = 1000;
            int iterationToRemoveAt = 0;

            for(int b=0;b<colorObjFilteredList.Count;b++){

                double value = Math.Sqrt(Math.Pow((colorObjFilteredList[b].getLab()[0]-labMain[0]),2)+Math.Pow((colorObjFilteredList[b].getLab()[1]-labMain[1]),2)+Math.Pow((colorObjFilteredList[b].getLab()[2]-labMain[2]),2));

                if(value<distanceValue){
                    nearestColorObject0 = colorObjFilteredList[b];
                    distanceValue = value;
                    iterationToRemoveAt = b;
                }
            }
            nearestColorList.Add(nearestColorObject0);
            colorObjFilteredList.RemoveAt(iterationToRemoveAt);
        }

        Debug.Log("colorObjFilteredList size nach remove: "+colorObjFilteredList.Count+" nearestColorList size:"+nearestColorList.Count);

        for(int c=0;c<nearestColorList.Count;c++){
            colorObjFilteredList.Add(nearestColorList[c]);
        }

        Debug.Log("colorObjFilteredList size nach add: "+colorObjFilteredList.Count+" nearestColorList size:"+nearestColorList.Count);


        GenerateColorObjFilteredList(true);

        return nearestColorList;
    }





    //hex to xyz to lab color
    public static float[] Lab(int[] rgbO){
        float[] xyz = new float[3];
        float[] lab = new float[3];
        float[] rgb = new float[] {rgbO[0],rgbO[1],rgbO[2]};
        rgb[0] = rgbO[0] / 255.0f;
        rgb[1] = rgbO[1] / 255.0f;
        rgb[2] = rgbO[2] / 255.0f;
        if (rgb[0] > .04045f){
            rgb[0] = (float)Math.Pow((rgb[0] + .055) / 1.055, 2.4);
        }
        else{
            rgb[0] = rgb[0] / 12.92f;
        }
        if (rgb[1] > .04045f){
            rgb[1] = (float)Math.Pow((rgb[1] + .055) / 1.055, 2.4);
        }
        else{
            rgb[1] = rgb[1] / 12.92f;
        }
        if (rgb[2] > .04045f){
            rgb[2] = (float)Math.Pow((rgb[2] + .055) / 1.055, 2.4);
        }
        else{
            rgb[2] = rgb[2] / 12.92f;
        }
        rgb[0] = rgb[0] * 100.0f;
        rgb[1] = rgb[1] * 100.0f;
        rgb[2] = rgb[2] * 100.0f;
        xyz[0] = ((rgb[0] * .412453f) + (rgb[1] * .357580f) + (rgb[2] * .180423f));  
        xyz[1] = ((rgb[0] * .212671f) + (rgb[1] * .715160f) + (rgb[2] * .072169f));
        xyz[2] = ((rgb[0] * .019334f) + (rgb[1] * .119193f) + (rgb[2] * .950227f));
        xyz[0] = xyz[0] / 95.047f;
        xyz[1] = xyz[1] / 100.0f;
        xyz[2] = xyz[2] / 108.883f;
        if (xyz[0] > .008856f){
            xyz[0] = (float)Math.Pow(xyz[0], (1.0 / 3.0));
        }
        else{
            xyz[0] = (xyz[0] * 7.787f) + (16.0f / 116.0f);
        }
        if (xyz[1] > .008856f){
            xyz[1] = (float)Math.Pow(xyz[1], 1.0 / 3.0);
        }
        else{
            xyz[1] = (xyz[1] * 7.787f) + (16.0f / 116.0f);
        }
        if (xyz[2] > .008856f){
            xyz[2] = (float)Math.Pow(xyz[2], 1.0 / 3.0);
        }
        else{
            xyz[2] = (xyz[2] * 7.787f) + (16.0f / 116.0f);
        }
        lab[0] = (116.0f * xyz[1]) - 16.0f;
        lab[1] = 500.0f * (xyz[0] - xyz[1]);
        lab[2] = 200.0f * (xyz[1] - xyz[2]);

        float[] labL = new float[3]{lab[0],lab[1],lab[2]};
        return labL;
    }  

    //hex string to rgb int[]
    public static int[] getHexToRGB(string hex){
        int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        int[] rgb = new int[]{r,g,b};
        return rgb;
    }

    //generiert list mit bestimmten product lines (types) 
    public static void GenerateColorObjFilteredList(bool enable){
        colorObjFilteredList.Clear();

        //wenn toggle active, add to colorObjFilteredList
        if(toggleMontanaBlack.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("MontanaBlack");
        }
        if(toggleMontanaGold.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("MontanaGold");
        }
        if(toggleLoop.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("Loop");
        }
        if(toggleMTNHardcore.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("MTN-Hardcore");
        }
        if(toggleMTN94.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("MTN-94");
        }
        if(toggleMolotow.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("Molotow");
        }
        if(toggleFlameBlue.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("FlameBlue");
        }
        if(toggleFlameOrange.GetComponent<Toggle>().isOn==true){
            AddToColorObjFilteredList("FlameOrange");
        }

        //wenn keine farben vorhanden, deaktiviere savebtn
        if(colorObjFilteredList.Count==0){
            saveBtn.GetComponent<Button>().interactable = false;
        }else{
            saveBtn.GetComponent<Button>().interactable = true;
        }
    }

    public static void AddToColorObjFilteredList(string type){
        for(int a=0;a<colorObjList.Count;a++){
            if(colorObjList[a].getType().Equals(type)){
                colorObjFilteredList.Add(colorObjList[a]);
            }
        }
    }
}
