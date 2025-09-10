using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Setting : MonoBehaviour
{
    public SetData setData = new SetData();
    public GameObject soundholder;
    public GameObject bgmholder;
    public void Awake(){
        setData = setData.loadJson(setData);
        if(SceneManager.GetActiveScene().name == "Setup"){
            if(setData.boolSOUND == false){
                soundholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(66, 0, 0);
            }
            else{
                soundholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(-66, 0, 0);
            }
            if(setData.boolBGM == false){
                bgmholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(66, 0, 0);
            }
            else{
                bgmholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(-66, 0, 0);
            }
        }
    }
    public void openYoutube(){
        Application.OpenURL("http://youtube.com");
    }
    public void openTwitter(){
        Application.OpenURL("http://twitter.com");
    }
    public void openPatreon(){
        Application.OpenURL("http://patreon.com");
    }
    public void setSound(){
        if(setData.boolSOUND == true){
            setData.boolSOUND = false;
            soundholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(66, 0, 0);
            setData.saveJson(setData);
            return;
        }
        else{
            setData.boolSOUND = true;
            soundholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(-66, 0, 0);
            setData.saveJson(setData);
            return;
        }
    }
    public void setBgm(){
        if(setData.boolBGM == true){
            setData.boolBGM = false;
            bgmholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(66, 0, 0);
            setData.saveJson(setData);
            return;
        }
        else{
            setData.boolBGM = true;
            bgmholder.GetComponent<RectTransform>().anchoredPosition = new Vector3(-66, 0, 0);
            setData.saveJson(setData);
            return;
        }
    }

}

[System.Serializable]
public class SetData{
    public bool boolBGM;
    public bool boolSOUND;
    public void saveJson(SetData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            path = Path.Combine(Application.persistentDataPath+ "SetData" + ".json");
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            path = Path.Combine(Application.persistentDataPath+ "SetData" + ".json");
        }
        else{
            path = Path.Combine(Application.dataPath + "/local/Json/SetData" + ".json");
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
    public SetData loadJson(SetData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            path = Path.Combine(Application.persistentDataPath+ "SetData" + ".json");
            if (System.IO.File.Exists(path)!=true){
                TextAsset textData;
                textData = Resources.Load<TextAsset>("Json/setDefault");
                data = JsonUtility.FromJson<SetData>(textData.ToString());
                File.WriteAllText(path, textData.ToString());
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<SetData>(jsonData);
            return data;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            path = Path.Combine(Application.persistentDataPath+ "SetData" + ".json");
            if (System.IO.File.Exists(path)!=true){
                TextAsset textData;
                textData = Resources.Load<TextAsset>("Json/setDefault");
                data = JsonUtility.FromJson<SetData>(textData.ToString());
                File.WriteAllText(path, textData.ToString());
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<SetData>(jsonData);
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/local/Json/SetData" + ".json");
            if (System.IO.File.Exists(path)!=true){
                TextAsset textData;
                textData = Resources.Load<TextAsset>("Json/setDefault");
                data = JsonUtility.FromJson<SetData>(textData.ToString());
                File.WriteAllText(path, textData.ToString());
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<SetData>(jsonData);
            return data;
        }
    }
}
