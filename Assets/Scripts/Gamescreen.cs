using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamescreen : MonoBehaviour
{
    public void GameSetup(){
        SceneManager.LoadScene("Setup", LoadSceneMode.Additive);
    }
    public void GameSave(){
        SceneManager.LoadScene("Save", LoadSceneMode.Single);
    }
    public void GameMain(){
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void GameIntro(){
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
    public void GameNew(){
        List<SaveData> saveData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData.saveData;
        GameObject.Find("Gamemanager").GetComponent<Player>().playerData.loadData = saveData[0];
        GameObject.Find("Gamemanager").GetComponent<Player>().playerData.saveJson(GameObject.Find("Gamemanager").GetComponent<Player>().playerData);
        SceneManager.LoadScene("Story", LoadSceneMode.Single);
    }
    public void unloadsetup(){
        SceneManager.UnloadSceneAsync("Setup");
    }
}