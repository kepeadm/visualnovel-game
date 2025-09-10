using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public GameObject slotList;
    public List<SaveData> saveData = new List<SaveData>();
    public void Start(){
        saveData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.saveData;
        foreach(SaveData savedata in saveData){
            GameObject saveIcon = Instantiate(slotPrefab, slotList.transform);
            string text;
            text = "스토리 : " + savedata.storyname.ToString() + "\n파트 : " + savedata.part.ToString();
            saveIcon.transform.Find("text").GetComponent<TextMeshProUGUI>().text = text;
            saveIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            saveIcon.GetComponent<Button>().onClick.AddListener(delegate{loadSaveData(savedata);});
        }
    }
    public void loadSaveData(SaveData savedata){
        GameObject.Find("Gamemanager").GetComponent<Player>().playerData.loadData = savedata;
        GameObject.Find("Gamemanager").GetComponent<Player>().playerData.saveJson(GameObject.Find("Gamemanager").GetComponent<Player>().playerData);
        SceneManager.LoadScene("Story", LoadSceneMode.Single);
    }
}