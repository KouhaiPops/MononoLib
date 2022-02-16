using Microsoft.Xna.Framework;

using MonoTest.Debug;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.TestScenes.Utils
{
    public class CircleMouseBehaviour : IUpdateBehaviour<CircleN>
    {
        Point lastPosition;
        public void OnUpdate(CircleN updatable)
        {
            var mousePos = Microsoft.Xna.Framework.Input.Mouse.GetState().Position;
            if(mousePos.X != lastPosition.X && mousePos.Y != lastPosition.Y)
            {
            }
            var rPos = updatable.Transform.Position - mousePos.ToVector2();
            var circleHypo = (rPos.X * rPos.X) + (rPos.Y * rPos.Y);
            var hypo = (int)Math.Floor(Math.Sqrt(circleHypo));
            var smallerCircleRadius = updatable.Radius * 0.8f;
            //Stats.QueueMessage($"Rel posotion: {updatable.Transform.Position - mousePos.ToVector2()} Hypo: {hypo}");
            if(hypo > smallerCircleRadius)
            {
                rPos.Normalize();
                var delta = hypo - smallerCircleRadius;
                var direction = rPos * delta;
                //Stats.QueueMessage($"Outside smaller {direction}");
                updatable.Transform.Position -= direction;
                //updatable.Transform.Position.X = mousePos.X;
                //updatable.Transform.Position.Y = mousePos.Y;
            }
            lastPosition = mousePos;
            
        }
    }
}
