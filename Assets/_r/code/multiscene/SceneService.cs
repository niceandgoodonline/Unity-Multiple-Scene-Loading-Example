using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    public bool logging;
    public string currentScene;
    public List<string> requestedScenes;
    public List<string> loadedScenes;
    private PlayerCurrentScene playerSceneComponent;
    private List<string> alwaysActiveScenes = new List<string>(){"Layer0", "Player", "UI"};

    private void Start()
    {
        AsyncManageRequestedScenes();
        foreach(string _s in alwaysActiveScenes) AsyncLoadAdditive(_s);
    }

    private void AsyncManageRequestedScenes()
    {
        foreach (string s in requestedScenes)
        {
            AsyncLoadAdditive(s);
            if (!loadedScenes.Contains(s)) loadedScenes.Add(s);
            if (logging) slog.inst.Print($"Additive loaded {s} and added it to list of active scenes.");
        }
        UnloadScenes();
    }

    public void AsyncLoadAdditive(string sceneName)
    {
        Scene _scn = SceneManager.GetSceneByName(sceneName);
        if (!_scn.isLoaded) SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    private void UnloadScenes()
    {
        List<string> staleScenes = new List<string>();
        foreach (string _snc in loadedScenes)
        {
            if (!requestedScenes.Contains(_snc))
            {
                try
                {
                    SceneManager.UnloadSceneAsync(_snc);
                    if (logging) slog.inst.Print($"{_snc} is loaded but not requested. Unloading {_snc}");
                }
                catch
                {
                    if (logging) slog.inst.Print($"tried to unload {_snc} but had an error. marking list item for removal.");
                    staleScenes.Add(_snc);
                }
            }
        }
        RemoveFromLoadedList(staleScenes);
    }

    private void RemoveFromLoadedList(List<string> _stale)
    {
        foreach(string _snc in _stale) loadedScenes.Remove(_snc);
    }

    public string GetSceneObjectIn(GameObject _g)
    {
    	return _g.scene.name;
    }

    public void SetPlayerCurrentScene(PlayerCurrentScene _playerSceneComponent, GameObject _g)
    {
        playerSceneComponent              = _playerSceneComponent;
        playerSceneComponent.currentScene = GetSceneObjectIn(_g);
        currentScene                      = playerSceneComponent.currentScene;
        EnsureCurrentSceneActive();
    }


    public void PlayerEnter(Door _door)
    {
        if (currentScene != _door.inThisScene) currentScene = _door.inThisScene;
        requestedScenes = new List<string>();
        foreach (string _s in _door.scenesConnectedTo) requestedScenes.Add(_s);
        EnsureCurrentSceneActive();
        AsyncManageRequestedScenes();
    }

    private void EnsureCurrentSceneActive()
    {
        if (!requestedScenes.Contains(currentScene)) requestedScenes.Add(currentScene);
    }
}
