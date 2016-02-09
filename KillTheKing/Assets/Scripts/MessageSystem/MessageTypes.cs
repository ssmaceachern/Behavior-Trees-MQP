﻿using UnityEngine;
using System.Collections;

public class MessageTypes : MonoBehaviour 
{
	public enum MsgType
	{
		LayTrap,
		CheckTrap,
		SpawnGuy,
		SetTarget,
		DealDamage,
		MoveTo,
		GiveCommand,
		ActivateEntity,
		MakeGreedy,
		ResetAI,
		ChangeGold,
		DestroyBuilding,
		GetSpooked,
		FollowMe,
		Saved,
		SelectUnit,
		DeselectUnit,
		GhoulBomb,
		PriestHeal,
		BlueSong,
		GreenSong,
        Deactivate,
        SpikeTrap
	};
}