using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IHeapItem<Node>
{
    public bool walkable;
    public bool semiWalkable;//create a buffer around the unwalkable areas.
    public Vector3 position;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parent;

    int heapIndex;

    public Node(bool _walkable, bool _semiWalkable, Vector3 _position, int _gridX, int _gridY)
    {
        walkable = _walkable;
        semiWalkable = _semiWalkable;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
