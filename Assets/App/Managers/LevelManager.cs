using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelLoadPolicy
{
    SingleSceneMultiLevelMethod,
    SingleSceneSingleLevelMethod,  
    SeperateSceneSingleLevelMethod,
    SeperateSceneMultiLevelMethod,
}

[System.Serializable]
public class MasterLevelConfiguration
{
    public LevelLoadPolicy LevelLoadPolicy;
    public List<LevelData> LevelDatas;

}

public class LevelData
{
    public Scene Scene;
    public List<Level> Levels;
}

public class Level : MonoBehaviour
{
    public int LevelCount;
    public int MaxLevelRunCount;

}


public class LevelManager : MonoBehaviour
{
    [SerializeField] MasterLevelConfiguration MasterLevelConfiguration;

    private Scene CurrentScene;



    private void CreateSequenceByPolicy()
    {
        switch (MasterLevelConfiguration.LevelLoadPolicy)
        {
            case LevelLoadPolicy.SingleSceneMultiLevelMethod:
                break;
            case LevelLoadPolicy.SingleSceneSingleLevelMethod:
                break;
            case LevelLoadPolicy.SeperateSceneSingleLevelMethod:
                break;
            case LevelLoadPolicy.SeperateSceneMultiLevelMethod:
                break;
            default:
                break;
        }
    }

    private void RunLevel()
    {

    }
}
