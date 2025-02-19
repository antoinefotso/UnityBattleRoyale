﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityPUBG.Scripts.Entities;
using UnityPUBG.Scripts.Items;
using UnityPUBG.Scripts.Logic;

namespace UnityPUBG.Scripts
{
    public class ItemContainerDebugger : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_Text debugText;

        private Player containerOwner;
        private readonly StringBuilder stringBuilder = new StringBuilder();

        #region 유니티 메시지
        private void Awake()
        {
            EntityManager.Instance.OnMyPlayerSpawn += (object sender, EventArgs e) =>
            {
                containerOwner = EntityManager.Instance.MyPlayer;
                if (containerOwner != null && containerOwner.ItemContainer != null)
                {
                    containerOwner.ItemContainer.OnContainerUpdate += ItemContainer_OnUpdateContainer;
                    debugText.text = $"ItemConainer: Count[{containerOwner.ItemContainer.Count}/{containerOwner.ItemContainer.Capacity}]";
                }
                else
                {
                    debugText.text = $"디버깅 할 {nameof(containerOwner.ItemContainer)}를 찾을 수 없습니다";
                }
            };
        }

        //private void Update()
        //{
        //    if (Keyboard.current.digit1Key.wasPressedThisFrame)
        //    {
        //        containerOwner.DropItemsAtSlot(0, 1);
        //    }
        //    else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        //    {
        //        containerOwner.DropItemsAtSlot(1, 1);
        //    }
        //    else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        //    {
        //        containerOwner.DropItemsAtSlot(2, 1);
        //    }
        //    else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        //    {
        //        containerOwner.DropItemsAtSlot(3, 1);
        //    }
        //    else if (Keyboard.current.digit5Key.wasPressedThisFrame)
        //    {
        //        containerOwner.DropItemsAtSlot(4, 1);
        //    }
        //    else if (Keyboard.current.digit6Key.wasPressedThisFrame)
        //    {
        //        containerOwner.DropItemsAtSlot(5, 1);
        //    }
        //}
        #endregion

        private void ItemContainer_OnUpdateContainer(object sender, System.EventArgs e)
        {
            if (!(sender is ItemContainer container))
            {
                return;
            }

            stringBuilder.Clear();
            stringBuilder.AppendLine($"ItemConainer: Count[{container.Count}/{container.Capacity}]");
            for (int slot = 0; slot < container.Count; slot++)
            {
                var item = container.GetItemAt(slot);
                stringBuilder.Append($"Slot{slot}: ");
                if (item.IsStackEmpty)
                {
                    stringBuilder.AppendLine("Empty");
                }
                else
                {
                    stringBuilder.AppendLine($"{item.Data.ItemName} [{item.CurrentStack}/{item.Data.MaximumStack}]");
                }
            }
            debugText.text = stringBuilder.ToString();
        }
    }
}