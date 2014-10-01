using UnityEngine;
using System.Collections;

public abstract class AttackBase : MonoBehaviour {

	public abstract float Damage {
				get;
				set;
	}

	public abstract float Range {
				get;
				set;
	}

	public abstract float timeRemaining {
				get;
				set;
	}

	public abstract void attack(Vector3 direction, Vector3 pointOfOrigin);


}
