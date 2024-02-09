using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GotoLevel1()
    {
        SceneManager.LoadScene("SelectPerso");
    }
}
