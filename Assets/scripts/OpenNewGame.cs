using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace quiz
{
    public class OpenNewGame : MonoBehaviour
    {
        public void NewGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}

