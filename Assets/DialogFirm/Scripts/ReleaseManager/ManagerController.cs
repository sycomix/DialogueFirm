﻿using UnityEngine;

using System.IO;
using DialogFirm;
using UnityEngine.UI;


/// <summary>
/// Bot controller.
/// </summary>
public class ManagerController : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    private BotEngine bot;
    public Sprite angrySprite;
    public Sprite happySprite;
    public Sprite bitAngrySprite;
    private SpriteRenderer spriteRenderer;
    public Image managerImage;

    public void Start()
    {
        Debug.Log("Called Star()");
        angrySprite = Resources.Load<Sprite>("DialogFirm/ReleaseManager/manager-angry") as Sprite;
        happySprite = Resources.Load<Sprite>("DialogFirm/ReleaseManager/manager-happy") as Sprite;
        bitAngrySprite = Resources.Load<Sprite>("DialogFirm/ReleaseManager/manager-bit-angry") as Sprite;
        managerImage.sprite = happySprite;
        this.LoadConfig();
    }

    public void SaveText()
    {
        var reply = this.bot.ReplySentence(inputField.text);
        int angerLevel = bot.State.GetInt("anger-level");
		this.ChangeImage(angerLevel);
        text.text = reply;
        inputField.text = "";
    }

    void LoadConfig()
    {
        string settingFilePath = this.GetStreamingAssetsPath("DialogFirm/ReleaseManager/manager-conf.json");
        string settingString = File.ReadAllText(settingFilePath);
        this.bot = new BotEngine(settingString);
    }

	void ChangeImage(int angerLevel)
	{
		if (angerLevel == 0)
        {
            managerImage.sprite = happySprite;
        } 
        else if (angerLevel == 1)
        {
            managerImage.sprite = bitAngrySprite;
        }
        else
		{
            managerImage.sprite = angrySprite;
        }
	}

    string GetStreamingAssetsPath(string suffix)
    {
        string path;
#if UNITY_EDITOR
        path = Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID
     path = "jar:file://"+ Application.dataPath + "!/assets/";
#elif UNITY_IOS
     path = Application.dataPath + "/Raw/";
#else
     //Desktop (Mac OS or Windows)
     path = Application.dataPath + "/StreamingAssets/";
#endif
        path = path + suffix;
        return path;
    }
}
