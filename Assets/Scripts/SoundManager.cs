using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public SetData setData = new SetData();
    public void Awake(){
        setData = setData.loadJson(setData);
    }
    public void Start(){
        if(SceneManager.GetActiveScene().name == "Main"){
            playbgm("main");
        }
    }
    public List<SoundData> soundData = new List<SoundData>();
    public AudioClip buttonclip;
    public AudioClip bgmclip;
    public void playbuttonsound(){
        if(setData.boolSOUND){
            AudioSource audiosource = getSource("buttonclick");
            buttonclip = Resources.Load<AudioClip>("sounds/buttonclick");
            audiosource.clip = buttonclip;
            audiosource.Play();
        }
    }
    public void playbgm(string name){
        if(setData.boolBGM){
            AudioSource audiosource = getSource("bgm");
            bgmclip = Resources.Load<AudioClip>("sounds/bgm_main");
            audiosource.clip = bgmclip;
            audiosource.Play();
        }
    }
    public AudioSource getSource(string name){
        foreach(SoundData sd in soundData){
            if(sd.name == name){
                return sd.audiosource;
            }
        }
        return null;
    }
}

[System.Serializable]
public class SoundData{
    public string name;
    public AudioSource audiosource;
}