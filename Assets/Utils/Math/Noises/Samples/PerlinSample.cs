using UnityEngine;

public class PerlinSample : MonoBehaviour 
{
    public Renderer Renderer;
    public Vector2 OffsetGlobal;
    public Vector2 OffsetX;
    public Vector2 OffsetY;
    public Vector2 ScaleGlobal;
    public Vector2 ScaleX;
    public Vector2 ScaleY;
    public float DepthX;
    public float DepthY;
    public float Saturation;
    public float Contract;
    [Min(1)]
    public int PixelPerUnit = 1;


    private void OnValidate() 
    {
        if(! Renderer) return;

        var quadSize = Renderer.bounds.size;
        var resolution = new Vector2Int((int)quadSize.x, (int)quadSize.y) * PixelPerUnit;
        var perlin = new PerlinNoise(resolution, OffsetGlobal, ScaleGlobal);
        var perlinX = new PerlinNoise(resolution, OffsetX, ScaleX);
        var perlinY = new PerlinNoise(resolution, OffsetY, ScaleY);
        var texture2D = new Texture2D(resolution.x, resolution.x);

        for (int x = 0; x < resolution.x; x++)
        {
            for (int y = 0; y < resolution.y; y++)
            {
                var valueX = perlinX.GetValue(x, y) + DepthX;
                var valueY = perlinY.GetValue(x, y) + DepthY;
                var value = perlin.GetNormalizedValue(valueX, valueY);
                value = Mathf.Pow(value, Contract);
                value *= Saturation;
                var color = new Color(value, value, value);
                texture2D.SetPixel(x, y, color);
            }
        }

        texture2D.filterMode = FilterMode.Point;
        texture2D.Apply();

        var propertyBlock = new MaterialPropertyBlock();
        Renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetTexture("_MainTex", texture2D);
        Renderer.SetPropertyBlock(propertyBlock);
    }
}