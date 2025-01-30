using UnityEngine;
using UnityEngine.SceneManagement;

public class TempLoadScene : MonoBehaviour
{
    [SerializeField] private string _nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }
}
