using System.Collections;
using System.Collections.Generic;
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


}
