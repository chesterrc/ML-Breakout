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
    public BrickHandler brick_handler;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 start_block_placement = new Vector3(x_start, y_start);
        for (int i = 0; i < col_len * row_len; i++)
        {
            Vector3 block_spacing = new Vector3(x_space * (i % col_len),
            y_start + (y_space * (i / col_len)));

            Vector3 next_block_placement = start_block_placement + block_spacing;
            Color brick_color = color_arr[(i / col_len) % color_arr.Length];
            int points_value = (i / col_len + 1) * 10;
            brick_handler.PlaceBrick(next_block_placement, brick_color, points_value);
        }
    }

}
