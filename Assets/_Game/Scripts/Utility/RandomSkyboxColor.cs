using UnityEngine;

public class RandomSkyboxColor : MonoBehaviour
{
    [SerializeField] Material _skyboxMat;

    private void Start() {
        float HColor = Random.Range(0f, 1f);
        Color topColor = Color.HSVToRGB(HColor, .13f, 1f); 
        Color botColor = Color.HSVToRGB(HColor, .34f, 1f);

        _skyboxMat.SetColor("_Top", topColor);
        _skyboxMat.SetColor("_Bottom", botColor);
    }
}
