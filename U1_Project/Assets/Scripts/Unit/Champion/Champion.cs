using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Rarity
{
    //�Ϲ�
    COMMON,
    //���
    RARE,
    //����
    HERO,
    //����
    EPIC,
    //������
    LEGEND,
}

public enum SkinTheme
{
    //������Ʈ ��Ų
    CYBORG,
    //�ܿ�ձ�
    FROZEN,
    //�Ÿ��� �Ǹ�
    NIGHTMARE,
    //�Ƹ�ī��
    ARCANA,
    //�ܹ�
    HONEYBEE,
}

public enum JobThema
{
    //�˻�
    WARRIOR,
    //���˻�
    MAGICWARRIOR,
    //������
    MAGICIAN,
    //�ü�
    ARCHER,
    //��Ŀ
    TANKER,
}


public class Champion : Unit
{
    //������
    private float mana_max;

    //�������
    private float mana_regen;

 
    // ũ��Ƽ��Ȯ��
    private float critical;

    // n �� 
	private int starPoint = 1;

    //����
    private int price;

    //��͵�
    private Rarity rarity;


    //��Ų�׸�
    private SkinTheme skinthema;

    //�����׸�
    private JobThema jobThema;

    //iDrag ������ �Ἥ è�Ǿ��� �巡�� �� �� �����Լ� ��������


}
