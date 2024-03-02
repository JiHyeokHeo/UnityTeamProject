using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;


public enum Rarity
{
    //일반
    COMMON,
    //희귀
    RARE,
    //영웅
    HERO,
    //에픽
    EPIC,
    //레전드
    LEGEND,
}

public enum SkinTheme
{
    //프로젝트 스킨
    CYBORG,
    //겨울왕국
    FROZEN,
    //거리의 악마
    NIGHTMARE,
    //아르카나
    ARCANA,
    //꿀벌
    HONEYBEE,
}

public enum JobThema
{
    //검사
    WARRIOR,
    //마검사
    MAGICWARRIOR,
    //마법사
    MAGICIAN,
    //궁수
    ARCHER,
    //탱커
    TANKER,
}

public class Champion : Unit
{
    //공격사거리(근거리는 짧게, 원거리는 길게 보다 작거나 같을때 공격가능)
    private float range_Attack;

    //마나통
    private float mana_max;

    //마나재생
    private float mana_regen;

 
    // 크리티컬확률
    private float critical;

    // n 성 
	private int starPoint = 1;

    //가격
    private int price;

    //희귀도
    private Rarity rarity;


    //스킨테마
    private SkinTheme skinthema;

    //직업테마
    private JobThema jobThema;

    //iDrag 같은거 써서 챔피언을 드래그 할 때 관련함수 만들어야함

    // 스킬 Action을 추가해서 + 한다음 캐릭터 생성할때 추가해주는 매서드가 필요하겠네
    // 캐릭 생성기능어디서할껀지 
    
    
    #region GetSet

    public float Range_Attack { get { return range_Attack; } private set { range_Attack = value; } }

    public float Mana_max { get { return mana_max; } private set { mana_max = value; } }

    public float Mana_regen { get { return mana_regen; } private set { mana_regen = value; } }

    public float Critical { get { return critical; } private set { critical = value; } }
    public int StarPoint { get { return starPoint; } private set { starPoint = value; } }

    public int Price { get { return price; } private set { price = value; } }
    
    #endregion 

    public void JaxW() { 
        //particle system
        // 추가피해량을 늘려야함, -> 적이있는 자료구조에서 적을 찾고 -> 적을 찾으면 다가가고 -> 적을 때렸다는걸 서버로 보내주는 코드짜고
        // -> 소리, 파티클, 이미지 -> 맞은애의 object -> 
        // 추가적인 데미지를 주어야함
        
    }


    public void JaxQ()
    {
        //particle system

        // 추가적인 데미지를 주어야함

    }


}




