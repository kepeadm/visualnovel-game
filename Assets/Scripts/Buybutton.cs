using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Buybutton : MonoBehaviour
{
    public string itemName;
    public ShopitemData itemData;
    public void buyListen(){
        GameObject gamemanager = GameObject.Find("Gamemanager");
        gamemanager.GetComponent<Shop>().buyListen(itemName, itemData);
    }
}