using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDealay = 1.0f;
    [SerializeField] private GameObject[] dropPrefabs;
    [SerializeField] private GameObject[] dropLines;
    [SerializeField] private GameObject spawnLine;
    [SerializeField] private GameObject endLine;
    [SerializeField] private GameObject destroyLine;
    private LinkedList<GameObject>[] lineObjectsArrays;
    private Vector3 [] spawnPoints;
    private float lastSpawnTime;
    // Start is called before the first frame update
    private void Awake()
    {
        lastSpawnTime = Time.realtimeSinceStartup - spawnDealay;
        lineObjectsArrays = new LinkedList<GameObject>[dropPrefabs.Length];
        spawnPoints = new Vector3[dropPrefabs.Length];
    }
    void Start()
    {
        for (int lineCounter = 0; lineCounter < spawnPoints.Length; ++lineCounter)
        {
            lineObjectsArrays[lineCounter] = new LinkedList<GameObject>();
            spawnPoints[lineCounter].x = dropLines[lineCounter].transform.position.x;
            spawnPoints[lineCounter].y = spawnLine.transform.position.y;
        }
    }

    void Update()
    {
        foreach (LinkedList<GameObject> lineObjects in lineObjectsArrays)
        {
            DestroyFallenCubes(lineObjects);
        }
        if (Time.realtimeSinceStartup - lastSpawnTime > spawnDealay)
        {
            SpawnRandomDrop();
            lastSpawnTime = Time.realtimeSinceStartup;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressFirst(lineObjectsArrays[0]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PressFirst(lineObjectsArrays[1]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            PressFirst(lineObjectsArrays[2]);
        }
    }

    private void DestroyFallenCubes(LinkedList<GameObject> lineObjects)
    {
        while (lineObjects.Count > 0 && lineObjects.First.Value.transform.position.y < destroyLine.transform.position.y)
        {
            Destroy(lineObjects.First.Value);
            lineObjects.RemoveFirst();
        }
    }

    public void PressFirst(LinkedList<GameObject> lineObjects)
    {
        LinkedListNode<GameObject> currentLineObject = lineObjects.First;
        float resultDistance;
        while (currentLineObject != null)
        {
            resultDistance = currentLineObject.Value.transform.position.y - endLine.transform.position.y;
            if (resultDistance > 0)
            {
                Destroy(currentLineObject.Value);
                lineObjects.Remove(currentLineObject);
                Debug.Log("Precision: " + resultDistance);
                return;
            }
            currentLineObject = currentLineObject.Next;
        }
    }

    public void SpawnRandomDrop()
    {
        int lineIndex = Random.Range(0, dropPrefabs.Length);
        lineObjectsArrays[lineIndex].AddLast(Instantiate<GameObject>(dropPrefabs[lineIndex], spawnPoints[lineIndex], dropPrefabs[lineIndex].transform.rotation));
    }
}
