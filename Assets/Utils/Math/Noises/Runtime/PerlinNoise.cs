using UnityEngine;

[System.Serializable]
public struct PerlinNoise
{
    public PerlinNoise(Vector2Int resolution, Vector2 offset, Vector2 scale)
    {
        _resolution = resolution;
        _offset = offset;
        _scale = scale;
    }
    
    public PerlinNoise(int width, int height, Vector2 offset, float scale)
    {
        _resolution = new Vector2Int(width, height);
        _offset = offset;
        _scale = new Vector2(scale, scale);
    }

    private Vector2Int _resolution;
    private Vector2 _offset;
    private Vector2 _scale;

    public float GetValue(int x, int y)
    {
        var coord = new Vector2(x, y);
        return GetValue(coord);
    }
    
    public float GetNormalizedValue(float x, float y)
    {
        var coord = new Vector2(x, y);
        coord = coord * _scale + _offset;
        return Mathf.PerlinNoise(coord.x, coord.y);
    }

    public float GetValue(Vector2 coord)
    {
        coord = (coord / _resolution) * _scale + _offset;
        return Mathf.PerlinNoise(coord.x, coord.y);
    }

    public Texture2D GetTexture()
    {
        var texture = new Texture2D(_resolution.x, _resolution.x);

        for (int x = 0; x < _resolution.x; x++)
        {
            for (int y = 0; y < _resolution.y; y++)
            {
                var value = GetValue(x, y);
                var color = new Color(value, value, value);
                texture.SetPixel(x, y, color);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
    
    /* public Texture2D GetTextureFast()
    {
        var texture = new Texture2D(_resolution.x, _resolution.x);
        var mipCount = texture.mipmapCount;
        var colors = GetColors();

        texture.SetPixels(colors);
        texture.filterMode = FilterMode.Point;
        texture.Apply(false);
        return texture;
    }

    private Color[] GetColors()
    {
        var colors = new Color[_resolution.x * _resolution.y];
        for (int x = 0; x < _resolution.x; x++)
        {
            for (int y = 0; y < _resolution.y; y++)
            {
                var value = GetValue(x, y);
                var index = x * y + x;
                colors[index] = new Color(value, value, value);
            }
        }

        return colors;
    } */
}