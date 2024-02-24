using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CollorObject : MonoBehaviour
{
    //attribute
    private string type;
    private int capacity;
    private string hex;
    private string name;
    private string altName;
    private int num;
    private int r;
    private int g;
    private int b;
    private float[] lab;

    //constructor
    public CollorObject(string type, int capacity, string hex, string name, string altName, int num, int[] rgb, float[] lab){
        this.type = type;
        this.capacity = capacity;
        this.hex = hex;
        this.name = name;
        this.altName = altName;
        this.num = num;
        this.r = rgb[0];
        this.g = rgb[1];
        this.b = rgb[2];
        this.lab = lab;
    }

    //methoden
    public void Info(){
        Debug.Log(type+"-"+capacity+"ml : "+name+" [#"+hex+","+altName+", rgb{"+r+","+g+","+b+"},"+num+", lab{"+lab[0]+","+lab[1]+","+lab[2]+"}]");
    }

    //getter
    public string getType(){
        return this.type;
    }
    public int getCapacity(){
        return this.capacity;
    }
    public string getHex(){
        return this.hex;
    }
    public string getName(){
        return this.name;
    }
    public string getAltName(){
        return this.altName;
    }
    public int getNum(){
        return this.num;
    }
    public List<int> getRGB(){
        List<int> a = new List<int>();
        a.Add(this.r);
        a.Add(this.g);
        a.Add(this.b);
        return a;
    }
    public float[] getLab(){
        return this.lab;
    }

    //setter
    public void setType(string type){
        this.type = type;
    }
    public void setCapacity(int capacity){
        this.capacity = capacity;
    }
    public void setHex(string hex){
        this.hex = hex;
    }
    public void setName(string name){
        this.name = name;
    }
    public void setAltName(string altName){
        this.altName = altName;
    }
    public void setNum(int num){
        this.num = num;
    }
}
