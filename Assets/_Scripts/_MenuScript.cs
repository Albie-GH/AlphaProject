using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayLevel");
    }
}
