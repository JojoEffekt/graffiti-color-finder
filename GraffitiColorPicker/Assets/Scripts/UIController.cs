using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public TMP_InputField inputFieldHexRGB;
    public TMP_InputField inputFieldR;
    public TMP_InputField inputFieldG;
    public TMP_InputField inputFieldB;

    public TMP_Text yourColor;
    public TMP_Text nearestColorText0;
    public TMP_Text nearestColorText1;
    public TMP_Text nearestColorText2;
    public TMP_Text nearestColorText3;

    public GameObject userInputField;
    public GameObject nearestColorObject0;
    public GameObject nearestColorObject1;
    public GameObject nearestColorObject2;
    public GameObject nearestColorObject3;

    private string hexRGB;
    private int r;
    private int g;
    private int b;

    public void ButtonPressed(){

        hexRGB = inputFieldHexRGB.text;

        try{
            r = int.Parse(inputFieldR.text);
            g = int.Parse(inputFieldG.text);
            b = int.Parse(inputFieldB.text);

        }catch (Exception e){
            
            r = 255;
            g = 255;
            b = 255;

            userInputField.GetComponent<SpriteRenderer>().color = new Color((float)r/(float)255,(float)g/(float)255,(float)b/(float)255,1);
        }

        inputFieldHexRGB.text = "";
        inputFieldR.text = "";
        inputFieldG.text = "";
        inputFieldB.text = "";

        Debug.Log(hexRGB+" : "+r+":"+g+":"+b);

        if(CheckInputFieldHexRGB()==false){
            if(CheckImputFieldRGB()==true){
                //set color from RGB input
                userInputField.GetComponent<SpriteRenderer>().color = new Color((float)r/(float)255,(float)g/(float)255,(float)b/(float)255,1);
                
                string hexValue = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
                CollorObject nearestColor0 = ColorController.findNearestColor(hexValue)[0];
                nearestColorObject0.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor0.getRGB()[0]/(float)255,(float)nearestColor0.getRGB()[1]/(float)255,(float)nearestColor0.getRGB()[2]/(float)255,1);

                yourColor.text = "Hex code: #"+hexValue+", rgb("+r+","+g+","+b+")";
                nearestColorText0.text = nearestColor0.getType()+"-"+nearestColor0.getCapacity()+"ml "+nearestColor0.getName()+": Hex code: #"+nearestColor0.getHex()+", rgb("+nearestColor0.getRGB()[0]+","+nearestColor0.getRGB()[1]+","+nearestColor0.getRGB()[2]+")";
            
                //Partner raussuchen wo die nächste color nichtmehr mit in der list enthalten ist
                
                CollorObject nearestColor1 = ColorController.findNearestColorTest(hexValue)[1];
                nearestColorObject1.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor1.getRGB()[0]/(float)255,(float)nearestColor1.getRGB()[1]/(float)255,(float)nearestColor1.getRGB()[2]/(float)255,1);
                nearestColorText1.text = nearestColor1.getType()+"-"+nearestColor1.getCapacity()+"ml "+nearestColor1.getName()+": Hex code: #"+nearestColor1.getHex()+", rgb("+nearestColor1.getRGB()[0]+","+nearestColor1.getRGB()[1]+","+nearestColor1.getRGB()[2]+")";
                CollorObject nearestColor2 = ColorController.findNearestColorTest(hexValue)[2];
                nearestColorObject2.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor2.getRGB()[0]/(float)255,(float)nearestColor2.getRGB()[1]/(float)255,(float)nearestColor2.getRGB()[2]/(float)255,1);
                nearestColorText2.text = nearestColor2.getType()+"-"+nearestColor2.getCapacity()+"ml "+nearestColor2.getName()+": Hex code: #"+nearestColor2.getHex()+", rgb("+nearestColor2.getRGB()[0]+","+nearestColor2.getRGB()[1]+","+nearestColor2.getRGB()[2]+")";
                CollorObject nearestColor3 = ColorController.findNearestColorTest(hexValue)[3];
                nearestColorObject3.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor3.getRGB()[0]/(float)255,(float)nearestColor3.getRGB()[1]/(float)255,(float)nearestColor3.getRGB()[2]/(float)255,1);
                nearestColorText3.text = nearestColor3.getType()+"-"+nearestColor3.getCapacity()+"ml "+nearestColor3.getName()+": Hex code: #"+nearestColor3.getHex()+", rgb("+nearestColor3.getRGB()[0]+","+nearestColor3.getRGB()[1]+","+nearestColor3.getRGB()[2]+")";
            }
        }else{
            //set color from hex input
            int[] rgb = ColorController.getHexToRGB(hexRGB);

            userInputField.GetComponent<SpriteRenderer>().color = new Color((float)rgb[0]/(float)255,(float)rgb[1]/(float)255,(float)rgb[2]/(float)255,1);
            yourColor.text = "Hex code: #"+hexRGB+"  rgb("+rgb[0]+","+rgb[1]+","+rgb[2]+")";

            CollorObject nearestColor0 = ColorController.findNearestColor(hexRGB)[0];
            nearestColorObject0.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor0.getRGB()[0]/(float)255,(float)nearestColor0.getRGB()[1]/(float)255,(float)nearestColor0.getRGB()[2]/(float)255,1);
            nearestColorText0.text = nearestColor0.getType()+"-"+nearestColor0.getCapacity()+"ml "+nearestColor0.getName()+": Hex code: #"+nearestColor0.getHex()+", rgb("+nearestColor0.getRGB()[0]+","+nearestColor0.getRGB()[1]+","+nearestColor0.getRGB()[2]+")";

            CollorObject nearestColor1 = ColorController.findNearestColorTest(hexRGB)[1];
            nearestColorObject1.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor1.getRGB()[0]/(float)255,(float)nearestColor1.getRGB()[1]/(float)255,(float)nearestColor1.getRGB()[2]/(float)255,1);
            nearestColorText1.text = nearestColor1.getType()+"-"+nearestColor1.getCapacity()+"ml "+nearestColor1.getName()+": Hex code: #"+nearestColor1.getHex()+", rgb("+nearestColor1.getRGB()[0]+","+nearestColor1.getRGB()[1]+","+nearestColor1.getRGB()[2]+")";
            CollorObject nearestColor2 = ColorController.findNearestColorTest(hexRGB)[2];
            nearestColorObject2.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor2.getRGB()[0]/(float)255,(float)nearestColor2.getRGB()[1]/(float)255,(float)nearestColor2.getRGB()[2]/(float)255,1);
            nearestColorText2.text = nearestColor2.getType()+"-"+nearestColor2.getCapacity()+"ml "+nearestColor2.getName()+": Hex code: #"+nearestColor2.getHex()+", rgb("+nearestColor2.getRGB()[0]+","+nearestColor2.getRGB()[1]+","+nearestColor2.getRGB()[2]+")";
            CollorObject nearestColor3 = ColorController.findNearestColorTest(hexRGB)[3];
            nearestColorObject3.GetComponent<SpriteRenderer>().color = new Color((float)nearestColor3.getRGB()[0]/(float)255,(float)nearestColor3.getRGB()[1]/(float)255,(float)nearestColor3.getRGB()[2]/(float)255,1);
            nearestColorText3.text = nearestColor3.getType()+"-"+nearestColor3.getCapacity()+"ml "+nearestColor3.getName()+": Hex code: #"+nearestColor3.getHex()+", rgb("+nearestColor3.getRGB()[0]+","+nearestColor3.getRGB()[1]+","+nearestColor3.getRGB()[2]+")";
        }
    }

    public bool CheckInputFieldHexRGB(){
        if(hexRGB.Length==6){//ist der hex wert 6 oder 7 zeichen lang?
            if(IsHex(hexRGB)==true){//guck ob hex wert nur hex zeichen enthält
                return true;
            }
        }else if(hexRGB.Length==7){//wenn ja, guck ob 1 zeichen '#'
            if(hexRGB[0]=='#'){
                string val = hexRGB.Substring(1);
                if(IsHex(val)==true){//guck ob hex wert nur hex zeichen enthält
                    hexRGB = val;
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckImputFieldRGB(){
        if(IsRGB(r)==true&&IsRGB(g)==true&&IsRGB(b)==true){
            Debug.Log("2 true");
            return true;
        }else{
            Debug.Log("rgb length error");
        }
        return false;
    }

    public bool IsHex(string val){
        for(int a=0;a<val.Length;a++){
            if((val[a]==('0')||val[a]==('1')||val[a]==('2')||val[a]==('3')||val[a]==('4')||val[a]==('5')||val[a]==('6')||val[a]==('7')||val[a]==('8')||val[a]==('9')||val[a]==('a')||val[a]==('b')||val[a]==('c')||val[a]==('d')||val[a]==('e')||val[a]==('f')||val[a]==('A')||val[a]==('B')||val[a]==('C')||val[a]==('D')||val[a]==('E')||val[a]==('F'))==false){
                return false;
            }
        }
        return true;
    }

    public bool IsRGB(int val){
        if(val>=0&&val<=255){
            return true; 
        }
        return false;
    }
}
