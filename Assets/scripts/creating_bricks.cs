using System.Collections;
using System.Collections.Generic;
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
    public GameObject brick;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 start_block_placement = new Vector3 (x_start, y_start);
        for(int i = 0; i < col_len * row_len; i++){
            Vector3 block_spacing = new Vector3 (x_space * (i % col_len),
            y_start + (y_space * (i / col_len)));

            Vector3 next_blocK_placement = start_block_placement + block_spacing;

            GameObject new_brick = Instantiate(brick, next_blocK_placement, Quaternion.identity);
            Renderer renderer = new_brick.GetComponent<Renderer>();
            renderer.material.color = color_arr[(i/row_len) % color_arr.Length];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
