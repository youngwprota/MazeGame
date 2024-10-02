using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControles : MonoBehaviour
{
    public void PlayPressed(){
        Debug.Log("Play button pressed");
        SceneManager.LoadScene("Game");
    }

    public void ExitPres(){
        Application.Quit();
        Debug.Log("Exit pressed");
    }

    public void MenuPres()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Exit pressed");
    }

    public void LeaderBoardPres()
    {
        SceneManager.LoadScene("LeadersBoard");
    }
}
