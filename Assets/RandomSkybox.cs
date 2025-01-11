using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    [Header("Skybox List")]
    [Tooltip("Add the Skyboxes you want to randomize here.")]
    public Material[] skyboxMaterials;

    private void Start()
    {
        ApplyRandomSkybox();
    }

    public void ApplyRandomSkybox()
    {
        if (skyboxMaterials.Length > 0)
        {
            // Select a random skybox from the list
            Material randomSkybox = skyboxMaterials[Random.Range(0, skyboxMaterials.Length)];

            // Apply the selected skybox
            RenderSettings.skybox = randomSkybox;

            // Refresh the lighting to apply changes
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Skybox list is empty! Please add skyboxes to the list.");
        }
    }
}