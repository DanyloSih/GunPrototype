using UnityEngine;

namespace GunPrototype.SpatialControllers
{
    public interface IObjectMover
	{
		public void Move(Vector2 moveDirection);
	}
}