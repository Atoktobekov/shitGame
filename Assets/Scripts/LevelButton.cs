using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string sceneName;   
    public int levelNumber;    
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        int unlockedLevel = LevelManager.GetUnlockedLevel();

        button.interactable = levelNumber <= unlockedLevel;
        button.onClick.AddListener(() =>
        {
            SceneController.instance.loadScene(sceneName);
        });
    }
}