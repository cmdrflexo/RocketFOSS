using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RFOSSCore
{
    public class GameManagerObject : MonoBehaviour
    {
        private void Update()
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("main_menu"))
            {
                Destroy(gameObject);
            }
        }
    }
}