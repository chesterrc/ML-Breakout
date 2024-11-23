using System;
using System.Collections;
using UnityEngine;

public class BrickStatusMap : MonoBehaviour
{
    public static BrickStatusMap Instance { get; private set; }
    private const int length = 96; // equal to num_rows * num_cols in LevelBuilder
    private float[] brick_status_map;

    public BrickStatusMap()
    {
        brick_status_map = new float[length];
        Reset();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        for (int i = 0; i < brick_status_map.Length; i++)
        {
            brick_status_map[i] = 1.0f;
        }
    }

    public void DestroyBrick(int brick_id)
    {
        brick_status_map[brick_id] = 0.0f;
    }

    public float[] BrickObservations()
    {
        return brick_status_map;
    }


}
