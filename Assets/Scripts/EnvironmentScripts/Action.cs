using UnityEngine;
using UnityEngine.SceneManagement;

public class Action : MonoBehaviour
{
    public void myAction() { 
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        if (SceneManager.GetActiveScene().name == "Titles")
            SceneManager.LoadScene("Room");
        else if (SceneManager.GetActiveScene().name == "Room")
            SceneManager.LoadScene("Titles");
    }
}