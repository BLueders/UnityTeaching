// <copyright file="BaseEnemy.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>07/14/2017</date>

using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all enemies that can be killed somehow.
/// </summary>
public abstract class BaseEnemy : MonoBehaviour
{
	public abstract void Die ();
}

