using UnityEngine;

public class BrickHandler : MonoBehaviour
{
    public GameObject Brick;
    public ScoreKeeper score_keeper;

    public void PlaceBrick(Vector3 position, Color color, int points_value = 10)
    {
        GameObject new_obj = Instantiate(Brick, position, Quaternion.identity);
        PaintBrick(new_obj, color);

        Brick new_brick = new_obj.GetComponent<Brick>();
        new_brick.score_keeper = score_keeper;
        new_brick.points_value = points_value;
    }

    public void PaintBrick(GameObject brick, Color color)
    {
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material.color = color;
    }
}
