using UnityEngine;

namespace GunPrototype.Decals
{
    public class DecalsCanvasPainter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Decal _decal;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryPaint();
            }
        }

        private void TryPaint()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                DecalsCanvas decalsCanvas = hit.collider.GetComponent<DecalsCanvas>();
                if (decalsCanvas != null)
                {
                    Vector2 uv = hit.textureCoord;
                    //uv.y = 1 - uv.y;
                    decalsCanvas.Print(_decal, uv);
                }
            }
        }
    }
}