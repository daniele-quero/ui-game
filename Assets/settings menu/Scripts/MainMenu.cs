using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Canvas _canvas;

    private void OnEnable()
    {
        _canvas = GetComponent<Canvas>();
        GameChoiceButton.OpenGameChoice += Deactivate;
        ProfileButton.OpenProfile += Deactivate;
        MainMenuButton.GoToMain += Activate;
    }

    private void Activate()
    {
        _canvas.enabled = true;
        //foreach (var scene in SceneManager.GetAllScenes().Select(s => s).Where(s => s.name.Contains("_game")))
        //{
        //    SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        //}
    }

    private void Deactivate(bool otherActive)
    {
        if (otherActive)
        {
            _canvas.enabled = false;
        }
    }
}
