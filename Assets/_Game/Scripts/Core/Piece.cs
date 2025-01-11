using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] GameData _data;
    [Space]
    [SerializeField] Renderer _renderer;

    private void Start()
    {
        _renderer.material = _data.GetSelectedMaterial();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
