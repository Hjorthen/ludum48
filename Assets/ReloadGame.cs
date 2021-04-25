using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadGame : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Reload);
    }
    public void Reload()
    {
        Debug.Log("Reloading game");
        SceneManager.LoadScene(0);
    }
}
