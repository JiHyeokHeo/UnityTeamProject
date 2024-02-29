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

    // �� ������ ����
    //// % (Ctrl), # (Shift), & (Alt)
    [MenuItem("Tools/GenerateMap %#t")]
    private static void GenerateMap()
    {
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");

        foreach (GameObject go in gameObjects)
        {
            GridMap gm = Util.GetOrAddComponent<GridMap>(go);

            using (var writer = File.CreateText($"Assets/Resources/Map/{go.name}.txt"))
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
   
    }

#endif
}
