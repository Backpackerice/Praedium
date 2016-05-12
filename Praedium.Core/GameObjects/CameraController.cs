using Praedium.Core.Components;
using Praedium.Engine;
using Praedium.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praedium.Core.GameObjects
{
    [RequireComponent(typeof(CameraMovementHandler))]
    public class CameraController : GameObject
    {
        private CameraMovementHandler handler;
        protected override void OnStart()
        {
            handler = GetComponent<CameraMovementHandler>() as CameraMovementHandler;

            handler.LAG = 0.001f;
        }
    }
}
