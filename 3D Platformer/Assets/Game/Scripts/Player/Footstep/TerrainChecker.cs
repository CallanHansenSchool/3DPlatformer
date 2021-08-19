// Code from tutorial by Natty Creations on YouTube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChecker 
{
    private float[] GetTextureMix(Vector3 _playerPos, Terrain _t)
    {
        Vector3 tPos = _t.transform.position;
        TerrainData tData = _t.terrainData;
        int mapX = Mathf.RoundToInt((_playerPos.x - tPos.x) / tData.size.x * tData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((_playerPos.z - tPos.z) / tData.size.z * tData.alphamapHeight);
        float[,,] splatMapData = tData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellMix = new float[splatMapData.GetUpperBound(2) + 1];

        for (int i = 0; i < cellMix.Length; i++)
        {
            cellMix[i] = splatMapData[0, 0, i];
        }

        return cellMix;
    }

    public string GetLayerName(Vector3 _playerPos, Terrain _t)
    {
        float[] cellMix = GetTextureMix(_playerPos, _t);
        float strongest = 0;
        int maxIndex = 0;

        for(int i = 0; i < cellMix.Length; i++)
        {
            if(cellMix[i] > strongest)
            {
                maxIndex = i;
                strongest = cellMix[i];
            }
        }

        return _t.terrainData.terrainLayers[maxIndex].name;
    }
}
