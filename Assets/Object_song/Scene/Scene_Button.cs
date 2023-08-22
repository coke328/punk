using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Button : MonoBehaviour
{
    public string SceneName;
    public void GameSceneCtrl()
    {
        SceneManager.LoadScene(SceneName);
    }
}
