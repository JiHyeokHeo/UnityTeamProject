using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
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
    //���ݻ�Ÿ�(�ٰŸ��� ª��, ���Ÿ��� ��� ���� �۰ų� ������ ���ݰ���)
    private float range_Attack;

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

    // ��ų Action�� �߰��ؼ� + �Ѵ��� ĳ���� �����Ҷ� �߰����ִ� �ż��尡 �ʿ��ϰڳ�
    // ĳ�� ������ɾ���Ҳ��� 
    
    
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
        // �߰����ط��� �÷�����, -> �����ִ� �ڷᱸ������ ���� ã�� -> ���� ã���� �ٰ����� -> ���� ���ȴٴ°� ������ �����ִ� �ڵ�¥��
        // -> �Ҹ�, ��ƼŬ, �̹��� -> �������� object -> 
        // �߰����� �������� �־����
        
    }


    public void JaxQ()
    {
        //particle system

        // �߰����� �������� �־����

    }


}




