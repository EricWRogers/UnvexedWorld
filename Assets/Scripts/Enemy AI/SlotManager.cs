using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This Script needs to get attched to the player inorder for the AI to properly surround the Player
*/
public class SlotManager : MonoBehaviour
{
	private List<GameObject> slots;
	public int count;
	public float distance = 2f;

	void Start()
	{
		slots = new List<GameObject> ();
		for (int index = 0; index < count; ++index)
		{
			slots.Add (null);
		}
	}

	public Vector3 GetSlotPosition(int index)
	{
		float degreesPerIndex = 360f / count;
		var pos = transform.position;
		var offset = new Vector3 (0f, 0f, distance);
		return pos + (Quaternion.Euler(new Vector3(0f, degreesPerIndex * index, 0f)) * offset);
	}

	public int Reserve(GameObject attacker)
	{
		var bestPosition = transform.position;
		var offset = (attacker.transform.position - bestPosition).normalized * distance;
		bestPosition += offset;
		int bestSlot = -1;
		float bestDist = 99999f;
		for (int index = 0; index < slots.Count; ++index)
		{
			if (slots [index] != null)
				continue;
			var dist = (GetSlotPosition (index) - bestPosition).sqrMagnitude;
			if (dist < bestDist)
			{
				bestSlot = index;
				bestDist = dist;
			}
		}
		if (bestSlot != -1)
			slots [bestSlot] = attacker;
		return bestSlot;
	}

	public void Release(int slot)
	{
		slots [slot] = null;
	}

	void OnDrawGizmos()
	{
		for (int index = 0; index < count; ++index)
		{
			if (slots == null || slots.Count <= index || slots [index] == null)
				Gizmos.color = Color.black;
			else
				Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (GetSlotPosition (index), 0.5f);
		}
	}
}
