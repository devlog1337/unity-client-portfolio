using UnityEngine;
using UnityEngine.UI;

namespace PortfolioSamples.RenderTextureUI
{
    public sealed class RenderTexturePresenter : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        [SerializeField] private RawImage targetImage;
        [SerializeField] private Vector2Int resolution = new(1024, 1024);
        [SerializeField] private int depthBuffer = 24;

        private RenderTexture _renderTexture;

        private void Awake()
        {
            CreateRenderTexture();
        }

        private void CreateRenderTexture()
        {
            ReleaseRenderTexture();

            _renderTexture = new RenderTexture(
                resolution.x,
                resolution.y,
                depthBuffer,
                RenderTextureFormat.ARGB32)
            {
                name = "Portfolio_3D_UI_RenderTexture"
            };

            _renderTexture.Create();
            targetCamera.targetTexture = _renderTexture;
            targetImage.texture = _renderTexture;
        }

        private void OnDestroy()
        {
            ReleaseRenderTexture();
        }

        private void ReleaseRenderTexture()
        {
            if (_renderTexture == null)
                return;

            if (targetCamera != null && targetCamera.targetTexture == _renderTexture)
                targetCamera.targetTexture = null;

            if (targetImage != null && targetImage.texture == _renderTexture)
                targetImage.texture = null;

            _renderTexture.Release();
            Destroy(_renderTexture);
            _renderTexture = null;
        }
    }
}
