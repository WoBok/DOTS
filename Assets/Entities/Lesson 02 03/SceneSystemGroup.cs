using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract partial class SceneSystemGroup : ComponentSystemGroup
{
    protected abstract string AuthoringSceneName { get; }
    bool initialized;
    protected override void OnCreate()
    {
        base.OnCreate();
        initialized = false;
    }
    protected override void OnUpdate()
    {
        if (!initialized)
        {
            if (SceneManager.GetActiveScene().isLoaded)
            {
                var subScene = Object.FindFirstObjectByType<SubScene>();
                if (subScene != null)
                {
                    Enabled = AuthoringSceneName == subScene.gameObject.scene.name;
                }
                else
                {
                    Enabled = false;
                }
                initialized = true;
            }
        }

        base.OnUpdate();
    }
}