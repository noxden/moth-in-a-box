//----------------------------------------------------------------
// Darmstadt University of Applied Sciences, Expanded Realities
// Course: AR Art- and App-Development (Jan Alexander)
// Script by:    Jan Alexander, Daniel Heilmann (771144)
// Last changed:  22-02-22
//----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager
{
    public static int northMin
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.northMin, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.northMin, value);
        }
    }
    public static int northMax
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.northMax, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.northMax, value);
        }
    }
    public static int eastMin
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.eastMin, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.eastMin, value);
        }
    }
    public static int eastMax
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.eastMax, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.eastMax, value); ;
        }
    }
    public static int southMin
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.southMin, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.southMin, value);
        }
    }
    public static int southMax
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.southMax, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.southMax, value);
        }
    }
    public static int westMin
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.westMin, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.westMin, value);
        }
    }
    public static int westMax
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsKey.westMax, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.westMax, value);
        }
    }
}

//> This class works almost like an enum for the PlayerPrefs
public class PlayerPrefsKey     
{
    public static string northMin = "northMin";
    public static string northMax = "northMax"; 
    public static string eastMin = "eastMin"; 
    public static string eastMax = "eastMax";
    public static string southMin = "southMin";
    public static string southMax = "southMax";
    public static string westMin = "westMin";
    public static string westMax = "westMax";
}