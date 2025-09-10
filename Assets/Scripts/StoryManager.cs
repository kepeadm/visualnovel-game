using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class StoryManager : MonoBehaviour
{
    public StoryData storyData = new StoryData();
    public SaveData saveData;
    public GameObject namebox;
    public GameObject chatbox;
    public GameObject selecter;
    public GameObject selecterPrefab;
    public void Start(){
        SaveData savedata = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.loadData;
        saveData = new SaveData();
        if(savedata.part > 0){
            loadStory(savedata.storyname, savedata.part, savedata.partindex, savedata.scriptindex);
            return;
        }
        else{
            startStory(savedata.storyname);
        }
    }
    public void Update(){
        if(saveData.part != storyData.part){
            saveData.part = storyData.part;
        }
    }
    public void saveStory(){
        GameObject.Find("Gamemanager").GetComponent<Player>().playerData.saveData.Add(saveData);
        GameObject.Find("Gamemanager").GetComponent<Player>().playerData.saveJson(GameObject.Find("Gamemanager").GetComponent<Player>().playerData);
    }
    public void loadStory(string name, int part, int partindex, int scriptindex){

        storyData = storyData.loadJson(name, storyData);
        openNowScript(partindex, scriptindex);

    }
    public void startStory(string name){
        storyData = storyData.loadJson(name, storyData);
        saveData.storyname = name;
        openNowScript(0, 0);
    }
    public void openNowScript(int partindex, int scriptindex){
        saveData.partindex = partindex;
        saveData.scriptindex = scriptindex;

        PartData partData = new PartData();
        partData = storyData.partData[partindex];
        ScriptData scriptData = new ScriptData();
        scriptData = partData.scriptData[scriptindex];
        GameObject.Find("Canvas").GetComponent<Image>().sprite = Resources.Load<Sprite>(scriptData.backgroundSpritePath);
        if(scriptData.type != "select"){
            selecter.SetActive(false);
            GameObject buttonObject = GameObject.Find("Canvas").transform.Find("chatbox").gameObject;
            StoryCommandData scD = new StoryCommandData();
            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            if(scriptData.command.Count >= 1){
                foreach(StoryCommandData command in scriptData.command){
                    if(command.exp == true){
                        buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.expCommand(command.exptype, command.expname, command.expamount);});
                    }
                    if(command.goscript == true){
                        buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.scriptCommand(command.gopartindex,command.goscriptindex);});
                    }
                    if(command.setpart == true){
                        buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.partCommand(command.parttype,command.partamount);});
                    }
                    if(command.goMain == true){
                        buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.GoMain();});
                    }
                }
            }
        }
        if(scriptData.type == "chat"){
            GameObject sceneObject=null;
            foreach(Transform child in GameObject.Find("Illusts").transform){
                child.gameObject.SetActive(false);
                if(child.gameObject.name == "group1"){
                    child.gameObject.SetActive(true);
                    sceneObject = child.gameObject;
                }
            }
            CharacterData cD1 = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.getCharacter(scriptData.characterName[0]);
            sceneObject.transform.Find("person1").transform.Find("body").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + cD1.name + "/" + cD1.name+ "_body");
            sceneObject.transform.Find("person1").transform.Find("body").gameObject.GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person1").transform.Find("hair").GetComponent<Image>().sprite = cD1.hair.getSprite();
            sceneObject.transform.Find("person1").transform.Find("hair").gameObject.GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person1").transform.Find("cloth").GetComponent<Image>().sprite = cD1.cloth.getSprite();
            sceneObject.transform.Find("person1").transform.Find("cloth").gameObject.GetComponent<Image>().SetNativeSize();

            CharacterData cD2 = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.getCharacter(scriptData.characterName[1]);
            sceneObject.transform.Find("person2").transform.Find("body").GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + cD2.name + "/" + cD2.name+ "_body");;
            sceneObject.transform.Find("person2").transform.Find("body").GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person2").transform.Find("hair").GetComponent<Image>().sprite = cD2.hair.getSprite();
            sceneObject.transform.Find("person2").transform.Find("hair").GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person2").transform.Find("cloth").GetComponent<Image>().sprite = cD2.cloth.getSprite();
            sceneObject.transform.Find("person2").transform.Find("cloth").GetComponent<Image>().SetNativeSize();

            namebox.GetComponent<TextMeshProUGUI>().text = scriptData.speaker;
            chatbox.GetComponent<TextMeshProUGUI>().text = scriptData.text;
        }
        else if(scriptData.type == "chatsolo"){
            GameObject sceneObject=null;
            foreach(Transform child in GameObject.Find("Illusts").transform){
                child.gameObject.SetActive(false);
                if(child.gameObject.name == "group2"){
                    child.gameObject.SetActive(true);
                    sceneObject = child.gameObject;
                }
            }
            CharacterData cD1 = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.getCharacter(scriptData.characterName[0]);
            sceneObject.transform.Find("person1").transform.Find("body").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + cD1.name + "/" + cD1.name+ "_body");
            sceneObject.transform.Find("person1").transform.Find("body").gameObject.GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person1").transform.Find("hair").GetComponent<Image>().sprite = cD1.hair.getSprite();
            sceneObject.transform.Find("person1").transform.Find("hair").gameObject.GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person1").transform.Find("cloth").GetComponent<Image>().sprite = cD1.cloth.getSprite();
            sceneObject.transform.Find("person1").transform.Find("cloth").gameObject.GetComponent<Image>().SetNativeSize();

            namebox.GetComponent<TextMeshProUGUI>().text = scriptData.speaker;
            chatbox.GetComponent<TextMeshProUGUI>().text = scriptData.text;
        }
        else if(scriptData.type == "solo"){
            GameObject sceneObject=null;
            foreach(Transform child in GameObject.Find("Illusts").transform){
                child.gameObject.SetActive(false);
                if(child.gameObject.name == "group3"){
                    child.gameObject.SetActive(true);
                    sceneObject = child.gameObject;
                }
            }
            CharacterData cD1 = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.getCharacter(scriptData.characterName[0]);
            sceneObject.transform.Find("person1").transform.Find("body").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + cD1.name + "/" + cD1.name+ "_body");
            sceneObject.transform.Find("person1").transform.Find("body").gameObject.GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person1").transform.Find("hair").GetComponent<Image>().sprite = cD1.hair.getSprite();
            sceneObject.transform.Find("person1").transform.Find("hair").gameObject.GetComponent<Image>().SetNativeSize();
            sceneObject.transform.Find("person1").transform.Find("cloth").GetComponent<Image>().sprite = cD1.cloth.getSprite();
            sceneObject.transform.Find("person1").transform.Find("cloth").gameObject.GetComponent<Image>().SetNativeSize();

            namebox.GetComponent<TextMeshProUGUI>().text = scriptData.speaker;
            chatbox.GetComponent<TextMeshProUGUI>().text = scriptData.text;
        }
        else if(scriptData.type == "event"){
            foreach(Transform child in GameObject.Find("Illusts").transform){
                child.gameObject.SetActive(false);
            }
            namebox.GetComponent<TextMeshProUGUI>().text = scriptData.speaker;
            chatbox.GetComponent<TextMeshProUGUI>().text = scriptData.text;
        }
        else if(scriptData.type == "select"){
            GameObject sceneObject=null;
            GameObject chatboxObject = GameObject.Find("Canvas").transform.Find("chatbox").gameObject;
            chatboxObject.GetComponent<Button>().onClick.RemoveAllListeners();
            foreach(Transform child in GameObject.Find("Illusts").transform){
                child.gameObject.SetActive(false);
            }
            selecter.SetActive(true);
            foreach(Transform child in selecter.transform){
                Destroy(child.gameObject);
            }
            foreach(StoryCommandData command in scriptData.command){
                GameObject buttonObject = Instantiate(selecterPrefab, selecter.transform);
                buttonObject.transform.Find("text").GetComponent<TextMeshProUGUI>().text = command.text;
                buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
                if(command.exp == true){
                    buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.expCommand(command.exptype, command.expname, command.expamount);});
                }
                if(command.goscript == true){
                    buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.scriptCommand(command.gopartindex,command.goscriptindex);});
                }
                if(command.setpart == true){
                    buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.partCommand(command.parttype,command.partamount);});
                }
                if(command.goMain == true){
                    buttonObject.GetComponent<Button>().onClick.AddListener(delegate{command.GoMain();});
                }
            }
            namebox.GetComponent<TextMeshProUGUI>().text = scriptData.speaker;
            chatbox.GetComponent<TextMeshProUGUI>().text = scriptData.text;
        }
    }
}

[System.Serializable]
public class StoryData{
    public string name;
    public int part = 0;
    public List<PartData> partData = new List<PartData>();
    public StoryData loadJson(string name, StoryData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Stories/"+name);
            data = JsonUtility.FromJson<StoryData>(textData.ToString());
            return data;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Stories/"+name);
            data = JsonUtility.FromJson<StoryData>(textData.ToString());
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/Stories/"+name + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<StoryData>(jsonData);
            return data;
        }
    }
}


[System.Serializable]
public class PartData{
    public List<ScriptData> scriptData = new List<ScriptData>();
}


[System.Serializable]
public class ScriptData{
    public string type;
    public string speaker;
    public string text;
    public int day;
    public string time;
    public List<StoryCommandData> command = new List<StoryCommandData>();
    public List<string> characterName = new List<string>();
    public string backgroundSpritePath;
}

[System.Serializable]
public class StoryCommandData{
    public string text;

    public bool exp = false;
    public string exptype;
    public string expname;
    public int expamount;

    public bool goscript = false;
    public int gopartindex;
    public int goscriptindex;

    public bool setpart = false;
    public string parttype;
    public int partamount;

    public bool goMain = false;
    public void GoMain(){
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void partCommand(string parttype, int partamount){
        if(parttype == "set"){
            GameObject.Find("Gamemanager").GetComponent<StoryManager>().storyData.part = partamount;
        }
        else if(parttype == "add"){
            GameObject.Find("Gamemanager").GetComponent<StoryManager>().storyData.part += partamount;
        }
    }
    public void expCommand(string exptype, string expname, int expamount){
        if(exptype == "set"){
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            playerData.getCharacter(expname).exp = expamount;
            playerData.saveJson(playerData);
        }
        else if(exptype == "remove"){
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            playerData.getCharacter(expname).exp = playerData.getCharacter(expname).exp - expamount;
            playerData.saveJson(playerData);
        }
        else if(exptype == "add"){
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            playerData.getCharacter(expname).exp = playerData.getCharacter(expname).exp + expamount;
            playerData.saveJson(playerData);
        }
    }
    public void scriptCommand(int gopartindex, int goscriptindex){
        GameObject.Find("Gamemanager").GetComponent<StoryManager>().openNowScript(gopartindex, goscriptindex);
    }
}


[System.Serializable]
public class PartCommandData{
    public string type;
    public int amount;
}


[System.Serializable]
public class ExpData{
    public string type;
    public string name;
    public int amount;
}


[System.Serializable]
public class GoScriptData{
    public int partindex;
    public int scriptindex;
}
