using System;
using GunPrototype.Math;
using UnityEngine;

namespace GunPrototype.Weapons
{
    public class LineRendererTrajectorySetter
    {
        [Serializable]
        public class Config
        {
            [Tooltip("In seconds.")]
            public float TrajectoryTraverseDuration;
            [Tooltip("In seconds.")]
            public float StepTime;
        }

        private Config _config;
        private LineRenderer _lineRenderer;

        public LineRendererTrajectorySetter(Config config, LineRenderer lineRenderer)
        {
            _config = config;
            _lineRenderer = lineRenderer;
        }

        public void SetTrajectory(ITrajectory trajectory)
        {
            int iterationsCount = Mathf.RoundToInt(_config.TrajectoryTraverseDuration / _config.StepTime);
            Vector3[] trajectoryPositions = new Vector3[iterationsCount];
            _lineRenderer.positionCount = iterationsCount;

            int counter = 0;
            for (float time = 0; time < _config.TrajectoryTraverseDuration; time += _config.StepTime)
            {
                _lineRenderer.SetPosition(counter, trajectory.GetPosition(time));
                counter++;
            }   
        }
    }
}