using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIManager
{
    public List<Action> MonsterActions = new List<Action>(5);
    public List<GameObject> playermonsters;
    public List<GameObject> enemymonsters;

    public void GetMonstersInfo()
    {
        // 내 몬스터 정보를 싹다 읽어와서 

        // 1) 몬스터 클래스에 있는 탐지 범위 안에 안들어 왔으면
        // 2) 제일 가까운 몬스터 추적 
        // 3) 탐지 범위 안에 들어왔을 시 공격 패턴 시작
        // 4) Action 리스트에 함수 등록
        
    }

    public void OnUpdate()
    {
        // TODO 행동이 없다면 return

        if (MonsterActions != null)
        {
            //MonsterActions[0].Invoke();
        }
    }
}
