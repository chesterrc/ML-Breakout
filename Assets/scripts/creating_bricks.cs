using UnityEngine;

public class creating_bricks : MonoBehaviour
{
    private Color[] color_arr = {
        new Color(0.784f, 0.282f, 0.282f),
        new Color(0.776f, 0.424f, 0.227f),
        new Color(0.706f, 0.478f, 0.188f),
        new Color(0.635f, 0.635f, 0.165f),
        new Color(0.282f, 0.627f, 0.282f),
        new Color(0.259f, 0.282f, 0.784f)
    };

    public int col_len, row_len;
    public float x_space, y_space;
    public float x_start, y_start;
    public GameObject Brick;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 start_block_placement = new Vector3(x_start, y_start);
        for (int i = 0; i < col_len * row_len; i++)
        {
            Vector3 block_spacing = new Vector3(x_space * (i % col_len),
            y_start + (y_space * (i / col_len)));

            Vector3 next_block_placement = start_block_placement + block_spacing;
            PlaceBrick(next_block_placement, color_arr[(i / col_len) % color_arr.Length]);
        }
    }

    public void PlaceBrick(Vector3 position, Color color)
    {
        GameObject new_brick = Instantiate(original: Brick, position, Quaternion.identity);
        PaintBrick(new_brick, color);
    }

    public void PaintBrick(GameObject brick, Color color)
    {
        Renderer renderer = brick.GetComponent<Renderer>();
        renderer.material.color = color;
    }

}
