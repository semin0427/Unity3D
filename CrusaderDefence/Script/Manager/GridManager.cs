using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

    public enum TileType {
        Plain,
        Wall,
        Unit
    }

    public enum TileLocate
    {
        Area1,
        Area2,
        Area3,
        Area4
    }

    public TileType[] world = null;
    public TileLocate[] locate = null;

    int n_x;
    int n_z;

    static Vector3[] corners = { new Vector3(1, 0, 0), new Vector3(0, 0, 1) };

    public GameObject player;
    public GameObject tile;

    public GameObject tileA;
    public GameObject tileB;

    public GameObject TreeBox;
    public GameObject _tree;

    ListManager lm;
    PlayManager pm;
    void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        lm = GameObject.Find("ListMgr").GetComponent<ListManager>();
        pm = GameObject.Find("Castle").GetComponent<PlayManager>();
    }

    void Update()
    {
    }

    public void SetLocate(int n_row, int n_col)
    {
        n_x = n_row;
        n_z = n_col;

        int max_tiles = n_x * n_z;

        locate = new TileLocate[max_tiles];

        for (int x = 0; x < n_x; x++)
        {
            for (int z = 0; z < n_z; z++)
            {
                if ((x >= 0 && x < n_x / 2) && (z >= 0 && z < n_z / 2))
                    locate[x + z * n_x] = TileLocate.Area1;
                else if ((x >= n_x / 2 && x < n_x) && (z >= 0 && z < n_z / 2))
                    locate[x + z * n_x] = TileLocate.Area2;
                else if ((x >= 0 && x < n_x / 2) && (z >= n_z / 2 && z < n_z))
                    locate[x + z * n_x] = TileLocate.Area3;
                else if ((x >= n_x / 2 && x < n_x) && (z >= n_z / 2 && z < n_z))
                    locate[x + z * n_x] = TileLocate.Area4;
            }
        }
    }

    public TileLocate looseLocate(int n_row, int n_col)
    {
        SetLocate(n_row, n_col);

        int[] PlaneArea = new int[4];

        n_x = n_row;
        n_z = n_col;

        int max_tiles = n_x * n_z;

        for (int i = 0; i < max_tiles; i++)
        {
            if (world[i] == TileType.Plain)
            {
                if (locate[i] == TileLocate.Area1)
                    PlaneArea[0]++;
                if (locate[i] == TileLocate.Area2)
                    PlaneArea[1]++;
                if (locate[i] == TileLocate.Area3)
                    PlaneArea[2]++;
                if (locate[i] == TileLocate.Area4)
                    PlaneArea[3]++;
            }
        }
        
        TileLocate MaxPlaneArea = new TileLocate();

        if (PlaneArea[0] >= PlaneArea[1] && PlaneArea[0] >= PlaneArea[2]
             && PlaneArea[0] >= PlaneArea[3])
        {
            MaxPlaneArea = TileLocate.Area1;
        }
        else if (PlaneArea[1] >= PlaneArea[0] && PlaneArea[1] >= PlaneArea[2]
             && PlaneArea[1] >= PlaneArea[3])
        {
            MaxPlaneArea = TileLocate.Area2;
        }
        else if (PlaneArea[2] >= PlaneArea[1] && PlaneArea[2] >= PlaneArea[0]
             && PlaneArea[2] >= PlaneArea[3])
        {
            MaxPlaneArea = TileLocate.Area3;
        }
        else if (PlaneArea[3] >= PlaneArea[1] && PlaneArea[3] >= PlaneArea[2]
             && PlaneArea[3] >= PlaneArea[0])
        {
            MaxPlaneArea = TileLocate.Area4;
        }

        return MaxPlaneArea;
    }

    public Vector3 locateToCenter(Vector3 pos)
    {
        return pos + new Vector3(0.5f, 0, 0.5f);
    }

    Vector3 locateToRightBottom(Vector3 pos)
    {
        return pos + new Vector3(-0.5f, 0, 0.5f);
    }

    void CameraPos()
    {
        transform.position = player.transform.position + new Vector3(0, 11.32f, -5); // locate camera properly.
        transform.rotation = Quaternion.AngleAxis(46.71f, new Vector3(46.71f, 0, 0));
    }

    public void BuildWorld(int n_rows, int n_cols, float x_pos, float y_pos)
    {
        int max_tiles = n_rows * n_cols;

        n_x = n_rows;
        n_z = n_cols;

        // set up the player's position to the center of the grid.
        player.transform.position = new Vector3(x_pos, 0, y_pos); // place the player to the center.
        int player_cell = pos2Cell(player.transform.position);
        CameraPos();

        // construct a game world and assign walls.
        world = new TileType[max_tiles];
        for (int i = 0; i < max_tiles; i++)
        {
            if (i == player_cell) continue; // we assign the player's location as a plain grid cell.

            if (Random.Range(0.0f, 1.0f) < 0.1f) // wall is created with a probability of 25 %.
            {
                if (i % n_rows == 0 || i % n_rows == n_rows - 1 || i < n_rows || (i >= n_rows * (n_cols - 1) && i < n_rows * n_cols))
                {
                    world[i] = TileType.Plain; //가장자리는 plain
                }
                else if (Application.loadedLevel == 2)
                {
                    world[i] = TileType.Wall;
                    var wall = Instantiate<GameObject>(tileA);
                    wall.transform.parent = tile.transform;
                    wall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    wall.transform.Rotate(new Vector3(180, 0, 0));
                    wall.transform.position = locateToCenter(cell2Pos(i));
                }
                else if(Application.loadedLevel == 3 && i < 2700)
                {
                    world[i] = TileType.Wall;
                    var wall = Instantiate<GameObject>(tileA);
                    wall.transform.parent = tile.transform;
                    wall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    wall.transform.Rotate(new Vector3(180, 0, 0));
                    wall.transform.position = locateToCenter(cell2Pos(i));
                }
                else if (Application.loadedLevel == 4 || Application.loadedLevel == 9)
                {
                    if (i / 120 > 28 && i / 120 < 91)
                    {
                        if (i % 120 > 28 && i % 120 < 91)
                        {
                            world[i] = TileType.Wall;
                            var wall = Instantiate<GameObject>(tileA);
                            wall.transform.parent = tile.transform;
                            wall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            wall.transform.Rotate(new Vector3(180, 0, 0));
                            wall.transform.position = locateToCenter(cell2Pos(i));
                        }
                    }
                }
            }

            if (world[i] == TileType.Plain)
            {
                //var wall = GameObject.CreatePrimitive(PrimitiveType.Plane);
                var wall = Instantiate<GameObject>(tileB);
                wall.transform.parent = tile.transform;
                wall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                wall.transform.position = locateToCenter(cell2Pos(i));
                wall.transform.Rotate(new Vector3(180, 0, 0));
            }

            if (world[i] == TileType.Wall)
            {
                if (Application.loadedLevel == 3 && i == 800)
                    return;
                GameObject Tree = Instantiate(_tree);
                Tree.transform.parent = TreeBox.transform;
                Tree.transform.position = locateToCenter(cell2Pos(i));
                //lm.TreeList.Add(Tree);
            }
        }

        for (int i = 0; i < max_tiles; i++) drawRect(i, world[i] == TileType.Plain ? Color.black : Color.green);
    }
        
    public int pos2Cell(Vector3 pos)
    {
        return ((int)pos.z) * n_x + (int)pos.x;
    }

    public Vector3 cell2Pos(int cellno)
    {
        return new Vector3(cellno % n_x, 0, cellno / n_x);
    }

    void drawRect(int cellno, Color c, float duration=10000.0f)
    {
        Vector3 lb = cell2Pos(cellno);
        Debug.DrawLine(lb, lb + corners[0], c, duration);
        Debug.DrawLine(lb, lb + corners[1], c, duration);
        Vector3 rt = lb + corners[0] + corners[1];
        Debug.DrawLine(rt, rt - corners[0], c, duration);
        Debug.DrawLine(rt, rt - corners[1], c, duration);
    }

    int[] findNeighbors(int cellno, TileType[] world)
    {
        List<int> neighbors = new List<int> { -1, 1, -n_x, n_x, -n_x - 1, -n_x + 1, n_x - 1, n_x + 1 };

        if (cellno % n_x == 0)          neighbors.RemoveAll( (no) => { return no == -1   || no == -1 - n_x || no == -1 + n_x; } );
        if (cellno % n_x == n_x - 1)    neighbors.RemoveAll( (no) => { return no ==  1   || no ==  1 - n_x || no ==  1 + n_x; } );
        if (cellno / n_x == 0)          neighbors.RemoveAll( (no) => { return no == -n_x || no == -n_x - 1 || no == -n_x + 1; } );
        if (cellno / n_x == n_z - 1)    neighbors.RemoveAll( (no) => { return no ==  n_x || no ==  n_x - 1 || no ==  n_x + 1; } );
        
        for (int i=0; i< neighbors.Count; )
        {
            neighbors[i] += cellno;

            if (neighbors[i] < 0 || neighbors[i] >= n_x * n_z || world[neighbors[i]] == TileType.Wall)
                neighbors.RemoveAt(i);
            else i++; /* increase unless removing */
        }

        // remove crossing-neighbors if they are blocked by two adjacent walls. See ppt page 41.
        Vector3 X = cell2Pos(cellno);
        for (int i=0; i < neighbors.Count; )
        {
            Vector3 Xp = cell2Pos(neighbors[i]);
            if ( (X.x - Xp.x) * (X.z - Xp.z) != 0)
            {
                if ( world[ pos2Cell(new Vector3(Xp.x, 0, X.z))] == TileType.Wall
                    && world[ pos2Cell(new Vector3(X.x, 0, Xp.z))] == TileType.Wall )
                {
                    neighbors.RemoveAt(i);
                    continue;
                }
            }
            i++;
        }
        return neighbors.ToArray();
    }

    int[] findNeighborsForWorker(int cellno, TileType[] world)
    {
        List<int> neighbors = new List<int> { -1, 1, -n_x, n_x, -n_x - 1, -n_x + 1, n_x - 1, n_x + 1 };

        if (cellno % n_x == 0) neighbors.RemoveAll((no) => { return no == -1 || no == -1 - n_x || no == -1 + n_x; });
        if (cellno % n_x == n_x - 1) neighbors.RemoveAll((no) => { return no == 1 || no == 1 - n_x || no == 1 + n_x; });
        if (cellno / n_x == 0) neighbors.RemoveAll((no) => { return no == -n_x || no == -n_x - 1 || no == -n_x + 1; });
        if (cellno / n_x == n_z - 1) neighbors.RemoveAll((no) => { return no == n_x || no == n_x - 1 || no == n_x + 1; });

        for (int i = 0; i < neighbors.Count;)
        {
            neighbors[i] += cellno;

            if (neighbors[i] < 0 || neighbors[i] >= n_x * n_z)
                neighbors.RemoveAt(i);
            else i++; /* increase unless removing */
        }

        // remove crossing-neighbors if they are blocked by two adjacent walls. See ppt page 41.
        Vector3 X = cell2Pos(cellno);
        for (int i = 0; i < neighbors.Count;)
        {
            Vector3 Xp = cell2Pos(neighbors[i]);
            if ((X.x - Xp.x) * (X.z - Xp.z) != 0)
            {
                if (world[pos2Cell(new Vector3(Xp.x, 0, X.z))] == TileType.Wall
                    && world[pos2Cell(new Vector3(X.x, 0, Xp.z))] == TileType.Wall)
                {
                    neighbors.RemoveAt(i);
                    continue;
                }
            }
            i++;
        }
        return neighbors.ToArray();
    }

    int[] buildPath(int[] parents, int from, int to)
    {
        if (parents == null) return null;

        List<int> path = new List<int>();
        int current = to;
        while ( current != from )
        {
            path.Add(current);
            current = parents[current];
        }
        path.Add(from); // to -> ... -> ... -> from

        path.Reverse(); // from -> ... -> ... -> to
        return path.ToArray();
    }

    void drawPath(int[] path)
    {
        if (path == null) return;

        for (int i=0; i< path.Length-1; i++)
        {
            Debug.DrawLine(locateToCenter(cell2Pos(path[i])), locateToCenter(cell2Pos(path[i + 1])), Color.blue, 5.0f);
        }
    }

    int[] findShortestPath(int from, int to, TileType[] world)
    {
        int max_tiles = n_x * n_z;

        if (from < 0 || from >= max_tiles || to < 0 || to >= max_tiles) return null;

        // initialize the parents of all tiles to negative value, implying no tile number associated.
        int[] parents = new int[max_tiles];
        for (int i = 0; i < parents.Length; i++) parents[i] = -1;

        List<int> N = new List<int>() { from };
        while (N.Count > 0) 
        {
            int current = N[0]; N.RemoveAt(0); // dequeue

            int[] neighbors = findNeighbors(current, world);
            foreach (var neighbor in neighbors)
            {
                if (neighbor == to) 
                { 
                    // found the destination
                    parents[neighbor] = current;
                    return buildPath(parents, from, to); // read parents array and construct the shoretest path by traversal
                }

                if (parents[neighbor] == -1) // neighbor's parent is not set yet.
                {
                    parents[neighbor] = current; // make current tile as neighbor's parent
                    N.Add(neighbor); // enqueue
                }
            }
        }
        return null; // I cannot find any path from source to destination        
    }

    int[] findShortestPathForWorker(int from, int to, TileType[] world)
    {
        int max_tiles = n_x * n_z;

        if (from < 0 || from >= max_tiles || to < 0 || to >= max_tiles) return null;

        // initialize the parents of all tiles to negative value, implying no tile number associated.
        int[] parents = new int[max_tiles];
        for (int i = 0; i < parents.Length; i++) parents[i] = -1;

        List<int> N = new List<int>() { from };
        while (N.Count > 0)
        {
            int current = N[0]; N.RemoveAt(0); // dequeue

            int[] neighbors = findNeighborsForWorker(current, world);
            foreach (var neighbor in neighbors)
            {
                if (neighbor == to)
                {
                    // found the destination
                    parents[neighbor] = current;
                    return buildPath(parents, from, to); // read parents array and construct the shoretest path by traversal
                }

                if (parents[neighbor] == -1) // neighbor's parent is not set yet.
                {
                    parents[neighbor] = current; // make current tile as neighbor's parent
                    N.Add(neighbor); // enqueue
                }
            }
        }
        return null; // I cannot find any path from source to destination        
    }

    public IEnumerator Move(GameObject player, Vector3 destination, float _spd)
    {
        int start = pos2Cell(player.transform.position);
        int end = pos2Cell(destination);
        int[] path = findShortestPath(start, end, world);
        if (path == null)
        {
            if (lm.DefenceDefUnit.Count == 0)
                Application.LoadLevel(Application.loadedLevel); //시작하자마자 길이없으면 다시시작
            else
            {
                world[pos2Cell(pm.lastObject.transform.position)] = TileType.Plain;
                lm.DefenceDefUnit.Remove(pm.lastObject);
                Destroy(pm.lastObject);
            }
            //yield break;
        }

        // path should start from "source" to "destination".
                
        drawPath(path);
        List<int> remaining = new List<int>(path); // convert int array to List
        remaining.RemoveAt(0); // we don't need the first one, since the first element should be same as that of source.
        while (remaining.Count > 0)
        {
            int to = remaining[0]; remaining.RemoveAt(0);

            Vector3 toLoc = locateToCenter(cell2Pos(to));
            while (player.transform.position != toLoc)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, toLoc, _spd * Time.deltaTime);
                player.transform.LookAt(toLoc);
                drawRect(pos2Cell(player.transform.position), Color.red, Time.deltaTime);
                yield return null;
            }
        }
    }

    public IEnumerator MoveForWorker(GameObject player, Vector3 destination, float _spd)
    {
        int start = pos2Cell(player.transform.position);
        int end = pos2Cell(destination);
        int[] path = findShortestPathForWorker(start, end, world);
        if (path == null) yield break;

        // path should start from "source" to "destination".

        drawPath(path);
        List<int> remaining = new List<int>(path); // convert int array to List
        remaining.RemoveAt(0); // we don't need the first one, since the first element should be same as that of source.
        while (remaining.Count > 0)
        {
            int to = remaining[0]; remaining.RemoveAt(0);

            Vector3 toLoc = locateToCenter(cell2Pos(to));
            while (player.transform.position != toLoc)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, toLoc, _spd * Time.deltaTime);
                player.transform.LookAt(toLoc);
                drawRect(pos2Cell(player.transform.position), Color.red, Time.deltaTime);
                yield return null;
            }
        }
    }
}
