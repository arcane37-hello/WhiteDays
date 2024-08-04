using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(ThirdPersonCharacter))]
	public class AICharacterControl : MonoBehaviour
	{
		public Text DBGUI;

		public Transform target;

		public NavMeshAgent agent { get; private set; }

		public ThirdPersonCharacter character { get; private set; }

		private void Start()
		{
			agent = GetComponentInChildren<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();
			agent.updateRotation = false;
			agent.updatePosition = false;
		}

		private void Update()
		{
			if (!agent.enabled)
			{
				character.Move(Vector3.zero, false, false);
			}
			else if (target != null)
			{
				agent.SetDestination(target.position);
				character.Move(agent.desiredVelocity, false, false);
			}
			else
			{
				character.Move(Vector3.zero, false, false);
			}
		}

		public void SetTarget(Transform _target)
		{
			target = _target;
		}
	}
}
