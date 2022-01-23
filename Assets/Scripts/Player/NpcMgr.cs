using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NpcGroup
{
    public int groupId;
    public List<GameObject> objGroup;
}

public class NpcMgr : MonoBehaviour
{
    public List<NpcGroup> groups;
    public static NpcMgr instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // npc 向右移动
    public void SetNpcGroupMoveRight(int id)
    {
        SetNpcGroupMove(id, 1);
    }

    // npc 向左移动
    public void SetNpcGroupMoveLeft(int id)
    {
        SetNpcGroupMove(id, -1);
    }

    //npc开始移动
    public void SetNpcGroupMove(int id, float input)
    {
        foreach (var group in groups)
        {
            if (group.groupId == id)
            {
                foreach (var g in group.objGroup)
                {
                    //MyTimer.instance.WaitTimer(UnityEngine.Random.Range(0, 0.6f), () =>
                    //{
                        if (g) g.GetComponent<PlayerMove>().SetNpcMove(input);
                    //});
                }
                return;
            }
        }
    }

    //npc停止移动
    public void SetNpcGroupStop(int id)
    {
        foreach (var group in groups)
        {
            if (group.groupId == id)
            {
                foreach (var g in group.objGroup)
                {
                    //MyTimer.instance.WaitTimer(UnityEngine.Random.Range(0, 0.6f), () =>
                    //{
                        if (g) g.GetComponent<PlayerMove>().SetNpcStop();
                    //});
                }
                return;
            }
        }
    }

    //销毁一组npc
    public void KillNpcGroup(int id)
    {
        foreach (var group in groups)
        {
            if (group.groupId == id)
            {
                for (int i = 0; i < group.objGroup.Count; i++)
                {
                    Destroy(group.objGroup[i]);
                }
                return;
            }
        }
    }
}