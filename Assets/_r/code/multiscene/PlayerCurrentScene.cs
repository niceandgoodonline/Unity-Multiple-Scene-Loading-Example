using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentScene : MonoBehaviour
{
    public string currentScene;
    public LayerMask groundLayer;

    public delegate void updateSelfDelegate(PlayerCurrentScene _self, GameObject _g);
    public static event updateSelfDelegate RequestCurrentScene;
    public static void __RequestCurrentScene(PlayerCurrentScene _self, GameObject _g) 
    {
        if (RequestCurrentScene != null) RequestCurrentScene(_self, _g); 
    }

    public void __init()
    {
        if (currentScene == "") StartCoroutine(InitializePlayerScene());
    }

    private IEnumerator InitializePlayerScene()
    {
        GameObject _g = this.gameObject;
        while(true)
        {
            yield return null;
            _g = FindGround();
            if (_g != null) break;
        }
        __RequestCurrentScene(this, _g);
    }

    private GameObject FindGround()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale * 5, Quaternion.identity, groundLayer);
        if (hitColliders.Length > 0) return hitColliders[0].gameObject;
        return null;
    }

    private void OnEnable()
    {
        __init();
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
            SceneEventHandler.EmitCurrentScene += SetCurrentScene;
        }
        else
        {
            SceneEventHandler.EmitCurrentScene -= SetCurrentScene;
        }
    }

    private void SetCurrentScene(string _scene)
    {
        currentScene = _scene;
    }
}
