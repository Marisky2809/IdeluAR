using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Escena a cargar")]
    public string sceneToLoad;
    public void LoadAssignedScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no está asignado.");
        }
    }
}
