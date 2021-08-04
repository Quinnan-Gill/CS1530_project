using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public bool dangerous_top;
    public bool dangerous_bottom;
    public int hurt_val;
    public bool bounce;
    public bool no_rigid;
    public string description;
}