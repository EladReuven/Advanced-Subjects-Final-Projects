using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private float _scale = 5f; // Scale of the noise

    [SerializeField] private int _size = 5; // Scale of the noise

    [SerializeField] private AnimationCurve _heightCurve; // Curve to modify the generated heights
    [SerializeField] private AnimationCurve _extraHeightCurve; // Curve to modify the generated heights

    [SerializeField] private float _randomOffset = 0.1f; // Random offset for height variation

    [SerializeField] private float _randomBumps = 0.0001f; // Random offset for height variation

    private int _width = 512;  // Width of the terrain
    private int _height = 512; // Height of the terrain
    private Terrain _terrain => GetComponent<Terrain>();

    TerrainData _terrainData => _terrain.terrainData;

    private void Start()
    {
        Generate();
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[_width, _height];

        int _fwidth = _width * _size;
        int _fheight = _height * _size;

        float[,] fullHeights = new float[_fwidth, _fheight];

        // Calculate the maximum offset values to prevent exceeding the bounds
        float maxOffsetX = Mathf.Min(_randomOffset, _fwidth - _width);
        float maxOffsetY = Mathf.Min(_randomOffset, _fheight - _height);

        // Generate random offsets within the adjusted limits
        float offsetX = Random.Range(0, maxOffsetX);
        float offsetY = Random.Range(0, maxOffsetY);


        for (int x = 0; x < _fwidth; x++)
        {
            for (int y = 0; y < _fheight; y++)
            {
                float xCoord = (float)x / _fwidth * _scale;
                float yCoord = (float)y / _fheight * _scale;

                float heightValue = Mathf.PerlinNoise(xCoord, yCoord);
                fullHeights[x, y] = heightValue;

                // Apply the height curve to modify the generated heights
                heightValue = _heightCurve.Evaluate(heightValue);

                // Apply the height curve to modify the generated heights
                heightValue *= _extraHeightCurve.Evaluate(heightValue);

                // Add random offset for height variation
                heightValue += Random.Range(-_randomBumps, _randomBumps);

                fullHeights[x, y] = heightValue;
            }
        }

        // Assign fullHeights to heights
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                heights[x, y] = fullHeights[x+(int)offsetX, y +(int)offsetY];
            }
        }

        return heights;
    }

    [ContextMenu("Regenerate")]
    private void Generate()
    {
        // Get the current heightmap resolution
        int heightmapResolution = _terrainData.heightmapResolution;

        _width = heightmapResolution;
        _height= heightmapResolution;

        // Generate heights using Perlin noise
        float[,] heights = GenerateHeights();

        // Assign the generated heights to the terrain
        _terrain.terrainData.SetHeights(0, 0, heights);
    }
}


