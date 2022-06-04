using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Dynamics.World;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base;
using MonoTest.Base.Component;
using MonoTest.Base.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Physics
{
    public class Box2DSimulation : ISimulation
    {
        bool spaceWasDown = false;
        class Wrapper : IComponent
        {
            public IElement Element { get; }
            public Body Body { get; }

            public Wrapper(IElement element, Body body)
            {
                Element = element;
                Body = body;
            }
        }

        private HashSet<Wrapper> wrappers = new();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public bool Enabled { get; set; }
        public int UpdateOrder { get; }

        public World World { get; private set; }
        public float TimeStep { get; set; }
        public int VelocityIterations { get; set; } = 6;
        public int PositionIterations { get; set; } = 2;

        public void AddDynamic(IElement element, bool movable = false)
        {
            var bodyDef = new BodyDef
            {
                type = BodyType.Dynamic,
                position = new System.Numerics.Vector2(element.Transform.Position.X.ToMeter(), element.Transform.Position.Y.ToMeter())
            };

            var body = World.CreateBody(bodyDef);

            var dynamicBoxShape = new PolygonShape();
            dynamicBoxShape.SetAsBox((element.Transform.Size.X / 2).ToMeter(), (element.Transform.Size.Y / 2).ToMeter(), new System.Numerics.Vector2((element.Transform.Size.X / 2).ToMeter(), (element.Transform.Size.Y / 2).ToMeter()), 0);

            var fixtureDef = new FixtureDef
            {
                shape = dynamicBoxShape,
                density = 4f,
                friction = 0.5f,
            };
            body.CreateFixture(in fixtureDef);
            if(movable)
            {
                KeyboardController.AddHandler(Keys.Right, (_) =>
                {
                    //body.ApplyLinearImpulse(new System.Numerics.Vector2(1, 0), System.Numerics.Vector2.Zero);
                });
                KeyboardController.AddHandler(Keys.Left, (_) =>
                {
                    body.ApplyLinearImpulse(new System.Numerics.Vector2(-1, 0), System.Numerics.Vector2.Zero);
                });
            }
            wrappers.Add(new Wrapper(element, body));
        }
        public void AddFixed(IElement element)
        {
            var bodyDef = new BodyDef
            {
                position = new System.Numerics.Vector2(element.Transform.Position.X.ToMeter(), element.Transform.Position.Y.ToMeter())
            };
            var groundBody = World.CreateBody(bodyDef);
            var groundShape = new PolygonShape();
            groundShape.SetAsBox((element.Transform.Size.X/2).ToMeter(), (element.Transform.Size.Y/2).ToMeter(), new System.Numerics.Vector2((element.Transform.Size.X / 2).ToMeter(), (element.Transform.Size.Y / 2).ToMeter()), 0);

            groundBody.CreateFixture(groundShape);
        }
        public void Initialize()
        {
            Enabled = false;
            World = new World(new System.Numerics.Vector2(0, 10));
            //var bodyDef = new BodyDef
            //{
            //    position = new System.Numerics.Vector2(0, 200)
            //};
            //var groundBody = World.CreateBody(bodyDef);
            //var groundShape = new PolygonShape();
            //groundShape.SetAsBox(50, 10);

            //groundBody.CreateFixture(groundShape);

            TimeStep = 1f / 60f;

        }

        public void Simulate(GameTime gameTime)
        {
            if(Keys.Space.IsDown())
            {
                if (!spaceWasDown)
                {
                    Enabled = !Enabled;
                    spaceWasDown = true;
                }
            }
            else
            {
                spaceWasDown = false;
            }
            if(Enabled || Keys.O.IsDown())
            {
                World.Step(TimeStep, VelocityIterations, PositionIterations);
                foreach (var wrapper in wrappers)
                {
                    wrapper.Element.Transform.Position.X = wrapper.Body.Position.X.ToPixel();
                    wrapper.Element.Transform.Position.Y = wrapper.Body.Position.Y.ToPixel();
                    wrapper.Element.Transform.Rotation = wrapper.Body.GetAngle();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            Simulate(gameTime);
        }
    }
}
