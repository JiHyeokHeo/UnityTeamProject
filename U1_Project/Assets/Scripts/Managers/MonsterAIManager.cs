using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIManager
{
    public List<Action> MonsterActions = new List<Action>(5);
    public List<GameObject> playermonsters = new List<GameObject>();
    public List<GameObject> enemymonsters = new List<GameObject>();

    // �̵���� FIFO �¸� ���� ���� ����. ���Ḯ��Ʈ�� ���� 

    public void GetMonstersInfo()
    {
        // �� ���� ������ �ϴ� �о�ͼ� 

        // 1) ���� Ŭ������ �ִ� Ž�� ���� �ȿ� �ȵ�� ������
        // 2) ���� ����� ���� ���� // ���׶� �浹 ���� ��ǥ 
        // 3) Ž�� ���� �ȿ� ������ �� ���� ���� ����
        // 4) Action ����Ʈ�� �Լ� ���

    }

    public void OnUpdate()
    {
        // TODO �ൿ�� ���ٸ� return

        if (MonsterActions != null)
        {
            //MonsterActions[0].Invoke();
        }
    }
}
