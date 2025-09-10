using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScaleSetter : MonoBehaviour
{
    public void Start(){
        float fixedAspectRatio = 9f / 19f;
        var thisCanvas = this.gameObject.GetComponent<CanvasScaler>();
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;
        else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;
    }
}