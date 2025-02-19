﻿using UnityEngine;

[CreateAssetMenu(menuName = "UnityPUBG/ItemData/RarityColor")]
public class ItemRarityColorData : ScriptableObject
{
    [SerializeField] private Color color;

    public Color Color => color;
}