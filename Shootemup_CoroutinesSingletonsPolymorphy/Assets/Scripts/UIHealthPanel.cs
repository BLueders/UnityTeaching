// <copyright file="GameManager.cs" company="DIS Copenhagen">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>19/09/2019</date>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPanel : MonoBehaviour
{
    public static UIHealthPanel instance;

	[SerializeField] Image[] hearts;

    private void Awake()
    {
       instance = this;
    }

    /// <summary>
    /// Updates the hearts by modifying its image component depending of the number of lives.
    /// </summary>
    /// <param name="lives">Lives.</param>
    public void SetLives (int lives)
	{
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
	}
}
