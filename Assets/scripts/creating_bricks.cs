using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creating_bricks : MonoBehaviour
{
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

            Instantiate(brick, next_blocK_placement, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
