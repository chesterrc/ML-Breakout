using UnityEngine;
using System;
using System.Collections;

public class BrickHandler : MonoBehaviour
{
    public GameObject Brick;
    public ScoreKeeper ScoreKeeper;
    public Color[] Colors { get; private set; } = {
        new Color(0.784f, 0.282f, 0.282f),
        new Color(0.776f, 0.424f, 0.227f),
        new Color(0.706f, 0.478f, 0.188f),
        new Color(0.635f, 0.635f, 0.165f),
        new Color(0.282f, 0.627f, 0.282f),
        new Color(0.259f, 0.282f, 0.784f)
    };

    public void PlaceBrick(Vector3 position, Color color, LevelBuilder levelBuilder, int brick_id, int points_value = 10)
    {
        GameObject new_obj = Instantiate(Brick, position, Quaternion.identity);
        PaintBrick(new_obj, color);

        Brick new_brick = new_obj.GetComponent<Brick>();
        new_brick.ScoreKeeper = ScoreKeeper;
        new_brick.points_value = points_value;
        new_brick.brick_id = brick_id;
        new_brick.levelBuilder = levelBuilder;
        levelBuilder.brick_status_map[brick_id] = 1.0f;
    }

    public void PaintBrick(GameObject brick, Color color)
    {
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material.color = color;
    }
}
