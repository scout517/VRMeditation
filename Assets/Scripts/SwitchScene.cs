using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<Button>().interactable = !(gameObject.name == getCurrentScene());
        TMP_Text text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    private string getCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

}
