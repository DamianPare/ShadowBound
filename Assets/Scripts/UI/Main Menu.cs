using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlScreenON()
    {
        AudioManager.instance.PlaySound(TypeOfSound.UIButton, 0.5f);
        controlScreen.SetActive(true);
    }

    public void ControlScreenOFF()
    {
        AudioManager.instance.PlaySound(TypeOfSound.UIButton, 0.5f);
        controlScreen.SetActive(false);
    }

    public void PlayGame()
    {
        AudioManager.instance.PlaySound(TypeOfSound.UIButton, 0.5f);
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySound(TypeOfSound.UIButton, 0.5f);
        Application.Quit();
    }
}
