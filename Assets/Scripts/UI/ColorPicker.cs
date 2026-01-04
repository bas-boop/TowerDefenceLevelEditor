using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class ColorPicker : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private RawImage svImage;
        [SerializeField] private RawImage hueImage;
        [SerializeField] private Slider hueSlider;
        [SerializeField] private Image colorPreview;
        
        [SerializeField] private int textureSize = 256;
        [SerializeField] private int hueTextureWidth = 256;
        [SerializeField] private int hueTextureHeight = 1;

        private Texture2D _svTexture;
        private Texture2D _hueTexture;

        private float _hue;
        private float _saturation = 1f;
        private float _value = 1f;

        private void Awake()
        {
            CreateSvTexture();
            CreateHueTexture();
        }

        private void Start()
        {
            hueSlider.onValueChanged.AddListener(OnHueChanged);
            UpdateSvTexture();
            UpdateColor();
        }

        private void CreateSvTexture()
        {
            _svTexture = new (textureSize, textureSize, TextureFormat.RGB24, false)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Bilinear
            };
            
            svImage.texture = _svTexture;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UpdateSv(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateSv(eventData);
        }

        private void OnHueChanged(float h)
        {
            _hue = h;
            UpdateSvTexture();
            UpdateColor();
        }

        private void CreateHueTexture()
        {
            _hueTexture = new (hueTextureWidth, hueTextureHeight, TextureFormat.RGB24, false)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Bilinear
            };

            for (int x = 0; x < hueTextureWidth; x++)
            {
                float h = (float) x / (hueTextureWidth - 1);
                Color color = Color.HSVToRGB(h, 1f, 1f);
                _hueTexture.SetPixel(x, 0, color);
            }

            _hueTexture.Apply();
            hueImage.texture = _hueTexture;
        }
        
        private void UpdateSv(PointerEventData eventData)
        {
            RectTransform rect = svImage.rectTransform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );

            Rect r = rect.rect;

            _saturation = Mathf.Clamp01((localPoint.x - r.x) / r.width);
            _value = Mathf.Clamp01((localPoint.y - r.y) / r.height);

            UpdateColor();
        }

        private void UpdateSvTexture()
        {
            for (int y = 0; y < textureSize; y++)
            {
                float v = (float)y / (textureSize - 1);

                for (int x = 0; x < textureSize; x++)
                {
                    float s = (float)x / (textureSize - 1);
                    Color color = Color.HSVToRGB(_hue, s, v);
                    _svTexture.SetPixel(x, y, color);
                }
            }

            _svTexture.Apply();
        }

        private void UpdateColor()
        {
            Color c = Color.HSVToRGB(_hue, _saturation, _value);
            colorPreview.color = c;
        }

        public Color GetSelectedColor()
        {
            return Color.HSVToRGB(_hue, _saturation, _value);
        }
    }
}
