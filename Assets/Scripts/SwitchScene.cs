using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{

    void Start()
    {
        CurrentButtonColor();
        TMP_Text text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    private void CurrentButtonColor()
    {
        if (gameObject.name == getCurrentScene())
        {
            Button thisButton = gameObject.GetComponent<Button>();
            ColorBlock cb = thisButton.colors;
            cb.normalColor = Color.green;
            thisButton.colors = cb;
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    private string getCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

}
