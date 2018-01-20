using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour {

    Queue<PathReQuest> pathRequestQueue = new Queue<PathReQuest>();
    PathReQuest currentPathRequest;

    static PathRequestManager instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action <Vector3[], bool> callback)
    {
        PathReQuest newRequest = new PathReQuest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool succes)
    {
        currentPathRequest.callback(path, succes);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathReQuest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathReQuest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;                
        }
    }
}
