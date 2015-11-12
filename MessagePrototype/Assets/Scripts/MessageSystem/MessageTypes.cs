using UnityEngine;
using System.Collections;

public class MessageTypes : MonoBehaviour 
{
	public enum MsgType
	{
		LayTrap,
		CheckTrap,
		SpawnGuy,
		MoveTo
	};
}