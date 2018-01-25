﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleButton : MonoBehaviour
{

	public Button buttonComponent;
	public Text nameLabel;
	public Image iconImage;

	// Use this for initialization
	void Start()
	{
		buttonComponent.onClick.AddListener(HandleClick);
	}

	public void Setup(Item currentItem, ShopScrollList currentScrollList)
	{
		item = currentItem;
		nameLabel.text = item.itemName;
		iconImage.sprite = item.icon;
		priceText.text = item.price.ToString();
		scrollList = currentScrollList;

	}

	public void HandleClick()
	{

	}
}