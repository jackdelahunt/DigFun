using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tree", menuName = "DigFun/Tree")]
public class Tree : ScriptableObject
{
    public int logId;
    public int leafId;
    public int maxHeight;
    public int minHeight;
}
