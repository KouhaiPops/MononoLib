using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Graphics;
using MonoTest.Base.Input;
using MonoTest.Core.Collision;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public class UITree : GraphicGroup<StatefullUI<IUI>>
    {
        public UITree AddControl(IUI control)
        {
            Drawables.Add(new(control));
            return this;
        }
        public override void Update(GameTime gameTime)
        {

            var mousePosition = MouseInput.Position;
            var leftBtnPressed = MouseInput.LeftBtn;
            foreach (var stateful in Drawables)
            {
                var element = stateful.Element;
                var intersecting = element.BoundingBox.PointIntersecting(mousePosition);
                if (intersecting)
                {
                    if(leftBtnPressed && !stateful.Clicked)
                    {
                        element.Click();
                        stateful.Clicked = true;
                    }
                    else if(!leftBtnPressed && stateful.Clicked)
                    {
                        element.Release();
                        stateful.Clicked = false;
                    }
                    else if(stateful.Hovered)
                    {
                        element.Hover();
                    }
                    else
                    {
                        element.Enter();
                        stateful.Hovered = true;
                    }
                }
                else
                {
                    if(stateful.Hovered)
                    {
                        element.Exit();
                        stateful.Hovered = false;
                        if(stateful.Clicked)
                        {
                            element.Release();
                            stateful.Clicked = false;
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

    }
}
