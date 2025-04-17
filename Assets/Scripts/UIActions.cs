using UnityEngine;

public class UIActions : MonoBehaviour
{
    public void ResetLevelProgress()
    {
        LevelManager.ResetProgress();
        Debug.Log("Прогресс сброшен!");
    }
}