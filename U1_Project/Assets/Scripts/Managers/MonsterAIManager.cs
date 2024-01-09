using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIManager
{
    public List<Action> MonsterActions = new List<Action>(5);
    public List<GameObject> playermonsters = new List<GameObject>();
    public List<GameObject> enemymonsters = new List<GameObject>();

    // 이동명령 FIFO 걔를 먼저 실행 시켜. 연결리스트로 구현 

    public void GetMonstersInfo()
    {
        // 내 몬스터 정보를 싹다 읽어와서 

        // 1) 몬스터 클래스에 있는 탐지 범위 안에 안들어 왔으면
        // 2) 제일 가까운 몬스터 추적 // 동그란 충돌 센서 좌표 
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
