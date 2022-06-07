using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPaused;

    // Use DeltaTime for thing you want to pause
    // Use UnscaledTime for thing you dont want to pause 
    public static void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused) Time.timeScale = 0f;
        if (!isPaused) Time.timeScale = 1f;
    }
}
