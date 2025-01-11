using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomMenuItem : MonoBehaviour
{
    [MenuItem("SansDev/Open Game Scene", priority = 0)]
    static void LoadGameScene()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/_Game/Scenes/Game.unity");
        }
    }

    [MenuItem("SansDev/Customize/Credit Panel")]
    static void OpenCreditData()
    {
        string path = "Assets/Resources/ScriptableObject/Credit Data.asset";
        CreditDataSO data = (CreditDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(CreditDataSO));
        Selection.activeObject = data;
    }

    //[MenuItem("SansDev/Customize/Game Data")]
    //static void OpenObstacleColorData()
    //{
    //    string path = "Assets/Resources/ScriptableObject/GameData.asset";
    //    GameDataSO data = (GameDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(GameDataSO));
    //    Selection.activeObject = data;
    //}
}
