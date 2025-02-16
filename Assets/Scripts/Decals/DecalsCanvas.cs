using UnityEngine;

namespace GunPrototype.Decals
{

    public class DecalsCanvas : MonoBehaviour
    {
        [SerializeField] private MeshFilter _planeMeshFilter;
        [SerializeField] private MeshRenderer _planeMeshRenderer;
        [SerializeField] private int _texturePixelsPerUnit = 128;

        private RenderTexture _renderTexture;
        private Vector2Int _canvasSize;
        private Material _blitMaterial;
        private Material _drawMaterial;

        private void Start()
        {
            InitializeMaterials();
            InitializeCanvasSize();
            InitializeRenderTexture();
            ClearRenderTextureWithColor(_drawMaterial.color);
        }

        public void ClearRenderTextureWithColor(Color color)
        {
            Graphics.SetRenderTarget(_renderTexture);
            GL.Clear(true, true, color);
            Graphics.SetRenderTarget(null);
        }

        public void Print(Decal decal, Vector2 uvCenter)
        {
            if (decal.Sprite == null || _renderTexture == null) return;

            _blitMaterial.mainTexture = decal.Sprite.texture;
            _blitMaterial.color = decal.Color;

            uvCenter.y = 1 - uvCenter.y;
            Vector2 uvSize = GetUVSize(decal.Sprite, decal.Scale);
            Vector2 minUV = uvCenter - uvSize * 0.5f;
            Vector2 maxUV = uvCenter + uvSize * 0.5f;

            Graphics.SetRenderTarget(_renderTexture);
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, _renderTexture.width, _renderTexture.height, 0);

            Graphics.DrawTexture(
                new Rect(minUV.x * _renderTexture.width, minUV.y * _renderTexture.height,
                         uvSize.x * _renderTexture.width, uvSize.y * _renderTexture.height),
                decal.Sprite.texture, _blitMaterial);

            GL.PopMatrix();
            Graphics.SetRenderTarget(null);
        }

        private void InitializeMaterials()
        {
            _drawMaterial = new Material(_planeMeshRenderer.material);
            _planeMeshRenderer.material = _drawMaterial;
            _blitMaterial = new Material(Shader.Find("Custom/UnlitWithColorAndAlpha"));
        }

        private void InitializeCanvasSize()
        {
            Vector3 meshBounds = _planeMeshFilter.mesh.bounds.size;
            Vector3 transformScale = _planeMeshFilter.transform.localScale;
            _canvasSize = Vector2Int.RoundToInt(new Vector2(
                meshBounds.x * transformScale.x, meshBounds.z * transformScale.z));
        }

        private void InitializeRenderTexture()
        {
            _renderTexture = new RenderTexture(
                _canvasSize.x * _texturePixelsPerUnit,
                _canvasSize.y * _texturePixelsPerUnit,
                0,
                RenderTextureFormat.ARGB32);

            _renderTexture.Create();
            _drawMaterial.mainTexture = _renderTexture;
        }

        private Vector2 GetUVSize(Sprite sprite, Vector2 spriteScale)
        {
            Vector2Int renderTextureSize = new Vector2Int(_renderTexture.width, _renderTexture.height);
            Vector2 spriteSize = new Vector2(sprite.texture.width, sprite.texture.height);

            return new Vector2(
                (spriteSize.x * spriteScale.x) / renderTextureSize.x,
                (spriteSize.y * spriteScale.y) / renderTextureSize.y
            );
        }

        private void OnDestroy()
        {
            if (_renderTexture != null)
            {
                _renderTexture.Release();
                Destroy(_renderTexture);
            }

            if (_drawMaterial != null)
            {
                Destroy(_drawMaterial);
            }

            if (_blitMaterial != null)
            {
                Destroy(_blitMaterial);
            }
        }
    }
}