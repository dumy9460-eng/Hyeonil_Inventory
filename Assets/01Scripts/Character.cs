using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Character : MonoBehaviour
{
    // 기본 정보
    public string Name { get; private set; }
    public string Job { get; private set; }
    public int Level { get; private set; }
    public int Exp { get; private set; }
    public int MaxExp { get; private set; }
    public int Gold { get; private set; }

    // 기본 스탯
    public int BaseAttack { get; private set; }
    public int BaseDefense { get; private set; }
    public int BaseHP { get; private set; }
    public int BaseCritical { get; private set; }

    // 최종 스탯 (장비 포함)
    public int TotalAttack => BaseAttack + GetEquipmentBonus("Attack");
    public int TotalDefense => BaseDefense + GetEquipmentBonus("Defense");
    public int TotalHP => BaseHP + GetEquipmentBonus("HP");
    public int TotalCritical => BaseCritical + GetEquipmentBonus("Critical");

    // 인벤토리
    public List<Item> Inventory { get; private set; }
    public List<Item> EquippedItems { get; private set; }

    // 설명
    public string Description { get; private set; }

    // 생성자
    public Character(string name, string job, int level, int exp, int maxExp, int gold,
                     int attack, int defense, int hp, int critical, string description)
    {
        Name = name;
        Job = job;
        Level = level;
        Exp = exp;
        MaxExp = maxExp;
        Gold = gold;
        BaseAttack = attack;
        BaseDefense = defense;
        BaseHP = hp;
        BaseCritical = critical;
        Description = description;

        Inventory = new List<Item>();
        EquippedItems = new List<Item>();
    }

    // 아이템 추가
    public void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    // 아이템 장착
    public void Equip(Item item)
    {
        if (!Inventory.Contains(item))
            return;

        if (EquippedItems.Contains(item))
            return;

        EquippedItems.Add(item);
    }

    // 아이템 장착 해제
    public void UnEquip(Item item)
    {
        if (EquippedItems.Contains(item))
        {
            EquippedItems.Remove(item);
        }
    }

    // 장비 보너스 계산
    private int GetEquipmentBonus(string statType)
    {
        int bonus = 0;
        foreach (Item item in EquippedItems)
        {
            switch (statType)
            {
                case "Attack":
                    bonus += item.AttackBonus;
                    break;
                case "Defense":
                    bonus += item.DefenseBonus;
                    break;
                case "HP":
                    bonus += item.HPBonus;
                    break;
                case "Critical":
                    bonus += item.CriticalBonus;
                    break;
            }
        }
        return bonus;
    }

    // 골드 추가/차감
    public void AddGold(int amount)
    {
        Gold += amount;
        if (Gold < 0) Gold = 0;
    }

    // 경험치 추가
    public void AddExp(int amount)
    {
        Exp += amount;
        while (Exp >= MaxExp)
        {
            LevelUp();
        }
    }

    // 레벨업
    private void LevelUp()
    {
        Exp -= MaxExp;
        Level++;
        MaxExp = Level * 10 + 2; // 간단한 레벨업 공식

        // 스탯 증가
        BaseAttack += 2;
        BaseDefense += 1;
        BaseHP += 5;
        BaseCritical += 1;
    }

    // 장착 가능 여부 확인
    public bool CanEquip(Item item)
    {
        // 이미 장착 중인 같은 타입의 아이템이 있는지 확인
        foreach (Item equipped in EquippedItems)
        {
            if (equipped.Type == item.Type)
            {
                return false; // 같은 타입은 1개만 장착 가능
            }
        }
        return true;
    }
}
