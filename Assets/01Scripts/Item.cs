using System;

[Serializable]
public class Item
{
    public string Name;
    public string Type;
    public string Description;

    public int AttackBonus;
    public int DefenseBonus;
    public int HPBonus;
    public int CriticalBonus;

    public string IconName;

    // 기본 생성자 (JSON 역직렬화용)
    public Item() { }

    // 일반 생성자
    public Item(string name, string type, string description,
                int attack, int defense, int hp, int critical, string iconName)
    {
        Name = name;
        Type = type;
        Description = description;
        AttackBonus = attack;
        DefenseBonus = defense;
        HPBonus = hp;
        CriticalBonus = critical;
        IconName = iconName;
    }
}
