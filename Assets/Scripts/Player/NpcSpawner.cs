using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 2;
    private TimeCounter timer;

    void OnEnable()
    {
        timer = MyTimer.instance.StartTimer(spawnInterval, () =>
        {
            GameObject go = Instantiate(prefab, transform);
            go.GetComponent<PlayerMove>().SetNpcMove(1);
        });
    }

    private void OnDisable()
    {
        timer.Kill();
    }

    private void OnDestroy()
    {
        timer.Kill();
    }

}
