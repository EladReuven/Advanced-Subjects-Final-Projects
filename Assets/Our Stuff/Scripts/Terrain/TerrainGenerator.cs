using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 512;  // Width of the terrain
    public int height = 512; // Height of the terrain
    public float scale = 5f; // Scale of the noise

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();

        // Generate heights using Perlin noise
        float[,] heights = GenerateHeights();

        // Assign the generated heights to the terrain
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                float heightValue = Mathf.PerlinNoise(xCoord, yCoord);
                heights[x, y] = heightValue;
            }
        }

        return heights;
    }
}