using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventHandler : MonoBehaviour
{
    public SceneService sceneService;

    public delegate void stringDelegate(string _s);
    public static event stringDelegate EmitCurrentScene;
    public static void __EmitCurrentScene(string _s) { if (EmitCurrentScene != null) EmitCurrentScene(_s); }

    private void OnEnable()
    {
        SubscribeToEvents(true);
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }

    private void SubscribeToEvents(bool state)
    {
        if(state)
        {
            Door.DoorEvent                         += UpdateSceneState;
            PlayerCurrentScene.RequestCurrentScene += UpdatePlayerScene;
        }
        else
        {
            Door.DoorEvent                         -= UpdateSceneState;
            PlayerCurrentScene.RequestCurrentScene -= UpdatePlayerScene;
        }
    }

    private void UpdatePlayerScene(PlayerCurrentScene _playerSceneComponent, GameObject _g)
    {
        sceneService.SetPlayerCurrentScene(_playerSceneComponent, _g);
    }

    private void UpdateSceneState(Door _door)
    {
        sceneService.PlayerEnter(_door);
        __EmitCurrentScene(_door.inThisScene);
    }
}
