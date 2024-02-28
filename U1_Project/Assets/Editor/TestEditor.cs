using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestEditor 
{
#if UNITY_EDITOR

    // 맵 데이터 추출
    //// % (Ctrl), # (Shift), & (Alt)
    [MenuItem("Tools/GenerateMap %#t")]
    private static void GenerateMap()
    {
        GameObject obj = GameObject.Find("Astar");
        if (obj == null)
            return;

        GridMap gm = Util.GetOrAddComponent<GridMap>(obj);
        if (gm == null)
            return;

        using (var writer = File.CreateText("Assets/Resources/Map/output.txt"))
        {
            writer.WriteLine(gm._gridSizeX);
            writer.WriteLine(gm._gridSizeY);

            for (int y = gm._gridSizeY - 1; y >= 0; y--)
            {
                for (int x = 0; x < gm._gridSizeX; x++)
                {
                    if (gm._grid[x, y]._walkable == false)
                        writer.Write("1");
                    else
                        writer.Write("0");
                }
                writer.WriteLine();
            }

        }
    }

#endif
}
