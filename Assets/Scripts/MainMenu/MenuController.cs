using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MathPresentation
{
    public class MenuController : MonoBehaviour
    {
       public void LoadDocs()
        {
            SceneManager.LoadSceneAsync("Main");
        }
    }
}